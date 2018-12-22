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
            database.CreateTable<UserModel>();
            database.CreateTable<SessionModel>();
            database.CreateTable<SessionData>();
            database.CreateTable<SessionInit>();
        }

        public SessionModel GetSession(int id)
        {
            return database.Table<SessionModel>().Where(i => i.ID == id).FirstOrDefault();
        }

        public List<SessionModel> GetSessions()
        {
            return database.Table<SessionModel>().ToList();
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

        public List<SessionModel> GetSessionsNotDone()
        {
            return database.Query<SessionModel>("SELECT * FROM [SessionModel] ");
        }

        public List<SessionModel> GetSessionsByUser(UserModel user)
        {
            return database.Table<SessionModel>().Where(i => i.UserID == user.ID).ToList();
        }

        public int DeleteSession(SessionModel item)
        {
            return database.Delete(item);
        }



        public List<UserModel> GetUsers()
        {
            return database.Table<UserModel>().ToList();
        }

        public UserModel GetUser(int id)
        {
            return database.Table<UserModel>().Where(i => i.ID == id).FirstOrDefault();
        }
        public UserModel GetUser(string userName)
        {
            return database.Table<UserModel>().Where(i => i.UserName == userName).FirstOrDefault();
        }

        public List<UserModel> GetItemsNotDone()
        {
            return database.Query<UserModel>("SELECT * FROM [UserModel] ");
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

        public int DeleteUser(UserModel item)
        {
            return database.Delete(item);
        }





        public int DeleteSessionData(SessionData item)
        {
            return database.Delete(item);
        }

        public List<SessionData> GetSessionDataNotDone()
        {
            return database.Query<SessionData>("SELECT * FROM [SessionData] ");
        }

        public List<SessionData> GetSessionData()
        {
            return database.Table<SessionData>().ToList();
        }


        public int SaveSessionData(SessionData item)
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



        public int DeleteSessionInit(SessionInit item)
        {
            return database.Delete(item);
        }

        public List<SessionInit> GetSessionInitNotDone()
        {
            return database.Query<SessionInit>("SELECT * FROM [SessionInit] ");
        }

        public List<SessionInit> GetSessionInit()
        {
            return database.Table<SessionInit>().ToList();
        }
        public int SaveSessionInit(SessionInit item)
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



    }
}
