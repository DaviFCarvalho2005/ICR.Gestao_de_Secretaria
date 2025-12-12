using ICRManagement.Domain.Model.FederationAggregate;
using ICRManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICRManagement.Infra.Repositories
{
    public class FederationRepository : IFederationRepository
    {
        private readonly ConnectionContext _context;

        public FederationRepository(ConnectionContext context)
        {
            _context = context;
        }

        // Adiciona uma nova Federação
        public void Add(Federation federation)
        {
            if (federation == null) throw new ArgumentNullException(nameof(federation));

            _context.Set<Federation>().Add(federation);
            _context.SaveChanges();
        }

        // Retorna todas as federações paginadas
        public List<Federation> Get(int pageNumber, int pageQuantity)
        {
            return _context.Set<Federation>()
                .OrderBy(f => f.Name)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        // Retorna uma federação pelo Id
        public Federation? Get(string id)
        {
            return _context.Set<Federation>().Find(id);
        }

        // Atualiza nome da federação
        public void UpdateName(string federationId, string newName)
        {
            var federation = _context.Set<Federation>().Find(federationId);
            if (federation == null) throw new InvalidOperationException("Federation not found");

            federation.SetName(newName);
            _context.SaveChanges();
        }

        // Remove federação
        public void Delete(string federationId)
        {
            var federation = _context.Set<Federation>().Find(federationId);
            if (federation == null)
                throw new InvalidOperationException("Federation not found");

            _context.Set<Federation>().Remove(federation);
            _context.SaveChanges();
        }

        // Retorna próximo número sequencial do mês atual
        public int GetNextSequence()
        {
            var now = DateTime.UtcNow;
            var prefix = $"ICR{now:yyyyMM}-";

            var last = _context.Set<Federation>()
                .Where(f => f.Id.StartsWith(prefix))
                .OrderByDescending(f => f.Id)
                .FirstOrDefault();

            if (last == null) return 1;

            var lastNumber = int.Parse(last.Id.Split('-')[1]);
            return lastNumber + 1;
        }
    }
}
