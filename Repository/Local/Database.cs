using Microsoft.Data.Sqlite;
using Model.Model.Pokémon_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Repository.Local
{
    public class Database
    { 
        public async static Task InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("pokedex.db", CreationCollisionOption.OpenIfExists);
            string databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "pokedex.db");

            using (SqliteConnection connection = new SqliteConnection($"Filename={databasePath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand("PRAGMA foreign_keys = ON", connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand("CREATE TABLE IF NOT EXISTS pokemon (" +
                    "id INTEGER NOT NULL UNIQUE PRIMARY KEY, " +
                    "name NVARCHAR(50) NOT NULL UNIQUE," +
                    "base_experience INTEGER NOT NULL," +
                    "height INTEGER NOT NULL," +
                    "weight INTEGER NOT NULL," +
                    "hp INTEGER NOT NULL," +
                    "attack INTEGER NOT NULL," +
                    "defense INTEGER NOT NULL," +
                    "special_attack INTEGER NOT NULL," +
                    "special_defense INTEGER NOT NULL," +
                    "speed INTEGER NOT NULL)", connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand("CREATE TABLE IF NOT EXISTS type_tb (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "pokemon_id INTEGER REFERENCES pokemon(id) ON DELETE CASCADE," +
                    "type NVARCHAR(50) NOT NULL)", connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand("CREATE TABLE IF NOT EXISTS ability_tb (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "pokemon_id INTEGER REFERENCES pokemon(id) ON DELETE CASCADE," +
                    "ability NVARCHAR(50) NOT NULL)", connection);
                command.ExecuteNonQuery();

                TypeDatabase.PathDatabase = databasePath;
                PokemonDatabase.PathDatabase = databasePath;
                AbilityDatabase.PathDatabase = databasePath;
                
                connection.Close();
            }
        }

        public static bool InsertPokemon(Pokemon pokemon)
        {
          return PokemonDatabase.InsertPokemon(pokemon);
        }

        public static void InsertPokemonType(int pokemonID, string type)
        {
            TypeDatabase.InsertPokemonType(pokemonID, type);
        }

        public static void InsertPokemonAbility(int pokemonID, string ability)
        {
            AbilityDatabase.InsertPokemonAbility(pokemonID, ability);
        }

        public static Pokemon FindPokemonByID(int ID)
        {
           return PokemonDatabase.FindPokemonByID(ID);
        }

        public static List<Pokemon> FindPokemonByName(string pokemonName)
        {
           return PokemonDatabase.FindPokemonByName(pokemonName);
        }

        public static List<Pokemon> FindPokemonByType(string pokemonType)
        {
            return PokemonDatabase.FindPokemonByType(pokemonType);
        }

        public static List<Pokemon> GetTenPokemon(int page)
        {
            return PokemonDatabase.GetTenPokemon(page);
        }

        public static int AmountOfPokemonDatabase()
        {
            return PokemonDatabase.AmountOfPokemonDatabase(); 
        }
    }
}
