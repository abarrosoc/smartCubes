using System;
using System.IO;
using Xamarin.Forms;


[assembly: Dependency(typeof(SaveIOS))]
class SaveIOS: ISave
{

    public string Save(string filename, string contentType, MemoryStream stream)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, filename);

        FileStream fileStream = File.Open(filePath, FileMode.Create);
        stream.Position = 0;
        stream.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();

        return filePath;
    }
}
