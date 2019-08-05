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
        public string DeviceName { get; set; }
        public byte[] Data { get; set; }
    }
}
