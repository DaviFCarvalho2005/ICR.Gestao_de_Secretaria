using ICR.Domain.DTOs;
using ICR.Domain.Model.MemberAggregate;

using System.Collections.Generic;

namespace ICR.Domain.Model.CellAggregate
{
    public interface ICellRepository
    {
        Task AddAsync(Cell cell);

        Task<CellResponseDTO?> GetByIdAsync(long id);

        Task<List<CellResponseDTO>> GetAsync(int pageNumber, int pageQuantity);

        Task<List<CellResponseDTO>> GetByChurchIdAsync(Member leader);

        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(long id, Cell updatedCell);

        Task SaveAsync();
    }

}