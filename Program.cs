using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OpenJTalkInterfaceForYMM4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }
            else
            {
                if (args[0].ToLower() == "help")
                {
                    System.Reflection.Assembly myAssembly =   System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.StreamReader sr =
                        new System.IO.StreamReader(
                        myAssembly.GetManifestResourceStream("OpenJTalkInterfaceForYMM4.Help.txt"),
                            System.Text.Encoding.UTF8);
                    Console.WriteLine(sr.ReadToEnd());
                    return;
                }
                string[] ParsedArgs = CmdParser.ParseCmdArgs(args);
                if(ParsedArgs is null)
                {
                    return;
                }
                StringBuilder JtalkArgs = new StringBuilder();

                string? binaryPath = null;
                string? text = null;

                for (int i = 0; i < args.Length; i++)
                {
                    switch (ParsedArgs[i])
                    {
                        case "-bin":
                            binaryPath = ParsedArgs[++i];
                            break;
                        case "-text":
                            text= ParsedArgs[++i];
                            break;
                        default:
                            JtalkArgs.AppendFormat("{0} ",args[i]);
                            break;
                    }
                }

                if (binaryPath != null && text != null)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"text.txt", false,    System.Text.Encoding.GetEncoding("shift_jis")))
                    {
                        sw.Write(text);
                    }
                    JtalkArgs.AppendFormat("\"{0}text.txt\"", AppDomain.CurrentDomain.BaseDirectory);
                    Process prs=new Process();
                    prs.StartInfo.FileName = binaryPath;
                    prs.StartInfo.Arguments = JtalkArgs.ToString();

                    prs.Start();
                }

            }
        }
    }
}
