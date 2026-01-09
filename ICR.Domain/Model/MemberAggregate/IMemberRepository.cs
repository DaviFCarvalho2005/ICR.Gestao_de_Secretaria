using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICR.Domain.Model.MemberAggregate
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(long id);
        Task<IEnumerable<Member>> GetByChurchAsync(long churchId);
        Task<IEnumerable<Member>> GetByFamilyAsync(long familyId);
        Task<IEnumerable<Member>> GetBirthdaysByMonthAsync(int month, long churchId);

        Task AddAsync(Member member);
        void Update(Member member);
        void Remove(Member member);
    }
}
