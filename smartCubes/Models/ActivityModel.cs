using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace smartCubes.Models
{
    public class ActivityModel<TResult>
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("devices")]
        public List<String> Devices { get; set; }
    }
}

