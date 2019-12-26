using System;
using System.IO;
using System.Linq;

namespace Rename
{
    class Program
    {
        static void Main(string[] args)
        {
            RenameCore rc = new RenameCore();
            if (args.Length == 0 ||
                args.Any(d => d == "-?" || d == "/?"))
            {
                //显示帮助信息
                rc.ShowHelp();
                return;
            }

            var curPath = Directory.GetCurrentDirectory();
            var input = args.Last();
            var source = Path.Combine(curPath, input).Replace("\\","/");

            var options = args.Take(args.Length - 1).ToArray();
            for (int i = 0; i < options.Length; i++)
            {
                var o = options[i];
                var isfinish = false;
                switch (o.ToLower())
                {
                    case "-?":
                    case "/?":
                        {
                            //显示帮助信息
                            rc.ShowHelp();
                            isfinish = true;
                            break;
                        }
                    case "-iu":
                        {
                            //首字母大写
                            rc.InitialUpper(source);
                            isfinish = true;
                            break;
                        }
                    case "-il":
                        {
                            //首字母小写
                            rc.InitialLower(source);
                            isfinish = true;
                            break;
                        }
                    case "-u":
                        {
                            //全大写
                            rc.FullUpper(source);
                            isfinish = true;
                            break;
                        }
                    case "-l":
                        {
                            //全小写
                            rc.FullLower(source);
                            isfinish = true;
                            break;
                        }
                    default:
                        {
                            throw new Exception("不支持的参数");
                            isfinish = true;
                            break;
                        }
                }

                if (isfinish)
                {
                    break;
                }
            }
        }
    }
}
