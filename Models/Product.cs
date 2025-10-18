using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projet_6.Models;

namespace NexaWorksTickets.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }

        // Relations
        public ICollection<VersionNumber> Versions { get; set; }
    }
}
