using Model.Model.Ability_Classes;
using Model.Model.Status_Classes;
using Model.Model.Type_Classes;
using Type = Model.Model.Type_Classes.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Model.Model.Pokémon_Classes
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("base_experience")]
        public int Experience { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("types")]
        public List<TypeWrapper> Types { get; set; }

        [JsonProperty("abilities")]
        public List<AbilityWrapper> Abilities { get; set; }

        [JsonProperty("stats")]
        public List<StatusWrapper> StatusList { get; set; }

        public Status Status { get; set; }

        public string ImageFilePath { get; set; }
        public List<Type> StringTypes { get { return StringTypeList(); } }
        private List<Type> StringTypeList()
        {
            List<Type> typeList = new List<Type>();
            foreach (var type in Types)
            {
                typeList.Add(type.Type);
            }
            return typeList;
        }

        public Pokemon()
        {
        }

        public Pokemon(int id, string name, int experience, int height, int weight, int hp, int attack, int defense,
            int specialAttack, int specialDefense, int speed)
        {
            ID = id;
            Name = name;
            Experience = experience;
            Height = height;
            Weight = weight;
            Status = new Status
            {
                HP = hp,
                Attack = attack,
                Defense = defense,
                SpecialAttack = specialAttack,
                SpecialDefense = specialDefense,
                Speed = speed
            };
        }

        public override string ToString()
        {
            return $"{ID}";
        }
    }   
}
