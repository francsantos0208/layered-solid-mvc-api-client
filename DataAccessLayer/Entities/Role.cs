using System.ComponentModel;
using Newtonsoft.Json;

namespace DataAccessLayer.Entities
{
    public class Role
    {
        [DefaultValue("")]
        [JsonProperty("name", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Populate)]
        public string RoleName { get; set; }

        [DefaultValue("")]
        [JsonProperty("actor", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Populate)]
        public string ActorName { get; set; }
    }
}