using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Pokémon_Classes
{
    public class PokemonSummarizedList
    {
        [JsonProperty("pokemon")]
        public List<PokemonSummarizedWrapper> PokemonList { get; set; }
    }
}
