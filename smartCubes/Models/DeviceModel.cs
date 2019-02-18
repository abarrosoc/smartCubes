using System;
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

    }
}

