using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Ability_Classes
{
    public class AbilityWrapper
    {
        [JsonProperty("ability")]
        public Ability Ability { get; set; }
    }
}
