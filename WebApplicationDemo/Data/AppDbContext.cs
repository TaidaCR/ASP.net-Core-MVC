using Microsoft.EntityFrameworkCore;
using WebApplicationDemo.Models;


/*MANEJA PETICIONES A BASE DE DATOS*/
namespace WebApplicationDemo.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }   
        
        public DbSet<Tarea> Tarea { get; set; }
    }
}
