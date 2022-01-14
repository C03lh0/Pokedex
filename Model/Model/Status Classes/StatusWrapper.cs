using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Status_Classes
{
    public class StatusWrapper
    {
        [JsonProperty("base_stat")]
        public int Value { get; set; }

        [JsonProperty("stat")]
        public StatusInfo StatusInfo { get; set; }
    }
}
