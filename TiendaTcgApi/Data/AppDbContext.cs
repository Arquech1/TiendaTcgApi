﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Entidades;

namespace TiendaTcgApi.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; }
    }
}
