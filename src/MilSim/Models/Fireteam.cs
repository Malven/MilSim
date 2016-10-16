using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilSim.Models
{
    public class Fireteam
    {
        public int FireteamId { get; set; }
        public string FireteamName { get; set; }
        public string Member1 { get; set; }
        public string Member2 { get; set; }
        public string Member3 { get; set; }
        public string Member4 { get; set; }
        public string Member5 { get; set; }


        public int OperationId { get; set; }
        public virtual Operation Operation { get; set; }
    }
}
