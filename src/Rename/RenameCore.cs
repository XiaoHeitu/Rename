using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Rename
{
    public class RenameCore
    {
        /// <summary>
        /// 文件改名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void FileRename(string source, string target)
        {
            if (!File.Exists(source))
            {
                throw new Exception("文件不存在！");
            }

            Console.Write("{0} -> {1}", Path.GetFileName(source).PadRight(20), target.PadRight(20));
            Process.Start("cmd.exe", $"/c ren {source} {target}");
            
            Console.WriteLine("  OK!");

        }

        /// <summary>
        /// 递归处理文件夹
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileOperat"></param>
        private void Operat(string source, Action<string> fileOperat)
        {
            if (Directory.Exists(source))
            {
                var subDirs = Directory.GetDirectories(source);
                foreach (var d in subDirs)
                {
                    this.Operat(d, fileOperat);
                }
                var files = Directory.GetFiles(source);
                foreach (var f in files)
                {
                    fileOperat(f);
                }
                return;
            }

            fileOperat(source);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="source"></param>
        public void InitialUpper(string source)
        {
            this.Operat(source, (s) =>
            {
                var name = Path.GetFileNameWithoutExtension(s);
                var ext = Path.GetExtension(s);
                var targetName = name.Substring(0, 1).ToUpper() + name.Substring(1);
                var targetFullName = targetName + ext;

                this.FileRename(s, targetFullName);
            });
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="source"></param>
        public void InitialLower(string source)
        {
            this.Operat(source, (s) =>
            {
                var name = Path.GetFileNameWithoutExtension(s);
                var ext = Path.GetExtension(s);
                var targetName = name.Substring(0, 1).ToLower() + name.Substring(1);
                var targetFullName = targetName + ext;

                this.FileRename(s, targetFullName);
            });
        }
        /// <summary>
        /// 全大写
        /// </summary>
        /// <param name="source"></param>
        public void FullUpper(string source)
        {
            this.Operat(source, (s) =>
            {
                var name = Path.GetFileNameWithoutExtension(s);
                var ext = Path.GetExtension(s);
                var targetName = name.ToUpper();
                var targetFullName = targetName + ext;

                this.FileRename(s, targetFullName);
            });
        }
        /// <summary>
        /// 全小写
        /// </summary>
        /// <param name="source"></param>
        public void FullLower(string source)
        {
            this.Operat(source, (s) =>
            {
                var name = Path.GetFileNameWithoutExtension(s);
                var ext = Path.GetExtension(s);
                var targetName = name.ToLower();
                var targetFullName = targetName + ext;

                this.FileRename(s, targetFullName);
            });
        }
    }
}
