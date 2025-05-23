using Credit_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit_Management_System.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
           
            builder.HasKey(e => e.Id);
       
            builder.Property(e => e.FullName)
                   .IsRequired()
                   .HasMaxLength(100);
      
            builder.Property(e => e.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);
         
            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(100);
       
            builder.Property(e => e.Position)
                   .HasMaxLength(50);
    
            builder.Property(e => e.Department)
                   .HasMaxLength(50);

            builder.Property(e => e.Description)
                   .HasMaxLength(250);

            builder.HasOne(e => e.Branch)
                   .WithMany(b => b.Employees)
                   .HasForeignKey(e => e.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.LoansHandled)
          .WithOne(l => l.Employee);

            builder.HasOne(e => e.User)
              .WithMany()
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
