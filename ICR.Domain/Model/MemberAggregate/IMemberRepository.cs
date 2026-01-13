using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICR.Domain.Model.MemberAggregate
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member?> GetByIdAsync(long id);
        Task<IEnumerable<Member>> GetByFamilyAsync(long familyId);
        Task<IEnumerable<Member>> GetByChurchAsync(long churchId);
        Task<IEnumerable<Member>> GetByCellAsync(long cellId);

        Task<IEnumerable<Member>> GetBirthdaysByMonthAsync(int month, long churchId);

        Task AddAsync(Member member);
        void UpdateAsync(Member member);
        void RemoveAsync(Member member);

        Task SaveAsync();
    }
}

