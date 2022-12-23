using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Reflection;

namespace OpenJTalkInterfaceForYMM4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<UserDictionary> UserDic = new List<UserDictionary>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserDictionary>));
            
            Assembly asm = Assembly.GetEntryAssembly();
            string UserDicPath = Path.GetDirectoryName(asm.Location) + "\\UserDictionary.xml";
            
            if (!File.Exists(UserDicPath))
            {
                using (var Streamwriter = new StreamWriter(UserDicPath, false, new System.Text.UTF8Encoding(false)))
                {
                    UserDic.Add(new UserDictionary("OpenJTalk", "オープンジェートーク"));
                    serializer.Serialize(Streamwriter, UserDic);
                    Streamwriter.Flush();
                }
            }
            else
            {
                var xmlSettings = new System.Xml.XmlReaderSettings
                {
                    CheckCharacters = false,
                };
                using (var streamReader = new StreamReader(UserDicPath, Encoding.UTF8))
                using (var xmlReader = System.Xml.XmlReader.Create(streamReader, xmlSettings))
                {
                    UserDic = (List<UserDictionary>)serializer.Deserialize(xmlReader);
                }
            }
            if (args.Length == 0)
            {
                return;
            }
            else
            {
                if (args[0].ToLower() == "-help")
                {
                    System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.StreamReader sr =
                        new System.IO.StreamReader(
                        myAssembly.GetManifestResourceStream("OpenJTalkInterfaceForYMM4.Help.txt"),
                            System.Text.Encoding.UTF8);
                    Console.WriteLine(sr.ReadToEnd());
                    return;
                }
                StringBuilder JtalkArgs = new StringBuilder();

                string? binaryPath = null;
                string? text = null;

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-bin":
                            binaryPath = args[++i];
                            break;
                        case "-text":
                            text = args[++i];
                            break;
                        default:
                            JtalkArgs.AppendFormat("\"{0}\" ", args[i]);
                            break;
                    }
                }



                if (binaryPath != null && text != null)
                {
                    #region ユーザー辞書
                    foreach (UserDictionary i in UserDic)
                    {
                        if (i.IgnoreCase)
                        {
                            text = text.Replace(i.Word, i.Replace);
                        }
                        else
                        {
                            text = Regex.Replace(text, i.Word, i.Replace, RegexOptions.IgnoreCase);
                        }
                    }
                }

                #endregion
                Process prs = new Process();
                prs.StartInfo.FileName = binaryPath;
                prs.StartInfo.Arguments = JtalkArgs.ToString();
                prs.StartInfo.RedirectStandardInput = true;
                prs.Start();
                using (var stdin = prs.StandardInput)
                {
                    stdin.WriteLine(text);
                }
            }

        }
    }



}