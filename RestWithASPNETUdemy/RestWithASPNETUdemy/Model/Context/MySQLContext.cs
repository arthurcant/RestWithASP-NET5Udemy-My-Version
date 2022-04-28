using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext>  options) : base(options) 
        {

        }
                
        public DbSet<Person> People { get; set; } // Um DbSet representa a coleção de todas as entidades no contexto ou que pode ser 
                                                  // consultada a partir do banco de dados, de um determinado tipo.
                                                  // Os objetos DbSet são criados a partir de um DbContext usando o método DbContext.
        public DbSet<Book> Books { get; set; }
        


    }
}