using Microsoft.Data.Sqlite;
using Model.Model.Pokémon_Classes;
using Model.Model.Status_Classes;
using System.Collections.Generic;

namespace Repository.Local
{
    public class PokemonDatabase
    {
        public static string PathDatabase { get; set; }

        public static bool InsertPokemon(Pokemon pokemon)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand insertCommand = new SqliteCommand("INSERT OR IGNORE INTO pokemon VALUES (@id, @name," +
                    "@experience, @height, @weight, @hp, @attack, @defense, @special_attack, @special_defense, @speed)", connection);

                insertCommand.Parameters.AddWithValue("@id", pokemon.ID);
                insertCommand.Parameters.AddWithValue("@name", pokemon.Name);
                insertCommand.Parameters.AddWithValue("@experience", pokemon.Experience);
                insertCommand.Parameters.AddWithValue("@height", pokemon.Height);
                insertCommand.Parameters.AddWithValue("@weight", pokemon.Weight);
                insertCommand.Parameters.AddWithValue("@hp", pokemon.Status.HP);
                insertCommand.Parameters.AddWithValue("@attack", pokemon.Status.Attack);
                insertCommand.Parameters.AddWithValue("@defense", pokemon.Status.Defense);
                insertCommand.Parameters.AddWithValue("@special_attack", pokemon.Status.SpecialAttack);
                insertCommand.Parameters.AddWithValue("@special_defense", pokemon.Status.SpecialDefense);
                insertCommand.Parameters.AddWithValue("@speed", pokemon.Status.Speed);

                bool isPokemonInserted = insertCommand.ExecuteNonQuery() != 0;
                connection.Close();

                return isPokemonInserted;
            }
        }

        public static Pokemon FindPokemonByID(int ID)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM pokemon WHERE id = @value", connection);
                selectCommand.Parameters.AddWithValue("@value", ID);
                SqliteDataReader queryResponse = selectCommand.ExecuteReader();

                if (queryResponse.HasRows)
                {
                    queryResponse.Read();

                    Pokemon pokemon = new Pokemon
                    {
                        ID = queryResponse.GetInt32(0),
                        Name = queryResponse.GetString(1),
                        Experience = queryResponse.GetInt32(2),
                        Height = queryResponse.GetInt32(3),
                        Weight = queryResponse.GetInt32(4),
                        Status = new Status
                        {
                            HP = queryResponse.GetInt32(5),
                            Attack = queryResponse.GetInt32(6),
                            Defense = queryResponse.GetInt32(7),
                            SpecialAttack = queryResponse.GetInt32(8),
                            SpecialDefense = queryResponse.GetInt32(9),
                            Speed = queryResponse.GetInt32(10)
                        }
                    };

                    queryResponse.Close();

                    pokemon.Types = TypeDatabase.FindPokemonTypeByID(ID);
                    pokemon.Abilities = AbilityDatabase.FindPokemonAbilityByID(ID);

                    return pokemon;
                }
            }
            return null;
        }

        public static List<Pokemon> FindPokemonByName(string pokemonName)
        {
            List<Pokemon> pokemonsList = new List<Pokemon>();
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();
                SqliteCommand selectCommand = new SqliteCommand($"SELECT * FROM pokemon WHERE name LIKE '{pokemonName}%'", connection);
                SqliteDataReader queryResponse = selectCommand.ExecuteReader();

                while (queryResponse.Read())
                {
                    Pokemon wantedPokemon = new Pokemon
                    {
                        ID = queryResponse.GetInt32(0),
                        Name = queryResponse.GetString(1),
                        Experience = queryResponse.GetInt32(2),
                        Height = queryResponse.GetInt32(3),
                        Weight = queryResponse.GetInt32(4),
                        Status = new Status
                        {
                            HP = queryResponse.GetInt32(5),
                            Attack = queryResponse.GetInt32(6),
                            Defense = queryResponse.GetInt32(7),
                            SpecialAttack = queryResponse.GetInt32(8),
                            SpecialDefense = queryResponse.GetInt32(9),
                            Speed = queryResponse.GetInt32(10)
                        }
                    };
                    wantedPokemon.Types = TypeDatabase.FindPokemonTypeByID(wantedPokemon.ID);
                    wantedPokemon.Abilities = AbilityDatabase.FindPokemonAbilityByID(wantedPokemon.ID);
                    pokemonsList.Add(wantedPokemon);
                }
                queryResponse.Close();

            }
            return pokemonsList;
        }

        public static List<Pokemon> FindPokemonByType(string pokemonType)
        {
            List<Pokemon> pokemonListWithTheSameType = new List<Pokemon>();
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();
                SqliteCommand selectComand = new SqliteCommand("SELECT pokemon_id FROM type_tb WHERE type = @value ", connection);

                selectComand.Parameters.AddWithValue("@value", pokemonType);
                SqliteDataReader queryResponse = selectComand.ExecuteReader();

                while (queryResponse.Read())
                {
                    int pokemonId = queryResponse.GetInt32(0);

                    SqliteCommand selectComandID = new SqliteCommand("SELECT * FROM pokemon WHERE id = @valueId", connection);
                    selectComandID.Parameters.AddWithValue("@valueId", pokemonId);
                    SqliteDataReader queryResponseID = selectComandID.ExecuteReader();

                    queryResponseID.Read();

                    Pokemon wantedPokemon = new Pokemon
                    {
                        ID = queryResponse.GetInt32(0),
                        Name = queryResponse.GetString(1),
                        Experience = queryResponse.GetInt32(2),
                        Height = queryResponse.GetInt32(3),
                        Weight = queryResponse.GetInt32(4),
                        Status = new Status
                        {
                            HP = queryResponse.GetInt32(5),
                            Attack = queryResponse.GetInt32(6),
                            Defense = queryResponse.GetInt32(7),
                            SpecialAttack = queryResponse.GetInt32(8),
                            SpecialDefense = queryResponse.GetInt32(9),
                            Speed = queryResponse.GetInt32(10)
                        }
                    };

                    wantedPokemon.Types = TypeDatabase.FindPokemonTypeByID(wantedPokemon.ID);
                    wantedPokemon.Abilities = AbilityDatabase.FindPokemonAbilityByID(wantedPokemon.ID);

                    pokemonListWithTheSameType.Add(wantedPokemon);

                    queryResponseID.Close();
                }
                queryResponse.Close();
                return pokemonListWithTheSameType;
            }
        }

        public static List<Pokemon> GetTenPokemon(int page)
        {
            int offsetCount = page * 10;
            List<Pokemon> pokemonList = new List<Pokemon>();

            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();
                SqliteCommand selectComand = null;

                if (page < 25)
                {
                    selectComand = new SqliteCommand($"SELECT * FROM pokemon WHERE id < 251 ORDER BY id LIMIT 10 OFFSET {offsetCount}", connection);
                }
                else
                {
                    selectComand = new SqliteCommand($"SELECT * FROM pokemon  WHERE id > 250 ORDER BY id LIMIT 10", connection);
                }

                SqliteDataReader queryResponse = selectComand.ExecuteReader();

                while (queryResponse.Read())
                {
                    Pokemon pokemon = new Pokemon
                    {
                        ID = queryResponse.GetInt32(0),
                        Name = queryResponse.GetString(1),
                        Experience = queryResponse.GetInt32(2),
                        Height = queryResponse.GetInt32(3),
                        Weight = queryResponse.GetInt32(4),
                        Status = new Status
                        {
                            HP = queryResponse.GetInt32(5),
                            Attack = queryResponse.GetInt32(6),
                            Defense = queryResponse.GetInt32(7),
                            SpecialAttack = queryResponse.GetInt32(8),
                            SpecialDefense = queryResponse.GetInt32(9),
                            Speed = queryResponse.GetInt32(10)
                        }
                    };

                    pokemon.Types = TypeDatabase.FindPokemonTypeByID(pokemon.ID);
                    pokemon.Abilities = AbilityDatabase.FindPokemonAbilityByID(pokemon.ID);
                    pokemonList.Add(pokemon);
          
                   
                }
                queryResponse.Close();
                return pokemonList;
            }
        }

        public static int AmountOfPokemonDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                int response = 0;
                connection.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT count(id) FROM pokemon", connection);
                SqliteDataReader queryResponse = selectCommand.ExecuteReader();
                if (queryResponse.Read())
                {
                    response = queryResponse.GetInt32(0);
                }
                return response;
            }
        }
    }
}
