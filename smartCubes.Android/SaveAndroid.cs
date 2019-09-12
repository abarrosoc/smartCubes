using System;
using System.IO;
using Java.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndroid))]
class SaveAndroid: ISave
    {
    public string Save(string fileName, string contentType, MemoryStream stream)
    {
        string root = null;
        if (Android.OS.Environment.IsExternalStorageEmulated)
        {
            root = Android.OS.Environment.ExternalStorageDirectory.ToString();
        }
        else
            root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
        myDir.Mkdir();

        Java.IO.File file = new Java.IO.File(myDir, fileName);

        if (file.Exists()) file.Delete();

        FileOutputStream outs = new FileOutputStream(file);
        outs.Write(stream.ToArray());

        outs.Flush();
        outs.Close();
        Android.Net.Uri path = null;
        if (file.Exists())
        {
            path = Android.Net.Uri.FromFile(file);
        }

        return path.Path;
    }
}
