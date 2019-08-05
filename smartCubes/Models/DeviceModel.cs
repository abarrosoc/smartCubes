using System;

namespace smartCubes.Models
{
    public class DeviceModel
    {
        public int ID { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Service { get; set; }
        public string Characteristic { get; set; }
    }
}

