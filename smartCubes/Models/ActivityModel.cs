using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace smartCubes.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DeviceModel> Devices { get; set; }
        public List<MessageDevice> Messages { get; set; }

    }
}

