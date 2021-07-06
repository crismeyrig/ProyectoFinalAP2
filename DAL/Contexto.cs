using ProyectoFinalAP2.BLL;
using ProyectoFinalAP2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalAP2.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source= Data/Usuarios.db");
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                UsuarioId = 1,
                Nombres = "Administrador",
                NombreUsuario = "admin",
                Contrasena = UsuarioBLL.Encriptar("admin"),
                Email = "Adminis@example.com"
            });
        }


    }
}
