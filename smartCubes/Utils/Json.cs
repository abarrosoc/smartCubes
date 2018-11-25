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
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ActivityModel)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("smartCubes.Resources.activities.json");

            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            ActivitiesModel list = JsonConvert.DeserializeObject<ActivitiesModel>(text);
            return list;
            //Console.WriteLine(myText);
        }
        internal static void addActivity(ActivityModel activity)
        {
            ActivitiesModel activities = getActivities();
            activities.Activities.Add(activity);

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            File.WriteAllText("smartCubes.Resources.activities.json", output);
        }

        internal static void updateActivity(ActivityModel activity)
        {
            ActivitiesModel activities = getActivities();
            foreach(ActivityModel activityOriginal in activities.Activities){
                if(activityOriginal.Id.Equals(activity)){
                    activityOriginal.Name = activity.Name;
                    activityOriginal.Devices = activity.Devices;
                }
            }

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            File.WriteAllText("smartCubes.Resources.activities.json", output);

           
        }

        internal static void deleteActivity(ActivityModel activity)
        {
            ActivitiesModel activities = getActivities();
            activities.Activities.Remove(activity);

            string output = JsonConvert.SerializeObject(activities, Formatting.Indented);

            File.WriteAllText("smartCubes.Resources.activities.json", output);

        }
    }
}
