using ICR.Domain.Model.MinisterAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Infra.Data.Repositories
{
    public class MinisterRepository : IMinisterRepository
    {
        private readonly ConnectionContext _context;

        public MinisterRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Minister minister)
        {
            _context.Ministers.Add(minister);
        }

        public Minister? GetById(long id)
        {
            return _context.Ministers.Find(id);
        }

        public List<Minister> Get(int pageNumber, int pageQuantity)
        {
            return _context.Ministers
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public List<Minister> GetByMemberId(long memberId)
        {
            return _context.Ministers
                .Where(m => m.MemberId == memberId)
                .ToList();
        }

        public List<Minister> GetByFamilyId(long familyId)
        {
            return (from minister in _context.Ministers
                    join member in _context.Members
                        on minister.MemberId equals member.Id
                    where member.FamilyId == familyId
                    select minister)
                   .ToList();
        }


        public Minister? GetByCpf(long cpf)
        {
            return _context.Ministers
                .FirstOrDefault(m => m.Cpf == cpf);
        }

        public List<Minister> GetByChurchId(long churchId)
        {
            return (from minister in _context.Ministers
                    join member in _context.Members
                        on minister.MemberId equals member.Id
                    where member.ChurchId == churchId
                    select minister)
                   .ToList();
        }

        public void Delete(long id)
        {
            var minister = GetById(id);
            if (minister != null)
                _context.Ministers.Remove(minister);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
