using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilSim.Models
{
    public class Operation
    {
        public int OperationId { get; set; }
        [Required]
        public string OperationTitle { get; set; }
        [Required]
        public string OperationDescription { get; set; }
        public string OperationImageUrl { get; set; }
        public string OperationHost { get; set; }
        public string OperationTSserver { get; set; }
        public string OperationTSserverPassword { get; set; }
        public DateTime OperationBeginDateTime { get; set; }
        public List<string> OperationRepos { get; set; }

        public virtual ICollection<Fireteam> Fireteams { get; set; }
    }
}
