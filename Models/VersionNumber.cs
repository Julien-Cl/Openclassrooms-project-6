using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projet_6.Models;

namespace NexaWorksTickets.Models
{
    public class VersionNumber
    {
        [Key]
        public int Id { get; set; }
        public string VersionLabel { get; set; }

        // Foreign key vers Product
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Relations vers Configuration
        public ICollection<Configuration> Configurations { get; set; }
    }
}
