using ICR.Application.Services;
using ICR.Domain.Model.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICR.Infra.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ConnectionContext _context;
        private readonly IdSequenceService _idSequenceService;

        public MemberRepository(ConnectionContext context)
        {
            _context = context;
            _idSequenceService = new IdSequenceService(_context);
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members
                .Include(m => m.Family)
                .ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(long id)
        {
            return await _context.Members
                .Include(m => m.Family)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Member>> GetByFamilyAsync(long familyId)
        {
            return await _context.Members
                .Where(m => m.FamilyId == familyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetByChurchAsync(long churchId)
        {
            return await _context.Members
                .Include(m => m.Family)
                .Where(m => m.Family != null && m.Family.ChurchId == churchId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetByCellAsync(long cellId)
        {
            return await _context.Members
                .Include(m => m.Family)
                .Where(m => m.Family != null && m.Family.CellId == cellId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetBirthdaysByMonthAsync(int month, long churchId)
        {
            return await _context.Members
                .Include(m => m.Family)
                .Where(m =>
                    m.BirthDate.Month == month &&
                    m.Family != null &&
                    m.Family.ChurchId == churchId
                )
                .OrderBy(m => m.BirthDate.Day)
                .ToListAsync();
        }

        public async Task AddAsync(Member member)
        {
            var newId = await _idSequenceService.GetNextIdAsync<Member>();
            member.Id = newId;

            await _context.Members.AddAsync(member);
        }

        public async void UpdateAsync(Member member)
        {
            _context.Members.Update(member);
        }

        public async void RemoveAsync(Member member)
        {
            _context.Members.Remove(member);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
