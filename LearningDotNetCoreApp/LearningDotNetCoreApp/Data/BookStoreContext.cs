using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) :base(options)
        {

        }

        public DbSet<Books> books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookGallery> BookGalleries { get; set; }
    }
}
