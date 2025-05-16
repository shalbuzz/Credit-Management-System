using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class LoanDetailConfiguration : IEntityTypeConfiguration<LoanDetail>
    {
        public void Configure(EntityTypeBuilder<LoanDetail> builder)
        {
            builder.HasKey(ld => ld.LoanId);

            builder.Property(ld => ld.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(ld => ld.CurrentDebt)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(ld => ld.InterestRate)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(ld => ld.StartDate)
                   .IsRequired();

            builder.Property(ld => ld.EndDate)
                   .IsRequired();

            builder.Property(ld => ld.Status)
                   .HasMaxLength(50);

            builder.HasOne(ld => ld.Loan)
                   .WithOne(l => l.LoanDetail)
                   .HasForeignKey<LoanDetail>(ld => ld.LoanId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
