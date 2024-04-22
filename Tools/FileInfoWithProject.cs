using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCopyExtensionVs.Tools
{
    internal class FileInfoWithProject
    {
        public FileInfo Item { get; set; }
        public string ProjectFullName { get; set; }

        public FileInfoWithProject()
        {

        }

        public FileInfoWithProject(FileInfo item, string projectFullName)
        {
            Item = item;
            ProjectFullName = projectFullName;
        }
    }
}
