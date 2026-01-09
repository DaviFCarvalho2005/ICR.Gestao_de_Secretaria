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

        public MemberRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(long id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<IEnumerable<Member>> GetByChurchAsync(long churchId)
        {
            return await _context.Members
                .Where(m => m.ChurchId == churchId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetByFamilyAsync(long familyId)
        {
            return await _context.Members
                .Where(m => m.FamilyId == familyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetBirthdaysByMonthAsync(int month, long churchId)
        {
            return await _context.Members
                .Where(m => m.BirthDate.Month == month && m.ChurchId == churchId)
                .ToListAsync();
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }

        public void Update(Member member)
        {
            _context.Members.Update(member);
        }

        public void Remove(Member member)
        {
            _context.Members.Remove(member);
        }
    }
}
