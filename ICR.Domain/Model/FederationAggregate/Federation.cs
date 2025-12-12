using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICRManagement.Domain.Model.FederationAggregate
{
    [Table("federation")]
    public class Federation
    {
        [Key]
        public string Id { get; private set; } // Identificador único da comissão

        public string Name { get; private set; } // Nome da comissão federada

        // Construtor principal
        public Federation(string name, string id)
        {
            Id= id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        private string GenerateCustomId(int sequenceNumber)
        {
            // Pega ano e mês atual UTC
            var now = DateTime.UtcNow;
            return $"ICR{now:yyyyMM}-{sequenceNumber}";
        }
        // Métodos para atualizar campos
        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty", nameof(newName));

            Name = newName;
        }

    }
}
