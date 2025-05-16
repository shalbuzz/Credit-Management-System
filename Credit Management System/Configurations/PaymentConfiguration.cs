using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); 

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.ReferenceNumber)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.HasOne(p => p.Loan)
                   .WithMany(l => l.Payments)
                   .HasForeignKey(p => p.LoanId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
