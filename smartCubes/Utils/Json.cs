using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using smartCubes.Models;

namespace smartCubes.Utils
{
    public class Json
    {
        public Json()
        {
        }
        internal static ActivitiesModel GetActivities()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            if (!File.Exists(filename))
                LoadActivities();
            var text = File.ReadAllText(filename);

            ActivitiesModel list = JsonConvert.DeserializeObject<ActivitiesModel>(text);
            return list;
        }

        internal static ActivityModel GetActivityByName(String activityName)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            var text = File.ReadAllText(filename);

            ActivitiesModel list = JsonConvert.DeserializeObject<ActivitiesModel>(text);
            foreach (ActivityModel activity in list.Activities)
            {
                if (activity.Name.Equals(activityName))
                    return activity;
            }
            return null;
        }

        internal static bool AddActivity(ActivityModel activity)
        {
            ActivitiesModel activities = GetActivities();
            foreach (ActivityModel act in activities.Activities)
            {
                if (act.Name.Equals(activity.Name))
                    return false;
            }

            activity.Id = activities.Activities[activities.Activities.Count - 1].Id + 1;
            activities.Activities.Add(activity);

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            File.WriteAllText(filename, output);
            return true;
        }

        internal static bool UpdateActivity(ActivityModel activity)
        {
            ActivitiesModel activities = GetActivities();
            foreach (ActivityModel activityOriginal in activities.Activities)
            {
                if (activityOriginal.Id.Equals(activity.Id))
                {
                    activityOriginal.Name = activity.Name;
                    activityOriginal.Devices = activity.Devices;
                    activityOriginal.Messages = activity.Messages;
                    activityOriginal.Description  = activity.Description;
                }
            }

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            File.WriteAllText(filename, output);
            return true;

        }

        internal static bool DeleteActivity(ActivityModel activity)
        {
            ActivityModel activityRemove = null;
            ActivitiesModel activities = GetActivities();
            foreach (ActivityModel act in activities.Activities)
            {
                if (act.Id == activity.Id)
                {
                    activityRemove = act;
                }
            }
            if (activityRemove == null)
                return false;
            activities.Activities.Remove(activityRemove);

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            File.WriteAllText(filename, output);
            return true;
        }

        internal static void DeleteFilesActivities()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            if (File.Exists(filename))
                File.Delete(filename);
        }
        internal static void LoadActivities()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Json)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("smartCubes.ActivitiesApp.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "ActivitiesApp.json");
            File.WriteAllText(filename, text);
        }
    }
}
