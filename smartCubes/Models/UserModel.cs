using System;
using System.Collections.ObjectModel;
using smartCubes.Enum;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<SessionModel> sessions { get; set; }
    }
}
