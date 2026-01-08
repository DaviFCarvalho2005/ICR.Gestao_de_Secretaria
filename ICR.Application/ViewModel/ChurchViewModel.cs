using ICR.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Application.ViewModel
{
    public class ChurchViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public long FederationId { get; set; }
        public long? MinisterId { get; set; }

    }
}
