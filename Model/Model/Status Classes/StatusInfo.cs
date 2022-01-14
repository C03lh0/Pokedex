using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Status_Classes
{
    public class StatusInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
