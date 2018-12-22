using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    [Table("SessionData")]
    public class SessionData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(SessionInit))] 
        public int SessionInitId { get; set; }
        public String StudentCode { get; set; }
        public String DeviceName { get; set; }
        public String Data { get; set; }
    }
}
