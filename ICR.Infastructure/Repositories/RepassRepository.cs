using ICR.Domain.Model.RepassAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Infra.Data.Repositories
{
    public class RepassRepository : IRepassRepository
    {
        private readonly ConnectionContext _context;

        public RepassRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Repass repass)
        {
            _context.Repasses.Add(repass);
        }

        public Repass? GetById(long id)
        {
            return _context.Repasses.Find(id);
        }

        public List<Repass> Get(int pageNumber, int pageQuantity)
        {
            return _context.Repasses
                .OrderBy(r => r.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public List<Repass> GetByChurchId(long churchId)
        {
            return _context.Repasses
                .Where(r => r.ChurchId == churchId)
                .ToList();
        }

        public List<Repass> GetByReference(long reference)
        {
            return _context.Repasses
                .Where(r => r.Reference == reference)
                .ToList();
        }

        public void Delete(long id)
        {
            var repass = GetById(id);
            if (repass != null)
                _context.Repasses.Remove(repass);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
