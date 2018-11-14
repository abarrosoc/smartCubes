using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace smartCubes.Models
{
    public class DeviceModel
    {

            [JsonProperty("id")]
            public long Id { get; set; }
            [JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("state")]
            public String State { get; set; }
            [JsonProperty("uid")]
            public String Uid { get; set; }

        public DeviceModel(long id, String name, String state, String uid)
        {
            Id = id;
            Name = name;
            State = state;
            Uid = uid;
        }
    }
}

