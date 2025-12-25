using Microsoft.EntityFrameworkCore;
using WebAppPractiece.Models;

namespace WebAppPractiece.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderDetail> SlidersDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    Id = 1,
                    Image = "blog-feature-img-1.jpg",
                    Title = "First Blog Post",
                    Desciption = "This is the content of the first blog post.",
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                    IsDeleted = false
                },
                new Blog
                {
                    Id = 2,
                    Image = "blog-feature-img-3.jpg",
                    Title = "Second Blog Post",
                    Desciption = "This is the content of the second blog post.",
                    CreatedAt = new DateTime(2025, 1, 2, 10, 0, 0),
                    IsDeleted = false
                },
                new Blog
                {
                    Id = 3,
                    Image = "blog-feature-img-4.jpg",
                    Title = "Third Blog Post",
                    Desciption = "This is the content of the third blog post.",
                    CreatedAt = new DateTime(2025, 1, 3, 10, 0, 0),
                    IsDeleted = false
                }
            );
        }
    }
}
