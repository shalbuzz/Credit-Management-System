using System.Runtime.InteropServices;
using AutoMapper;
using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Services.Implementations
{
    public class LoanDetailService : ILoanDetailService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LoanDetailService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDetailVM>> GetAllWithLoansAsync()
        {
            var loanDetails = await _context.LoanDetails
                .Include(ld => ld.Loan)
                    .ThenInclude(l => l.Customer)
                .Where(ld => !ld.IsDeleted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LoanDetailVM>>(loanDetails);
        }

        public async Task<LoanDetailVM?> GetByLoanIdAsync(int loanId)
        {
            var loanDetail = await _context.LoanDetails
                .Include(ld => ld.Loan)
                .FirstOrDefaultAsync(ld => ld.LoanId == loanId);

            return _mapper.Map<LoanDetailVM>(loanDetail);
        }

        public async Task<LoanDetailVM?> GetByIdWithLoansAsync(int id)
        {
            var loanDetail = await _context.LoanDetails
                .Include(ld => ld.Loan)
                    .ThenInclude(l => l.Customer)
                .FirstOrDefaultAsync(ld => ld.LoanId == id);

            return _mapper.Map<LoanDetailVM>(loanDetail);
        }

        public async Task<LoanDetailDetailsVM?> GetByIdWithLoansAndPaymentsAsync(int id)
        {
            var loanDetail = await _context.LoanDetails
                .Include(ld => ld.Loan)
                    .ThenInclude(l => l.Payments)
                .Include(ld => ld.Loan.Customer)
                .FirstOrDefaultAsync(ld => ld.LoanId == id);

            return _mapper.Map<LoanDetailDetailsVM>(loanDetail);
        }

        public async Task<LoanDetailUpdateVM?> GetByIdVMWithLoansAndPaymentsAsync(int id)
        {
            var loanDetail = await _context.LoanDetails
             
                .FirstOrDefaultAsync(ld => ld.Id == id && !ld.IsDeleted);

            if (loanDetail == null)
                return null;

            return _mapper.Map<LoanDetailUpdateVM>(loanDetail);
        }



        public async Task<LoanDetailCreateVM> CreateWithLoansAsync(LoanDetailCreateVM createVM)
        {
            var loan = await _context.Loans
                .FirstOrDefaultAsync(l => l.Id == createVM.LoanId && !l.IsDeleted);

            if (loan == null)
            {
                throw new Exception("Cannot create or restore LoanDetail because the associated Loan is deleted or not found.");
            }

            var existing = await _context.LoanDetails
                .FirstOrDefaultAsync(x => x.LoanId == createVM.LoanId);

            if (existing != null)
            {
                if (existing.IsDeleted)
                {
                    _mapper.Map(createVM, existing);
                    existing.IsDeleted = false;
                    existing.UpdatedAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    return _mapper.Map<LoanDetailCreateVM>(existing);
                }
                else
                {
                    throw new Exception("LoanDetail already exists for this loan.");
                }
            }

            var entity = _mapper.Map<LoanDetail>(createVM);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.LoanDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<LoanDetailCreateVM>(entity);
        }




        public async Task<LoanDetailUpdateVM> UpdateWithLoansAsync(LoanDetailUpdateVM updateVM)
        {
            var entity = await _context.LoanDetails
                .FirstOrDefaultAsync(x => x.Id == updateVM.Id && !x.IsDeleted);

            if (entity == null)
                throw new Exception("LoanDetail not found.");

            _mapper.Map(updateVM, entity); 

            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<LoanDetailUpdateVM>(entity);
        }



        public async Task DeleteAsync(int id)
        {
            var loanDetail = await _context.LoanDetails.FirstOrDefaultAsync(ld => ld.LoanId == id);
            if (loanDetail == null) throw new Exception("LoanDetail not found");

            loanDetail.IsDeleted = true;
            loanDetail.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

    }



}
