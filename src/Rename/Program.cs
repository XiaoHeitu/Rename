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
            if (args.Length == 0)
            {
                //显示帮助信息
                rc.ShowHelp();
                return;
            }

            var input = args.Last();

            var options = args.Take(args.Length - 1).ToArray();
            for (int i= 0;i < options.Length;i++)
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
                            rc.InitialUpper(input);
                            isfinish = true;
                            break;
                        }
                    case "-il":
                        {
                            //首字母小写
                            rc.InitialLower(input);
                            isfinish = true;
                            break;
                        }
                    case "-u":
                        {
                            //全大写
                            rc.FullUpper(input);
                            isfinish = true;
                            break;
                        }
                    case "-l":
                        {
                            //全小写
                            rc.FullLower(input);
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
