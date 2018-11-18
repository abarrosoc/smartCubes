using System.Collections.Generic;
using smartCubes.Models;
using SQLite;

namespace smartCubes.Data
{
    public class Database
    {
        readonly SQLiteConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
           // database.CreateTable<UserModel>();
            database.CreateTable<SessionModel>();
        }

        public List<UserModel> GetUsers()
        {
            return database.Table<UserModel>().ToList();
        }
        public List<SessionModel> GetSessions()
        {
            return database.Table<SessionModel>().ToList();
        }

        public List<UserModel> GetItemsNotDone()
        {
            return database.Query<UserModel>("SELECT * FROM [UserModel] ");
        }
        public List<SessionModel> GetSessionsNotDone()
        {
            return database.Query<SessionModel>("SELECT * FROM [SessionModel] ");
        }

        public UserModel GetUser(int id)
        {
            return database.Table<UserModel>().Where(i => i.ID == id).FirstOrDefault();
        }
        public SessionModel GetSession(int id)
        {
            return database.Table<SessionModel>().Where(i => i.ID == id).FirstOrDefault();
        }

        public int SaveUser(UserModel item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        public int SaveSession(SessionModel item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int DeleteUser(UserModel item)
        {
            return database.Delete(item);
        }
        public int DeleteSession(SessionModel item)
        {
            return database.Delete(item);
        }
    }
}
