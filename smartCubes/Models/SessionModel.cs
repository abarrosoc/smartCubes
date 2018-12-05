using System;
using System.Collections.Generic;
using SQLite;

namespace smartCubes.Models
{
    [Table("Sessions")]
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
