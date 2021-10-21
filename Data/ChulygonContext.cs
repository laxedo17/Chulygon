using Chulygon.Models;
using Microsoft.EntityFrameworkCore;

namespace Chulygon.Data
{
    public class ChulygonContext : DbContext
    {
        //Heredamos os elementos da clase base e non temos que tocar nada aqui o resto da app
        public ChulygonContext(DbContextOptions<ChulygonContext> opt) : base(opt)
        {

        }

        //DbContext e a base de datos en si, mentres que cada elemento DbSet representa unha taboa na base de datos
        public DbSet<Comando> Comandos { get; set; }
    }
}