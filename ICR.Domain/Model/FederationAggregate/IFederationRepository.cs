using ICRManagement.Domain.Model.FederationAggregate;
using System.Collections.Generic;

namespace ICRManagement.Domain.Repositories
{
    public interface IFederationRepository
    {
        // Adiciona uma nova federação
        void Add(Federation federation);

        // Retorna todas as federações paginadas
        List<Federation> Get(int pageNumber, int pageQuantity);

        // Retorna uma federação pelo Id
        Federation? Get(string id);

        // Atualiza nome de uma federação
        void UpdateName(string federationId, string newName);

        // Remove uma federação
        void Delete(string federationId);

        // Retorna o próximo número sequencial do mês atual
        int GetNextSequence();
    }
}
