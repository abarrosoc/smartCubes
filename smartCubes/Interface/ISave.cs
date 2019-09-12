using System.IO;

public interface ISave
{
    string Save(string filename, string contentType, MemoryStream stream);
}

