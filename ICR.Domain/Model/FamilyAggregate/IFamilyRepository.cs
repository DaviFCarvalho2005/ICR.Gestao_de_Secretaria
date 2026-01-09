using ICR.Domain.Model.MemberAggregate;
using System.Collections.Generic;
using ICR.Domain.Model.MemberAggregate;

namespace ICR.Domain.Model.FamilyAggregate
{
    public interface IFamilyRepository
    {
        void Add(Family family);
        Family? GetById(long id);
        List<Family> Get(int pageNumber, int pageQuantity);
        List<Family> GetFamiliesByWeddingBirthdayMonth(long memberId);

        List<Family> GetbyCellId(long cellid);
        void Delete(long id);
        void Save();

    }
}