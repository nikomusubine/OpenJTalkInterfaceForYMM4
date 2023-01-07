using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OpenJTalkInterfaceForYMM4
{
    internal class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSectionNames(string lpszReturnBuffer, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        static void Main(string[] args)
        {
            List<UserDictionary> UserDic = new List<UserDictionary>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserDictionary>));

            Assembly asm = Assembly.GetEntryAssembly();
            List<Character> characters=new List<Character>();

            #region キャラクター設定
            string iniFilePath = Path.GetDirectoryName(asm.Location) + "\\characters.ini";
            string sectionNames = new string('\0', 100);
            uint ret = GetPrivateProfileSectionNames(sectionNames, (uint)sectionNames.Length, iniFilePath);

            foreach (string sectionName in sectionNames.TrimEnd('\0').Split('\0'))
            {
                Character character = new Character();
                
                character.Name = sectionName;
                StringBuilder sb = new StringBuilder(256);
                //HTSVoice
                GetPrivateProfileString(sectionName, "HTSVoice", "", sb, (uint)sb.Capacity, iniFilePath);
                character.HTSVoice = sb.ToString();

                //SamplingFrequency
        
                ret = GetPrivateProfileInt(sectionName, "SamplingFrequency", 0, iniFilePath);
                character.SamplingFreq = ret;

                //FramePeriod
                ret = GetPrivateProfileInt(sectionName, "FramePeriod", 0, iniFilePath);
                character.FramePeriod = ret;

                //All-PassConstant
                GetPrivateProfileString(sectionName, "All-PassConstant", "-1.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.AllPass = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.AllPass = -1;
                }
                //PostFilteringCoefficient
                GetPrivateProfileString(sectionName, "PostFilteringCoefficient", "0.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.PostFilteringCoefficient = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.PostFilteringCoefficient = 0.0f;
                }
                
                //SpeechSpeed
                GetPrivateProfileString(sectionName, "SpeechSpeed", "1.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.SpeechSpeed = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.SpeechSpeed = 1.0f;
                }                //AdditionalHalf-tone
                GetPrivateProfileString(sectionName, "AdditionalHalf-Tone", "0.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.AdditionalhalfTone = Convert.ToSingle(sb.ToString());
                }catch (FormatException)
                {
                    character.AdditionalhalfTone= 0.0f;
                }
                //Threshold
                GetPrivateProfileString(sectionName, "Threshold", "0.5", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.Threshold = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.Threshold = 0.5f;
                }
                //WeightOfGVSpectrum
                GetPrivateProfileString(sectionName, "WeightOfGVSpectrum", "1.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.WeightOfGVSpectrum = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.WeightOfGVSpectrum = 1.0f;
                }
                //WeightOfGVlogF0
                GetPrivateProfileString(sectionName, "WeightOfGVlogF0", "1.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.WeightOfGVlogF0 = Convert.ToSingle(sb.ToString());
                }
                catch (FormatException)
                {
                    character.WeightOfGVlogF0 = 1.0f;
                }
                //Volume
                GetPrivateProfileString(sectionName, "Volume", "0.0", sb, (uint)sb.Capacity, iniFilePath);
                try
                {
                    character.Volume = Convert.ToSingle(sb.ToString());
                }catch (FormatException)
                {
                    character.Volume = 0.0f;
                }
                characters.Add(character);
            }
            #endregion



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
                string? dic = null;
                string? characterName = null;

                #region Config読み取り
                StringBuilder sb=new StringBuilder(256);
                GetPrivateProfileString("Config", "JTalkBinaryPath", "none", sb, (uint)sb.Capacity, Path.GetDirectoryName(asm.Location)+"\\config.ini");
                binaryPath = sb.ToString();
                GetPrivateProfileString("Config", "DictionaryPath", "", sb, (uint)sb.Capacity, Path.GetDirectoryName(asm.Location) + "\\config.ini");
                dic = sb.ToString();
                #endregion

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
                        case "-character":
                            characterName = args[++i];
                            break;
                        case "-x":
                            dic= args[++i];
                            break;
                        default:
                            JtalkArgs.AppendFormat("\"{0}\" ", args[i]);
                            break;
                    }
                }


                JtalkArgs.AppendFormat("-x \"{0}\"",dic);

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
                
                if (characterName != null)
                {
                    foreach(var i in characters)
                    {
                        if (i.Name == characterName)
                        {
                            JtalkArgs.Append(" ");
                            JtalkArgs.Append(i.ExportArgs());
                        }
                    }
                }
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

    class Character
    {
        public string Name { get; set; }
        public string HTSVoice { get; set; }
        public uint SamplingFreq { get; set; }
        public uint FramePeriod { get; set; }
        public float AllPass { get; set; }
        public float PostFilteringCoefficient { get; set; }
        public float SpeechSpeed { get; set; }  
        public float AdditionalhalfTone { get; set; }
        public float Threshold { get; set; }
        public float WeightOfGVSpectrum { get;set; }
        public float WeightOfGVlogF0 { get; set; }
        public float Volume { get; set; }

        public Character()
        {
            Name = "";
            HTSVoice = "";
            SamplingFreq = 0;
            FramePeriod = 0;
            AllPass = -1;
            PostFilteringCoefficient = 0;
            SpeechSpeed = 1;
            AdditionalhalfTone = 0;
            Threshold = 0.5f;
            WeightOfGVSpectrum = 1;
            WeightOfGVlogF0 = 1;
            Volume = 0;
        }

        public string ExportArgs()
        {
            StringBuilder sb=new StringBuilder();
            sb.AppendFormat("-m \"{0}\" ", HTSVoice);
            if(SamplingFreq != 0)
            {
                sb.AppendFormat("-s {0} ", SamplingFreq);
            }
            if(FramePeriod != 0)
            {
                sb.AppendFormat("-p {0} ",FramePeriod);
            }
            if (AllPass >= 0)
            {
                sb.AppendFormat("-a {0} ", AllPass);
            }
            if (PostFilteringCoefficient >= 0)
            {
                sb.AppendFormat("-b {0} ", PostFilteringCoefficient);
            }
                sb.AppendFormat("-r {0} ", SpeechSpeed);
            
            sb.AppendFormat("-fm {0} ",AdditionalhalfTone);
            sb.AppendFormat("-u {0} ",Threshold);
            sb.AppendFormat("-jm {0} ",WeightOfGVSpectrum);
            sb.AppendFormat("-jf {0} ", WeightOfGVlogF0);
            sb.AppendFormat("-g {0} ", Volume);
            
            return sb.ToString();
        }
    }

}