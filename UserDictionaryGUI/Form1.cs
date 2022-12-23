using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UserDictionaryGUI
{
    public partial class Form1 : Form
    {
        List<UserDictionary> UserDic = new List<UserDictionary>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserDictionary>));
        string UserDicPath = AppDomain.CurrentDomain.BaseDirectory + "UserDictionary.xml";
        public Form1()
        {
            InitializeComponent();

            UserDictionaryView.ColumnCount = 2;
            DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
            UserDictionaryView.Columns.Add(c);
            UserDictionaryView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UserDictionaryView.Columns[0].HeaderText = "置換前";
            UserDictionaryView.Columns[1].HeaderText = "置換後";
            UserDictionaryView.Columns[2].HeaderText = "大文字小文字の区別";
            UserDictionaryView.MultiSelect = false;
            UserDictionaryView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadDictionary();
            DictionaryRefresh();
            UserDictionaryView.CellValueChanged += UserDictionaryView_CellValueChanged;
        }

        void LoadDictionary()
        {
            if (File.Exists(UserDicPath))            
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
        }

        private void UserDictionaryView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < UserDictionaryView.Rows.Count)
            {

                var RowEditing = UserDictionaryView.Rows[e.RowIndex];

                if (e.RowIndex < UserDic.Count)
                {
                    UserDic[e.RowIndex].Word = (string)RowEditing.Cells[0].Value;
                    UserDic[e.RowIndex].Replace = (string)RowEditing.Cells[1].Value;
                    if (RowEditing.Cells[2].Value == null)
                    {
                        UserDic[e.RowIndex].IgnoreCase = false;
                    }
                    else
                    {
                        UserDic[e.RowIndex].IgnoreCase = (bool)RowEditing.Cells[2].Value;
                    }

                }
                else
                {

                    string Word = (string)RowEditing.Cells[0].Value;
                    if (Word == null)
                    {
                        Word = "";
                    }
                    string Replace = (string)RowEditing.Cells[1].Value;
                    if(Replace == null)
                    {
                        Replace = "";
                    }
                    bool IgnoreCase;
                    if (RowEditing.Cells[2].Value == null)
                    {
                        IgnoreCase = false;
                    }
                    else
                    {
                        IgnoreCase = (bool)RowEditing.Cells[2].Value;
                    }
                    
                    UserDic.Add(
                        new UserDictionary(Word, Replace, IgnoreCase));
                }

            }
        }

        void DictionaryRefresh()
        {
            UserDictionaryView.Rows.Clear();
            foreach (UserDictionary i in UserDic)
            {
                UserDictionaryView.Rows.Add(i.Word, i.Replace, i.IgnoreCase);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < UserDic.Count; i++)
            {
                UserDic[i].Word = (string)UserDictionaryView.Rows[i].Cells[0].Value;
                UserDic[i].Replace = (string)UserDictionaryView.Rows[i].Cells[1].Value;
                if (UserDictionaryView.Rows[i].Cells[2].Value == null)
                {
                    UserDic[i].IgnoreCase = false;
                }
                else
                {
                    UserDic[i].IgnoreCase = (bool)UserDictionaryView.Rows[i].Cells[2].Value;
                }
            }
            using (var Streamwriter = new StreamWriter(UserDicPath, false, new System.Text.UTF8Encoding(false)))
            {
                serializer.Serialize(Streamwriter, UserDic);
                Streamwriter.Flush();
            }
        }

        private void DicVIewMenuStripRemove_Click(object sender, EventArgs e)
        {
            if (UserDictionaryView.SelectedRows.Count > 0)
            {
                if (UserDictionaryView.SelectedRows[0].Index < UserDic.Count)
                
                {
                    UserDic.RemoveAt(UserDictionaryView.SelectedRows[0].Index);
                    UserDictionaryView.Rows.RemoveAt(UserDictionaryView.SelectedRows[0].Index);
                }
            }
        }
    }

    public class UserDictionary
    {
        public string Word { get; set; }
        public string Replace { get; set; }
        public bool IgnoreCase { get; set; }
        public UserDictionary(string word, string replace, bool ignoreCase)
        {
            Word = word;
            Replace = replace;
            IgnoreCase = ignoreCase;
        }
        public UserDictionary(string word, string replace)
        {
            Word = word;
            Replace = replace;
            IgnoreCase = false;
        }

        public UserDictionary()
        {
            Word = "";
            Replace = "";
            IgnoreCase = false;
        }
    }
}
