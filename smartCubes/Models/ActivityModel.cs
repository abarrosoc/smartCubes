using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace smartCubes.Models
{
    public class ActivityModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("devices")]
        public List<String> Devices { get; set; }

        public ActivityModel(long id, String name, List<String> devices)
        {
            Id = id;
            Name = name;
            Devices = devices;
        }
    }
}

