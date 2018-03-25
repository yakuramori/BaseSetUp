using System;
using System.IO;
using log4net;

namespace BaseSetUp
{
    public static class FileUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string GenericDownloadFolder = @"C:\Users\%%\Downloads";

        public static bool IfFileExistsInFolder(string fileName, string folder)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder))
            {
                throw new ArgumentException($"Invalid file name or folder. File name: {fileName} Folder: {folder}");
            }

            var path = $@"{folder}\{fileName}";
            var result = File.Exists(path);
            Log.Info($"File: {path} exists = {result}");
            return result;
        }

        public static bool IfFileExistsInDownloadFolder(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"Invalif file name: {fileName}");
            return IfFileExistsInFolder(fileName, GetDownloadFolder());
        }

        private static string GetDownloadFolder()
        {
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            var sysDownloadFolder = GenericDownloadFolder.Replace("%%", userName);
            return sysDownloadFolder;
        }
    }
}
