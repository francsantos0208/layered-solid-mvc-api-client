using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Movie
    {
        [JsonProperty("name")]
        public string MovieName { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<Role> Roles { get; set; }
    }
}
