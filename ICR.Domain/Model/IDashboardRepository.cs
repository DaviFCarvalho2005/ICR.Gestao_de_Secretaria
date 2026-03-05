using ICR.Domain.DTOs;
using System.Threading.Tasks;

namespace ICR.Domain.Model
{
    public interface IDashboardRepository
    {
        // Retorna um DTO com todos os totais para evitar múltiplas chamadas
        Task<DashboardStatsDTO> GetTotalStatsAsync();
    }
}