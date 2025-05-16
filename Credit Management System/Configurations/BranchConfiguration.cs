using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            
            builder.HasKey(b => b.Id);

           
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Address)
                .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                   .HasMaxLength(20);

            builder.Property(b => b.Email)
                .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(500);

            
            builder.HasOne(b => b.Merchant)
                   .WithMany(m => m.Branches)
                   .HasForeignKey(b => b.MerchantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Employees)
                   .WithOne(e => e.Branch)
                   .HasForeignKey(e => e.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
