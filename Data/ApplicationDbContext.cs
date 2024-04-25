using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Proyecto1.Models;

namespace Proyecto1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Ejercicio> Ejercicios { get; set; } = null!;
    public DbSet<EjercicioFisico> EjerciciosFisicos { get; set; } = null!;
}
