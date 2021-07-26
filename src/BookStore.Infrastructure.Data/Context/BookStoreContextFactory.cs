using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.Infrastructure.Data.Context 
{
    public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();
            optionsBuilder.UseNpgsql("Server = 127.0.0.1; Port = 5432; Database = bookstore; User Id = postgres; Password = pass;").UseSnakeCaseNamingConvention();            

            return new BookStoreContext(optionsBuilder.Options, null);
        }
    }
}