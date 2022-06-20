using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace Satelity
{
    public partial class Form1 : Form
    {
        public Baza baza;
        public Form1()
        {
            InitializeComponent();
            baza = new Baza();
        }
        List<Satelita> satelity = new List<Satelita>();
        int flag;
        bool flag2 = false;

        public void sorry()
        {
            var json = File.ReadAllText(@"sat1.json");
            Satelita data = JsonConvert.DeserializeObject<Satelita>(json);
            satelity.Add(data);
            richTextBox2.Text = "Pobrano dane";
        }


        public async Task<Satelita> GetDataAsync()
        {
            string call = "https://api.wheretheiss.at/v1/satellites/25544";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(call);
            Satelita data= JsonConvert.DeserializeObject<Satelita>(response);
            satelity.Add(data);
            baza.satelity.Add(data);
            baza.SaveChanges();
            richTextBox2.Text = "Pobrano dane";
            return data;

        }

       public void button1_Click(object sender, EventArgs e)
        {
            GetDataAsync();
        }

        //wyswietl dane
        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                richTextBox1.Text += $"\n{satelity[satelity.Count - 1].longitude}°";
            }
            else if (flag == 2)
            {
                richTextBox1.Text += $"\n{satelity[satelity.Count - 1].latitude}°";
            }
            else if (flag == 3)
            {
                double a = convert(satelity[satelity.Count - 1].altitude);
                string uniiit = whichUnit();
                richTextBox1.Text += $"\n{a}  {uniiit}";
            }
            else if (flag == 4)
            {
                double a = convert(satelity[satelity.Count - 1].velocity);
                string uniiit = whichUnit();
                richTextBox1.Text += $"\n{a}  {uniiit}/h";
            }
        }

        //długość geograficzna
        private void button3_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = "Długość geograficzna: \n";
            flag = 1;
        }

        //szerokosc geograficzna
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Szerokość geograficzna: \n";
            flag = 2;
        }

        //wysokosc npm
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Wysokość nad poziomem morza: \n";
            flag = 3;
        }

        //predkosc
        private void button6_Click(object sender, EventArgs e) 
        {
            richTextBox1.Text = "Prędkość: \n";
            flag = 4;
        }

        public string whichUnit()
        {
            if (flag2)
            {
                return "mile";
            }
            else
            {
                return "km";
            }
        }
        public double convert(double wielkosc)
        {
            if (flag2)
            {
                wielkosc = convertToMile(wielkosc);
            }
            return wielkosc;
        }
        public double convertToMile(double wielkosc)
        {
            return wielkosc * 0.6214;
        }

        public double convertToKilometer(double wielkosc)
        {
            return wielkosc / 0.6214;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(!flag2)
            {
                flag2 = true;
                button7.Text = "Zmień na kilometry";
            }
            else
            {
                flag2 = false;
                button7.Text = "Zmień na mile";
            }

        }
        //baza danych
        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            var satel = baza.satelity.Where(s => s.id > 0).ToList<Satelita>();
            foreach (var satelita in satel)
            {
                richTextBox3.Text += satelita.Show(flag);
            }
        }
    }
}
