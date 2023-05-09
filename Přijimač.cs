using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SeriovkaC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Získání listu dostupných portů
        string[] ports_ok = SerialPort.GetPortNames();
        // Vytvoření proměnné pro SerialPort
        SerialPort serialport;
        //Proměnná pro přijatá data
        string data;
        //Proměnná pro jméno portu
        string index;
        //Vytvoření tabulky přijatých hodnot
        string[] tab = { "00000000","00000001","00000010", "00000011", 
                         "00000100", "00000101", "00000110", "00000111",
                         "00001000", "00001001" };
        //Proměnná pro zobrazení přenosu
        string vysledek;

        private void button1_Click(object sender, EventArgs e)
        {   //Načtení dostupných portů button
            //Zobrazení dostupných portů uživateli v CMBX1 
            combo1.Items.Clear(); //Ošetření smazání dostupných portů z minulého načtení
            for (int i = 0; i < ports_ok.Length; i++) 
            {
                combo1.Items.Add(ports_ok[i].ToString());  //Přidání listu dostupných portů do CMBX1
            }
            button2.Enabled = true;
            combo1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {   //Connect button
            
            switch (combo1.SelectedIndex)  //Dle indexu z vybraneho CMBX1 se vykona case.
            {
                case 0:
                    index = ports_ok[0];  //Nahrátí jméno portu do proměnné
                    serialport = new SerialPort(index, 9600, Parity.None, 8, StopBits.One); //Nastavení parametrů
                    try
                    {
                        serialport.Open(); //Otevření portu     
                        if (serialport.IsOpen)
                        {
                            timer1.Enabled = true;  //zapnutí timeru pro následné zobrazení
                            button2.Enabled = false;
                            button3.Enabled = true;
                            label1.Text = "Connected to " + index;  //výpis hlášky
                        }
                    }
                    catch   //try,catch -> kvůli zabranění ukončení programu
                    {
                        label1.Text = "CHYBA: Port " + index +" se nepřipojil";
                    }
                    break;

                case 1:
                    index = ports_ok[1];
                    serialport = new SerialPort(index, 9600, Parity.None, 8, StopBits.One);
                    try
                    {
                        serialport.Open();
                        if (serialport.IsOpen)
                        {
                            timer1.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = true;
                            label1.Text = "Connected to " + index;
                        }
                    }
                    catch
                    {
                        label1.Text = "CHYBA: Port " + index + " se nepřipojil";
                    }
                    break;

                case 2:
                    index = ports_ok[2];
                    serialport = new SerialPort(index, 9600, Parity.None, 8, StopBits.One);
                    try
                    {
                        serialport.Open();
                        if (serialport.IsOpen)
                        {
                            timer1.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = true;
                            label1.Text = "Connected to " + index;
                        }
                    }
                    catch
                    {
                        label1.Text = "CHYBA: Port " + index + " se nepřipojil";
                    }
                    break;

                case 3:
                    index = ports_ok[3];
                    serialport = new SerialPort(index, 9600, Parity.None, 8, StopBits.One);
                    try
                    {
                        serialport.Open();
                        if (serialport.IsOpen)
                        {
                            timer1.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = true;
                            label1.Text = "Connected to " + index;
                        }
                    }
                    catch
                    {
                        label1.Text = "CHYBA: Port " + index + " se nepřipojil";
                    }
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        //Disconnect button
        {
            serialport.Close(); //Uzavření portu
            label1.Text = "Disconnected from "+index;
            timer1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        //Zobrazení dat, každých 10ms timer tickne
        { 
            data = serialport.ReadExisting(); //Čtení dat z portu a uložení do data
            label5.Text = data; //Zobrazení přijatých dat
            if (data!="")   //pokud není proměnná prázdná vykoná se blok
            {
                data = Convert.ToString(data);  //převod dat na string
                for (int i = 0; i < 10; i++)    //for pro hledani shody
                {
                    if (data == tab[i])         //pokud se shoduje prijate dato s tabulkovym datem vykonej příkaz if
                    {
                        vysledek = Convert.ToString(i); //převod indexu z tab na string, protože index=zobrazované číslo
                        label_zobraz.Text = vysledek;
                    }
                }
            }
        }

  
    }     
 }
            



