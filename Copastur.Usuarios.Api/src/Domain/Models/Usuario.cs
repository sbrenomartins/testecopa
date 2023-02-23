using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("usuarios")]
public class Usuario
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("email")]
    public string Email { get; set; }
}