using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilSim.Models {
    public class Fireteam {
        public int FireteamId { get; set; }
        public string FireteamName { get; set; }
        public virtual ICollection<SteamUser> FireteamMembers { get; set; }


        public int OperationId { get; set; }
        public virtual Operation Operation { get; set; }
    }
}
