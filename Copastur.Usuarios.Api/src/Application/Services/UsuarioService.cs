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

    public UsuarioService(PostgresContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> Read()
    {
        try
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }
        catch (Exception ex)
        {
            return new List<Usuario>();
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
