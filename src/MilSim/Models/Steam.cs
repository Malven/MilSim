using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilSim.Models
{
    public class Steam
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int lastlogoff { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
        public string gameextrainfo { get; set; }
        public string gameid { get; set; }
        public string loccountrycode { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
