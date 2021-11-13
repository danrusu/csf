using System;
using System.IO;

namespace csf.main.utils
{
    class FileUtils
    {
        public static string readResourceFileToString(string localResourcePath)
        {
            return File.ReadAllText(getFullResourcePath(localResourcePath));
        }

        public static string getFullResourcePath(string localResourcePath)
        {
            return Path.Join(
                Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
                "resources",
                localResourcePath);
        }
    }
}
