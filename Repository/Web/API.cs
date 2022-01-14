using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Model.Model.Pokémon_Classes;
using Repository.Local;

namespace Repository.Web
{
    public class API
    {
        private const string _pokedexAPI = "https://pokeapi.co/api/v2/";

        public static async Task<Pokemon> ConsumeAPIByIDAsync(int pokemonID)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_pokedexAPI) })
            {
                HttpResponseMessage response = await client.GetAsync($"pokemon/{pokemonID}");
                string responseBody = await response.Content.ReadAsStringAsync();
                Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(responseBody);

                return pokemon;
            }
        }
        public static async Task<Pokemon> ConsumeAPIByNameAsync(string pokemonName)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_pokedexAPI) })
            {
                HttpResponseMessage response = await client.GetAsync($"pokemon/{pokemonName}");
                string responseBody = await response.Content.ReadAsStringAsync();
                Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(responseBody);

                return pokemon;
            }
        }

        public static async Task<PokemonSummarizedList> ConsumeAPIByTypeAsync(string pokemonType)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_pokedexAPI) })
            {
                HttpResponseMessage response = await client.GetAsync($"type/{pokemonType}");
                string responseBody = await response.Content.ReadAsStringAsync();
                PokemonSummarizedList pokemonList = JsonConvert.DeserializeObject<PokemonSummarizedList>(responseBody);

                return pokemonList;
            }
        }
    }
}
