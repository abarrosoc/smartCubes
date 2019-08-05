﻿using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<SessionModel> Sessions { get; set; }
    }
}
