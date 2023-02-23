using System;

using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Models;

namespace Application.Interfaces;

public interface IUsuarioService
{
    Task<List<Usuario>> Read();
    Task<UserResponseDto> Read(Guid id);
    Task<bool> Create(CreateUserDto usuario);
    Task<UserResponseDto> Update(UpdateUserDto usuario);
    Task<bool> Delete(Guid id);
}

