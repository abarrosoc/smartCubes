using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace smartCubes.Models
{
    [Table("Users")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}