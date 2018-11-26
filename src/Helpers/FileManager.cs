using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace xTab.Tools.Helpers
{
    public class FileManager
    {
        private HttpServerUtility server;

        public FileManager(FileManagerOptions options = null)
        {
            options = options ?? new FileManagerOptions();
            server = HttpContext.Current.Server;
        }

        public List<String> GetFiles(string path)
        {
            var files = Directory.GetFiles(server.MapPath(path));

            return files.ToList();
        }

        public class File1
        {
            public String Name { get; set; }
            public String Path { get; set; }
            public String Directory { get; set; }
            public String Extension { get; set; }
            public Int32 Size { get; set; }
        }

        public class Directory1
        {
            public String Name { get; set; }
            public String Path { get; set; }
        }

        public class FileManagerOptions
        {
            public Boolean HideThumbs { get; set; } = true;
            public Boolean AllowUpload { get; set; } = true;
            public Boolean OnlyCurrentDirectory { get; set; } = true;
            public String AllowedExtensions { get; set; } = "jpg,jpeg,png,webp,gif,bmp,doc,docx,xls,xlsx,rtg,odf,pdf,txt,zip,7z";
        }
    }
}
