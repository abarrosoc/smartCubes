using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace smartCubes.Models
{
    public class DeviceModel
    {
        public int ID { get; set; }
        public String Uuid { get; set; }
        public String Name { get; set; }
        public String State { get; set; }
        public List<FieldDevice> Fields { get; set; }
    }
}

