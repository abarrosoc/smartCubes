using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    [Table("Sessions")]
    public class SessionModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActivityName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        [ForeignKey(typeof(UserModel))] 
        public int UserID { get; set; }
    }
}
