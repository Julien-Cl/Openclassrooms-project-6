using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projet_6.Models;

namespace NexaWorksTickets.Models
{
    public class OpSystem
    {
        [Key]
        public int Id { get; set; }
        public string SystemName { get; set; }

        public ICollection<Configuration> Configurations { get; set; }
    }
}
