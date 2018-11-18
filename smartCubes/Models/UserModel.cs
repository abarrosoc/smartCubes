using System;
using SQLite;
using Xamarin.Forms;

namespace smartCubes.Models
{
    public class UserModel
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
