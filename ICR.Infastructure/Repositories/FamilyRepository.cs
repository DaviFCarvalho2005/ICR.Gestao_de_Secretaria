using ICR.Domain.Model.FamilyAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Infra.Data.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly ConnectionContext _context;

        public FamilyRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Family family)
        {
            _context.Families.Add(family);
        }

        public Family? GetById(long id)
        {
            return _context.Families
                .FirstOrDefault(f => f.Id == id);
        }


        public List<Family> Get(int pageNumber, int pageQuantity)
        {
            return _context.Families
                .OrderBy(f => f.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public List<Family> GetFamiliesByWeddingBirthdayMonth(long memberId)
        {
            var member = _context.Members
                .Where(m => m.Id == memberId)
                .Select(m => m.BirthDate)
                .FirstOrDefault();

            if (member == default)
                return new List<Family>();

            var month = member.Month;

            return _context.Families
                .Where(f => f.WeddingDate != null &&
                            f.WeddingDate.Value.Month == month)
                .ToList();
        }

        public List<Family> GetbyCellId(long cellid)
        {
            return _context.Families
                .Where(f => f.CellId == cellid)
                .ToList();
        }

        public void Delete(long id)
        {
            var family = GetById(id);
            if (family != null)
                _context.Families.Remove(family);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
