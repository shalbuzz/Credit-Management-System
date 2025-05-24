using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(l => l.InterestRate)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(l => l.DurationInMonths)
                   .IsRequired()
                   .HasDefaultValue(12);

            builder.Property(l => l.StartDate)
                   .IsRequired();

            builder.Property(l => l.EndDate)
                   .IsRequired();

            builder.Property(l => l.StatusForLoan)
                   .HasMaxLength(50);

            builder.HasOne(l => l.Customer)
                   .WithMany(c => c.Loans)
                   .HasForeignKey(l => l.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Employee)
       .WithMany(e => e.LoansHandled)
       .HasForeignKey(l => l.EmployeeId)
       .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.LoanDetail)
                   .WithOne(ld => ld.Loan)
                   .HasForeignKey<LoanDetail>(ld => ld.LoanId)
                   .OnDelete(DeleteBehavior.Cascade); 

            
            builder.HasMany(l => l.LoanItems)
                   .WithOne(li => li.Loan)
                   .HasForeignKey(li => li.LoanId)
                   .OnDelete(DeleteBehavior.Cascade);

          
            builder.HasMany(l => l.Payments)
                   .WithOne(p => p.Loan)
                   .HasForeignKey(p => p.LoanId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
