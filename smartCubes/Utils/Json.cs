using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using smartCubes.Models;
using System.Collections.ObjectModel;

namespace smartCubes.Utils
{
    public class Json
    {
        public Json()
        {
        }

        internal static ActivitiesModel getActivities()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            //if (File.Exists(filename))
            //     Console.WriteLine("existe");
            var text = File.ReadAllText(filename);

            ActivitiesModel list = JsonConvert.DeserializeObject<ActivitiesModel>(text);
            return list;
        }
        internal static ActivityModel getActivityByName(String activityName)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            //if (File.Exists(filename))
            //     Console.WriteLine("existe");
            var text = File.ReadAllText(filename);

            ActivitiesModel list = JsonConvert.DeserializeObject<ActivitiesModel>(text);
            foreach (ActivityModel activity in list.Activities)
            {
                if (activity.Name.Equals(activityName))
                    return activity;
            }
            return null;
        }
        internal static bool addActivity(ActivityModel activity)
        {
            ActivitiesModel activities = getActivities();
            foreach (ActivityModel act in activities.Activities)
            {
                if (act.Name.Equals(activity.Name))
                    return false;
            }

            activity.Id = activities.Activities[activities.Activities.Count - 1].Id + 1;
            activities.Activities.Add(activity);

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            File.WriteAllText(filename, output);
            return true;
        }

        internal static bool updateActivity(ActivityModel activity)
        {
            ActivitiesModel activities = getActivities();
            foreach (ActivityModel activityOriginal in activities.Activities)
            {
                if (activityOriginal.Id.Equals(activity.Id))
                {
                    activityOriginal.Name = activity.Name;
                    activityOriginal.Devices = activity.Devices;
                }
            }

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            File.WriteAllText(filename, output);
            return true;

        }

        internal static bool deleteActivity(ActivityModel activity)
        {
            ActivityModel activityRemove = null;
            ActivitiesModel activities = getActivities();
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
            var filename = Path.Combine(documents, "activitiesApp.json");
            File.WriteAllText(filename, output);
            return true;
        }

        internal static void deleteFilesActivities()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            if (File.Exists(filename))
                File.Delete(filename);
        }
        internal static void loadActivities()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Json)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("smartCubes.activitiesApp.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "activitiesApp.json");
            File.WriteAllText(filename, text);
        }
    }
}
