using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    [Table("SessionInit")]
    public class SessionInit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(SessionModel))] 
        public int SessionId { get; set; }
        public String StudentCode { get; set; }
        public String Time { get; set; }
        public DateTime Date { get; set; }
    }
}
