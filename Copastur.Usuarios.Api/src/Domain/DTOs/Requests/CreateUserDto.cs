using System;

namespace Domain.DTOs.Requests;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}