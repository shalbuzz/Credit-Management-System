using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(m => m.Address)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(m => m.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(m => m.Email)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(m => m.Description)
                   .HasMaxLength(1000)
                   .IsRequired(false);

            builder.HasMany(m => m.Branches)
                   .WithOne(b => b.Merchant)
                   .HasForeignKey(b => b.MerchantId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
