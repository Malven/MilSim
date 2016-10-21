using MilSim.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MilSim.Models {

    public class Player {
        
        [Key]
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
    }

    public class Response {
        public IList<Player> players { get; set; }
    }

    public class SteamUser {
        public Response response { get; set; }
    }

    public class SteamFactory {

        public static HttpClient _client = new HttpClient();
        private readonly string ApiKey = "1678D429F67B9AC88EC696082AFE5F06";
        private readonly string location = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=1678D429F67B9AC88EC696082AFE5F06&steamids=";

        public string GetSteamUser(string steamid ) {
            HttpRequestMessage getMessage = new HttpRequestMessage();
            getMessage.RequestUri = new Uri( location+steamid );
            getMessage.Method = HttpMethod.Get;

            var getResponse = _client.SendAsync( getMessage ).Result;
            var getResponseBody = getResponse.Content.ReadAsStringAsync().Result;

            return getResponseBody;
        }

        public void UpdateSteamUser(string steamid, ApplicationDbContext context ) {
            var response = GetSteamUser( steamid);
            JObject steamUser = JsonConvert.DeserializeObject( response ) as JObject;
            var temp = steamUser.GetValue( "response" ).First;
            var t = temp.First;
            var tr = t.First;
            Player s = JsonConvert.DeserializeObject<Player>( t.First.ToString() );
            context.Steam.Update( s );
            context.SaveChangesAsync();
        }
    }
}
