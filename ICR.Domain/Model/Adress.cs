using System;
using System.Collections.Generic;
using System.Text;
namespace ICR.Domain.Model
{
    public class Address
    {
        public long ZipCode { get; set; }    // CEP
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }       // Cidade
        public string State { get; set; }      // Estado
    

    protected Address() { }
        public Address(string street, int number, string city, string state, long zipCode)
        {
            ZipCode = zipCode;
            Street = street ?? throw new ArgumentNullException(nameof(street));
            Number = number;
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
    }
}


