using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

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

                stringBuilder.Append(Path.GetFileName(path.FullName.TrimEnd('\\', '/')));
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
    }
}
