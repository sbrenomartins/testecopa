using Domain.Models;

namespace Domain.DTOs.Responses;

public class UserResponseDto
{
    public bool Status { get; set; }
    public Usuario? Usuario { get; set; }
}