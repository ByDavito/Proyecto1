﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public DbSet<Lugar> Lugares { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Persona_tipoEjercicio> Persona_tipoEjercicio { get; set; }
}
