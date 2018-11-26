using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Mvc;
using System.Threading;
using xTab.Tools.Extensions;

namespace xTab.Tools.Helpers
{
    public static class IoHelper
    {
        public static String GetRandomFileName(String Path, String Extension, Int32 NameLength, Boolean CreateDirectoryIfNotExists = true, String[] SubDirectoryes = null)
        {
            var result = String.Empty;
            var path = FormatPath(Path);
            var name = String.Empty;

            do
            {
                name = RandomHelper.RandomString(NameLength) + "." + Extension;
                result = path + name;
            }
            while (File.Exists(CurrentServer.MapPath(result)));

            if (CreateDirectoryIfNotExists)
                CreateDirectory(path, SubDirectoryes);

            return result;
        }

        public static String GetRandomDirectory(String Path, Int32 NameLength, Boolean CreateDirectoryIfNotExists = true, String[] SubDirectoryes = null)
        {
            var result = String.Empty;
            var path = FormatPath(Path);
            var name = String.Empty;

            do
            {
                name = RandomHelper.RandomString(NameLength);
                result = path + name + "/";
            }
            while (Directory.Exists(CurrentServer.MapPath(result)));

            if (CreateDirectoryIfNotExists)
                CreateDirectory(result, SubDirectoryes);

            return result;
        }


        public static String CreateDirectory(string path, string[] subDirectoryes = null)
        {
            path = FormatPath(path);

            try
            {
                if (!Directory.Exists(CurrentServer.MapPath(path)))
                {
                    Directory.CreateDirectory(CurrentServer.MapPath(path));
                    if (subDirectoryes != null)
                        foreach (String dir in subDirectoryes)
                            Directory.CreateDirectory(CurrentServer.MapPath(path + dir));
                }

            }
            catch
            {
                return null;
            }

            return path;
        }

        public static String GetSubDirectoryPath(String Path, String SubDirectory)
        {
            return Path.Insert(Path.LastIndexOf("/") + 1, SubDirectory + "/");
        }

        public static String GetFileDirectoryPath(string filePath)
        {
            filePath = FormatPath(filePath);

            return filePath.Substring(0, filePath.LastIndexOf('/'));
        }

        public static String DeleteFile(String Path, String[] SubDirectoryes = null)
        {
            try
            {
                var path = FormatPath(Path);

                if (File.Exists(CurrentServer.MapPath(path)))
                    File.Delete(CurrentServer.MapPath(path));

                if (SubDirectoryes != null)
                    foreach (String dir in SubDirectoryes)
                        DeleteFile(GetSubDirectoryPath(path, dir));

                return path;
            }
            catch
            {
                return null;
            }
        }

        public static String DeleteFile(String Path, String SubNames, Boolean postfixSubName = true, Char separator = ',')
        {
            try
            {
                var path = FormatPath(Path);
                var subNames = SubNames.ClearSplit(separator);

                if (!String.IsNullOrEmpty(SubNames))
                    File.Delete(CurrentServer.MapPath(path));

                if (subNames != null) 
                    foreach (String name in subNames)
                        DeleteFile(GetSubName(path, name, postfixSubName));

                return path;
            }
            catch
            {
                return null;
            }
        }


        public static String DeleteDirectory(string path)
        {
            path = FormatPath(path);

            try
            {
                foreach (var file in Directory.GetFiles(CurrentServer.MapPath(path)))
                {
                    File.Delete(file);
                }

                foreach (var dir in Directory.GetDirectories(CurrentServer.MapPath(path)))
                {
                    Directory.Delete(dir, true);
                }

                Thread.Sleep(1);
                Directory.Delete(CurrentServer.MapPath(path), true);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return path;
        }

        public static bool CopyFile(string sourcePath, string destPath)
        {
            sourcePath = FormatPath(sourcePath);
            destPath = FormatPath(destPath);

            CreateDirectory(GetFileDirectoryPath(destPath));

            try
            {
                File.Copy(CurrentServer.MapPath(sourcePath), CurrentServer.MapPath(destPath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool MoveFile(string sourcePath, string destPath)
        {
            sourcePath = FormatPath(sourcePath);
            destPath = FormatPath(destPath);

            CreateDirectory(GetFileDirectoryPath(destPath));

            try
            {
                File.Move(CurrentServer.MapPath(sourcePath), CurrentServer.MapPath(destPath));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool PathExist(string path)
        {
            path = FormatPath(path);

            if (File.Exists(CurrentServer.MapPath(path)))
                return true;

            if (Directory.Exists(CurrentServer.MapPath(path)))
                return true;

            return false;
        }

        public static string GetSubName(string path, string subName, bool postfixSubName = true)
        {
            path = FormatPath(path);

            if (postfixSubName)
                if (path.LastIndexOf(".") > 0)
                    return path.Insert(path.LastIndexOf("."), subName);
            else
                if (path.LastIndexOf("/") > 0)
                    return path.Insert(path.LastIndexOf("/") + 1, subName);

            return path;
        }

        public static String FormatPath(string path)
        {
            path = path ?? String.Empty; 

            path = path.Trim();

            if (!path.EndsWith("/") && !path.Contains("."))
                path += "/";

            if (!path.StartsWith("/"))
                path = "/" + path;

            return path;
        }  

        private static HttpServerUtility CurrentServer
        {
            get { return HttpContext.Current.Server; }
        }
    }
}