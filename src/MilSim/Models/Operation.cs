using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilSim.Models
{
    public class Operation
    {
        //public Operation() {
        //    Fireteams = new List<Fireteam>();
        //}

        public int OperationId { get; set; }
        [Required]
        public string OperationTitle { get; set; }
        [Required]
        public string OperationDescription { get; set; }

        public virtual ICollection<Fireteam> Fireteams { get; set; }
    }
}
