using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Domain.Model.UserRoleAgreggate
{
    [Table("roles")]
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
    

    public Role() { }

        public Role(long id, string name, string? description, bool active)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Active = active;
        }

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty", nameof(newName));
            Name = newName;
        }
        public void SetDescription(string newDescription) {
            Description = newDescription;
        }
        public void SetActive(bool isActive) {
            Active = isActive;
        }

    } }
