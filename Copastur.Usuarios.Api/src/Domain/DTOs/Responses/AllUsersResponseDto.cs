using Domain.Models;

namespace Domain.DTOs.Responses;

public class AllUsersResponseDto
{
    public bool Status { get; set; }
    public List<Usuario> Usuarios { get; set; }
}
