using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model.Model.Ability_Classes
{
    public class Ability
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public Ability (string name)
        {
            Name = name;
        }
    }

}
