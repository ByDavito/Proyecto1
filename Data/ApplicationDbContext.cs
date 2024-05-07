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
    public DbSet<TipoEjercicio> TipoEjercicios { get; set; }
    public DbSet<EjercicioFisico> EjerciciosFisicos { get; set; }
}
