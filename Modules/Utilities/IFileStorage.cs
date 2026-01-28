using System;
using System.IO;
using System.Web;
using ZadHolding.Been;

namespace ZadHolding.Utilities
{
    public interface IFileStorage
    {
        string SaveFile(HttpPostedFile file, string directory, string fileName);
        void SaveFile(Stream stream, string directory, string fileName);
        string GetFileUrl(string directory, string fileName);
        bool FileExists(string directory, string fileName);
        Stream GetFileStream(string directory, string fileName);
    }
}
