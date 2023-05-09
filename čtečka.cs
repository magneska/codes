using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace haaaaaaaaaaa
{
    public partial class Form1 : Form
    {
        //Vytvoření proměnných
        public string uct_name;
        public int counter;
        public string[] pole;
        string cas;
        string datum;
        string temp;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) 
        //Akce click na tlačítko Save
        {         
            //Uložení hodnot do csv douboru
            StreamWriter writer = File.AppendText("data.csv");
            writer.WriteLine(jmeno.Text +","+ produkt.Text);
            writer.Close();
            
            //Naplnění čísla produktu do pole + zobrazení csv v datagridu
            //pole[counter] = produkt.Text;
            jmeno.Enabled = false;
            produkt.Clear();
            produkt.Focus();
            dataGridView1.DataSource = Read("data.csv");           
            counter++;                               
        }
       
        public List<data> Read(string csvFile)
        {
            var temp1 = from start in File.ReadAllLines(csvFile)
                        let data = start.Split(',')
                        select new data
                        {
                            Jméno = data[0],
                            Číslo_produktu = data[1],                                                  
                        };
            return temp1.ToList();
        }

        public class data
        //Třída pro csv
        {
            public string Jméno { get; set; }
            public string Číslo_produktu { get; set; }

        }
        private void button4_Click(object sender, EventArgs e)
        //Akce click na tlačítko Pay
        {
            //odstranení všeho z csv souboru
            TextWriter csv = new StreamWriter("data.csv");
            csv.Write("");
            csv.Close();

            //Naplnení proměnných pro tisk účtenek
            uct_name = jmeno.Text;
            datum = DateTime.Now.ToLongDateString();
            cas = DateTime.Now.ToLongTimeString();
            //Vytvoření txt souboru pro simulaci tisku účtenky
            File.Create(uct_name + "_uctenka.txt").Close();

            //Tisk účtenky
            TextWriter uct = new StreamWriter(uct_name + "_uctenka.txt");
            { 
                uct.WriteLine("ID: "+uct_name);
                uct.WriteLine("_______________________");
                uct.WriteLine("Číslo zboží:");

            //Cyklus for pro naplnení čísel zboží do účtenky
            for (int i = 0; i < counter; i++)
            {
                    temp = pole[i];
                    if (temp == "11542S5") uct.WriteLine(temp + " Kladivo BUM");
                    if (temp == "ZTW2245") uct.WriteLine(temp + " Sada ochranných brýlí");
                    if (temp == "2245845") uct.WriteLine(temp + " Kleště SCŘÍP");
                   
                }
            
                uct.WriteLine("_______________________");
                uct.WriteLine("Datum a čas tisku: ");
                uct.WriteLine(datum);
                uct.WriteLine(cas);
                uct.Close();
            }

            counter = 0;
            jmeno.Enabled = true;
            jmeno.Clear();
            jmeno.Focus();
            produkt.Clear();            
            dataGridView1.DataSource = Read("data.csv");
                                                                     
        }

        private void start_Click(object sender, EventArgs e)
        //Akce click na tlačítko start
        {
            //Smazání obsahu v csv souboru + základní nastavení Formu
            TextWriter txt = new StreamWriter("data.csv");
            txt.Write("");
            txt.Close();
            dataGridView1.DataSource = Read("data.csv");

            jmeno.Enabled = true;
            produkt.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            jmeno.Focus();
        }            
    }
}
