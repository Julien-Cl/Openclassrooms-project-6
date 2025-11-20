using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexaWorksTickets.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusName { get; set; }

        // Relation inverse vers Ticket
        public ICollection<Ticket> Tickets { get; set; }
    }
}


