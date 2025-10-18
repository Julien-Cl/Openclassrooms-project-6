using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexaWorksTickets.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string Status { get; set; } // "Résolu" ou "En cours"
        public string Description { get; set; }
        public string Resolution { get; set; }

        // Foreign key vers Configuration
        [ForeignKey("Configuration")]
        public int ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }
    }
}


