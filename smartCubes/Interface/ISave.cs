using System;
using System.IO;
using System.Threading.Tasks;

public interface ISave
{
    string SaveAndView(string filename, string contentType, MemoryStream stream);
}

