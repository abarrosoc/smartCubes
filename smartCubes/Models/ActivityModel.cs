using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace smartCubes.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<DeviceModel> Devices { get; set; }
    }
}

