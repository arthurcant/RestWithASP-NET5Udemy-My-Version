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
        
        public DbSet<User> Users { get; set; }
        public virtual DbSet<Teste> Testes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql(@"Data Source=Macoratti;Initial Catalog=Northwind;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teste>().HasData(
                new Teste
                {
                    TesteId = 1,
                    Nome = "Macoratti",
                    Email = "macoratti@yahoo.com"
                },
                new Teste
                {
                    TesteId = 2,
                    Nome = "Miriam",
                    Email = "mimi@hotmail.com"
                }
            );
        }
}