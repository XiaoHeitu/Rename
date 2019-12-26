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
            source = source.Replace("\\", "/");
            if (!File.Exists(source))
            {
                throw new Exception("File is not found!");
            }

            var dir = Path.GetDirectoryName(source);
            var sfile = Path.GetFileName(source);

            Console.Write("{0} -> {1}", sfile.PadRight(20), target.PadRight(20));
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WorkingDirectory = Path.GetDirectoryName(source);
            psi.FileName = "cmd.exe";
            psi.Arguments = $"/c ren {sfile} {target}";
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            var p = Process.Start(psi);

            p.WaitForExit();
            var error = p.StandardError.ReadToEnd().Trim();
            if (string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("  OK!");
            }
            else
            {
                Console.WriteLine($"  Fail!{error}");
            }


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

        /// <summary>
        /// 显示帮助
        /// </summary>
        public void ShowHelp()
        {
            Console.WriteLine("dotnet-rename options path");
            Console.WriteLine();

            Console.WriteLine("options:");
            Console.WriteLine("  {0} {1}", "-?".PadRight(4), "Show help info");
            Console.WriteLine("  {0} {1}", "-u".PadRight(4), "Convert all letters to uppercase");
            Console.WriteLine("  {0} {1}", "-l".PadRight(4), "Convert all letters to lowercase");
            Console.WriteLine("  {0} {1}", "-iu".PadRight(4), "Convert first letter to uppercase");
            Console.WriteLine("  {0} {1}", "-il".PadRight(4), "Convert first letter to lowercase");
            Console.WriteLine();

            Console.WriteLine("example:");

            Console.WriteLine("  Convert all files in directory");
            Console.WriteLine("    dotnet-rename -u e:\\js");
            Console.WriteLine();
            Console.WriteLine("  Convert specified file");
            Console.WriteLine("    dotnet-rename -u e:\\js\\example.js");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
