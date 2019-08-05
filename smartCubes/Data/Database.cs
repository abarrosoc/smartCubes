using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.Utils;
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
        public void ResetDataBase(){
            database.DropTable<UserModel>();
            database.DropTable<SessionModel>();
            database.DropTable<SessionData>();
            database.DropTable<SessionInit>();

            database.CreateTable<UserModel>();
            database.CreateTable<SessionModel>();
            database.CreateTable<SessionData>();
            database.CreateTable<SessionInit>();

            UserModel user = new UserModel();
            user.UserName = "Admin";
            user.Password = Crypt.Encrypt("Admin", "uah2019");
            user.Role = "Administrador";
            user.Email = "admin@uah.es";
            SaveUser(user);
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
            List<SessionInit> lSessionInit = GetSessionInit(item.ID);

            List<SessionData> lSessionData = new List<SessionData>();

            foreach (SessionInit sessionInit in lSessionInit)
                lSessionData.AddRange(GetSessionData(sessionInit.ID));

            DeleteSessionDataById(lSessionData);
            DeleteSessionInitById(lSessionInit);
            return database.Delete(item);
        }


        /*---------------------------------------------------------------------
         * 
         *                              USERS
         * 
         ----------------------------------------------------------------------*/
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
        public void DeleteSessionDataById(List<SessionData> items)
        {
            foreach (SessionData sessionData in items)
                database.Query<SessionData>("DELETE FROM[SessionInit] WHERE ID = " + sessionData.ID);
        }
        public List<SessionData> GetSessionDataNotDone()
        {
            return database.Query<SessionData>("SELECT * FROM [SessionData] ");
        }
        public List<SessionData> GetSessionData(int idSessionInit)
        {
            return database.Table<SessionData>().Where(i => i.SessionInitId == idSessionInit).ToList();
        }
        public List<SessionData> GetSessionsData()
        {
            return database.Table<SessionData>().ToList();
        }

        /*---------------------------------------------------------------------
        * 
        *                              SESSION DATA
        * 
        ----------------------------------------------------------------------*/
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

        /*---------------------------------------------------------------------
        * 
        *                              SESSION INIT
        * 
        ----------------------------------------------------------------------*/
        public int DeleteSessionInit(SessionInit item)
        {
            DeleteSessionDataById(GetSessionData(item.ID));
            return database.Delete(item);
        }
        public void DeleteSessionInitById(List<SessionInit> items)
        {
            foreach (SessionInit sessionInit in items)
                database.Query<SessionInit>("DELETE FROM[SessionInit] WHERE ID = " + sessionInit.ID);

        }
        public List<SessionInit> GetSessionInitNotDone()
        {
            return database.Query<SessionInit>("SELECT * FROM [SessionInit] ");
        }
        public List<SessionInit> GetSessionInit(int idSession)
        {
            return database.Table<SessionInit>().Where(i => i.SessionId == idSession).ToList();
        }
        public List<SessionInit> GetSessionsInit()
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
