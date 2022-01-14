using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Type_Classes
{
    public class TypeWrapper
    {
        [JsonProperty("type")]
        public Type Type { get; set; }
    }
}
