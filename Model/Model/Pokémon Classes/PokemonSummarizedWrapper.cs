using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Pokémon_Classes
{
    public class PokemonSummarizedWrapper
    {
        [JsonProperty("pokemon")]
        public PokemonSummarized Pokemon { get; set; }
    }
}
