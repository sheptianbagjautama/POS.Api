using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using POS.Api.Entities;
using POS.Api.Models;

namespace POS.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Produk> Produk { get; set; }
        public DbSet<Meja> Meja { get; set; }
        public DbSet<Pesanan> Pesanan { get; set; }
        public DbSet<ProdukPesanan> ProdukPesanan { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Konfigurasi untuk harga di table produk
            builder.Entity<Produk>()
                .Property(p => p.Harga)
                .HasPrecision(18, 2); //18 total digit, 2 digit desimal

            builder.Entity<ProdukPesanan>()
                .HasKey(pp => new { pp.PesananId, pp.ProdukId });

            builder.Entity<ProdukPesanan>()
                .HasOne(pp => pp.Pesanan)
                .WithMany(p => p.ProdukPesanan)
                .HasForeignKey(pp => pp.PesananId);

            builder.Entity<ProdukPesanan>()
                .HasOne(pp => pp.Produk)
                .WithMany()
                .HasForeignKey(pp => pp.ProdukId);
        }
    }
}
