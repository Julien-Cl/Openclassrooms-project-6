using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projet_6.Models;

namespace NexaWorksTickets.Models
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }

        // Foreign keys
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("VersionNumber")]
        public int VersionId { get; set; }
        public VersionNumber VersionNumber { get; set; }

        [ForeignKey("OpSystem")]
        public int OpSystemId { get; set; }
        public OperatingSystem OpSystem { get; set; }

        // Relation vers Ticket df
        public ICollection<Ticket> Tickets { get; set; }
    }
}
