using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class LoanItemConfiguration : IEntityTypeConfiguration<LoanItem>
    {
        public void Configure(EntityTypeBuilder<LoanItem> builder)
        {
            builder.HasKey(li => li.Id);

            builder.Property(li => li.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(li => li.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(li => li.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(li => li.Product)
                   .WithMany(p => p.LoanItems)
                   .HasForeignKey(li => li.ProductId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(li => li.Loan)
                   .WithMany(l => l.LoanItems)
                   .HasForeignKey(li => li.LoanId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
