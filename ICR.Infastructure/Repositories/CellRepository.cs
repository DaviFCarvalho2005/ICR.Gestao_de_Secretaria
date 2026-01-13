using ICR.Application.Services;
using ICR.Domain.DTOs;
using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICR.Infra.Data.Repositories
{
    public class CellRepository : ICellRepository
    {
        private readonly ConnectionContext _context;
        private readonly IdSequenceService _idSequenceService;

        public CellRepository(ConnectionContext context)
        {
            _context = context;
            _idSequenceService = new IdSequenceService(context);
        }

        public async Task AddAsync(Cell cell)
        {
            var newId = await _idSequenceService.GetNextIdAsync<Cell>();
            cell.Id = newId;

            await _context.Cells.AddAsync(cell);
        }

        public async Task<CellResponseDTO?> GetByIdAsync(long id)
        {
            return await _context.Cells
                .Include(c => c.Church)
                .Include(c => c.Responsible)
                .Where(c => c.Id == id)
                .Select(c => new CellResponseDTO(
                    c.Id,
                    c.Name,
                    c.ChurchId,
                    c.Church.Name,
                    c.ResponsibleId,
                    c.Responsible != null ? c.Responsible.Name : null
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<List<CellResponseDTO>> GetAsync(int pageNumber, int pageQuantity)
        {
            return await _context.Cells
                .Include(c => c.Church)
                .Include(c => c.Responsible)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(c => new CellResponseDTO(
                    c.Id,
                    c.Name,
                    c.ChurchId,
                    c.Church.Name,
                    c.ResponsibleId,
                    c.Responsible != null ? c.Responsible.Name : null
                ))
                .ToListAsync();
        }

        public async Task<List<CellResponseDTO>> GetByChurchIdAsync(Member leader)
        {
            return await _context.Cells
                .Include(c => c.Church)
                .Include(c => c.Responsible)
                .Where(c => c.Church.MinisterId == leader.Id)
                .Select(c => new CellResponseDTO(
                    c.Id,
                    c.Name,
                    c.ChurchId,
                    c.Church.Name,
                    c.ResponsibleId,
                    c.Responsible != null ? c.Responsible.Name : null
                ))
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(long id, Cell updatedCell)
        {
            var cell = await _context.Cells.FirstOrDefaultAsync(c => c.Id == id);
            if (cell == null)
                return false;
            
            cell.SetName(updatedCell.Name);
            cell.SetChurch(updatedCell.ChurchId);
            cell.SetResponsible(updatedCell.ResponsibleId ?? 0);

            _context.Cells.Update(cell);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var cell = await _context.Cells.FirstOrDefaultAsync(c => c.Id == id);
            if (cell == null)
                return false;

            _context.Cells.Remove(cell);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
