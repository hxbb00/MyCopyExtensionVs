using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace MyCopyExtensionVs.Tools
{
    internal class ClipboardTools
    {
        internal static void SetNameText(List<FileInfo> paths)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var path in paths)
            {
                if(stringBuilder.Length > 0)
                {
                    stringBuilder.AppendLine();
                }

                stringBuilder.Append(System.IO.Path.GetFileName(path.FullName.TrimEnd('\\', '/')));
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        internal static void SetPathText(List<FileInfo> paths, bool linuxStyle)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var path in paths)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.AppendLine();
                }
                if (linuxStyle)
                {
                    stringBuilder.Append(path.FullName.TrimEnd('\\', '/').Replace('\\', '/'));
                }
                else
                {
                    stringBuilder.Append(path.FullName.TrimEnd('\\', '/'));
                }
            }

            Clipboard.SetText(stringBuilder.ToString());
        }

        internal static void SetRelativePathText(List<FileInfoWithProject> paths, bool linuxStyle)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var path in paths)
            {
                var subStr = string.IsNullOrEmpty(path.ProjectFullName) 
                    ? 0 : System.IO.Path.GetDirectoryName(path.ProjectFullName).Length + 1;

                if (stringBuilder.Length > 0)
                {
                    stringBuilder.AppendLine();
                }
                if (linuxStyle)
                {
                    stringBuilder.Append(path.Item.FullName
                        .Substring(subStr)
                        .TrimEnd('\\', '/')
                        .Replace('\\', '/'));
                }
                else
                {
                    stringBuilder.Append(path.Item.FullName
                        .Substring(subStr)
                        .TrimEnd('\\', '/'));
                }
            }

            Clipboard.SetText(stringBuilder.ToString());
        }
    }
}
