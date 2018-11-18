using System;
using SQLite;

namespace smartCubes.Models
{
    public class SessionModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ActivityName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
