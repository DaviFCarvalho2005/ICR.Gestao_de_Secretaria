using System;
using System.Collections.Generic;
using System.Text;
using ICR.Domain.Model;
using Microsoft.EntityFrameworkCore;
using ICR.Infra;
namespace ICR.Application.Services
{
    public class IdSequenceService
    {
        private readonly ConnectionContext _context;

        public IdSequenceService(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<long> GetNextIdAsync<T>() where T : class, BasicModel
        {
            // Abre a transação async
            await using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            var now = DateTime.UtcNow;
            var prefix = long.Parse($"{now:yyyyMM}");
            var min = prefix * 1000;
            var max = min + 999;

            // Busca o último ID usando FirstOrDefaultAsync
            var last = await _context.Set<T>()
                .Where(x => x.Id >= min && x.Id <= max)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var next = last == 0 ? min + 1 : last + 1;

            if (next > max)
                throw new InvalidOperationException("ID sequence overflow for current month.");

            await transaction.CommitAsync();
            return next;
        }
    }
}
