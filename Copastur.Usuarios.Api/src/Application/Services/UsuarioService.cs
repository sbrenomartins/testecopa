using System.Text.Json;

using Application.Interfaces;

using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Models;

using Infra.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly PostgresContext _context;
    private readonly IRabbitMQProducer _producer;

    public UsuarioService(PostgresContext context, IRabbitMQProducer producer)
    {
        _context = context;
        _producer = producer;
    }

    public async Task<AllUsersResponseDto> Read()
    {
        try
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return new AllUsersResponseDto
            {
                Status = true,
                Usuarios = usuarios
            };
        }
        catch (Exception ex)
        {
            return new AllUsersResponseDto
            {
                Status = false,
                Usuarios = new List<Usuario>()
            };
        }
    }

    public async Task<UserResponseDto> Read(Guid id)
    {
        try
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null)
                throw new Exception();

            return new UserResponseDto
            {
                Status = true,
                Usuario = usuario
            };

        }
        catch (Exception ex)
        {
            return new UserResponseDto
            {
                Status = false,
                Usuario = null
            };
        }
    }

    public async Task<bool> Create(CreateUserDto dto)
    {
        using (await _context.Database.BeginTransactionAsync())
        {
            try
            {
                Guid id = new Guid();
                var response = await _context.Usuarios.AddAsync(new Usuario
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Id = id
                });

                var message = new AuditoriaMessageDto
                {
                    Content = JsonSerializer.Serialize(dto),
                    Date = DateTime.UtcNow,
                    Method = "POST"
                };

                _producer.SendAuditMessage(message);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return false;
            }
        }
    }

    public async Task<UserResponseDto> Update(UpdateUserDto dto)
    {
        using (await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == dto.Id);

                if (usuario is null)
                    throw new Exception();

                usuario.Name = dto.Name;
                usuario.Email = dto.Email;
                usuario.Id = dto.Id;

                var response = _context.Usuarios.Update(usuario);

                var message = new AuditoriaMessageDto
                {
                    Content = JsonSerializer.Serialize(dto),
                    Date = DateTime.UtcNow,
                    Method = "PUT"
                };

                _producer.SendAuditMessage(message);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return new UserResponseDto
                {
                    Status = true,
                    Usuario = usuario
                };
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return new UserResponseDto
                {
                    Status = false,
                    Usuario = null
                };
            }
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        using (await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario is null)
                    throw new Exception();

                var response = _context.Usuarios.Remove(usuario);

                var message = new AuditoriaMessageDto
                {
                    Content = JsonSerializer.Serialize(id),
                    Date = DateTime.UtcNow,
                    Method = "DELETE"
                };

                _producer.SendAuditMessage(message);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return false;
            }
        }
    }
}
