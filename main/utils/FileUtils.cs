using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csf.main.utils
{
    class FileUtils
    {
        public static string readResourceFileToString(string resourceFilePath)
        {
            return File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                + @"\resources\" + resourceFilePath);
        }
    }
}
