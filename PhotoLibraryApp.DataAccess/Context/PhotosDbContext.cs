using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLibraryApp.DataAccess.Models;

namespace PhotoLibraryApp.DataAccess.Context
{
    public class PhotosDbContext : DbContext
    {
        public PhotosDbContext(DbContextOptions options) : base(options)
        {
        }

        protected PhotosDbContext()
        {

        }
        public DbSet<PhotoModel> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModel>(
                entity =>
                {
                    entity.HasKey("PhotoID");
                    entity.Property("PhotoID");
                    entity.Property("Name").HasMaxLength(50);
                    entity.Property("ImagePath").HasMaxLength(int.MaxValue);
                    entity.Property("Date").HasColumnType("datatime");                    
                }
                );
        }
    }
}
