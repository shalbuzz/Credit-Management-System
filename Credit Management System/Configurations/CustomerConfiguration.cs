using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(c => c.Loans)
                   .WithOne(l => l.Customer)
                   .HasForeignKey(l => l.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
