using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Contexts;

public class PostgresContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) {}
}