using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Testing
{
    public partial class Form1 : Form
    {       
        int sek;
        int mode;
        int jednoducha = 1;
        int decider = 1;
        [DllImport("k8055d.dll")]
        public static extern int OpenDevice(int AdresaKarty);
        [DllImport("k8055d.dll")]
        public static extern void CloseDevice();
        [DllImport("k8055D.DLL")]
        public static extern void WriteAllDigital(int data);
        [DllImport("k8055D.DLL")]
        public static extern int SetCurrentDevice(int lngCardAddress);
        [DllImport("K8055D.dll")]
        public static extern bool ReadDigitalChannel(int Channel);


        public Form1()
        {
            InitializeComponent();
        }
        private void chodci_hlavni_green()  //funkce pro chodce na hlavní - zelená
        {
            hp1.BackColor = Color.Lime;
            hp2.BackColor = Color.Black;
            hp3.BackColor = Color.Black;
            hp4.BackColor = Color.Lime;
            hp5.BackColor = Color.Lime;
            hp6.BackColor = Color.Black;
            hp7.BackColor = Color.Black;
            hp8.BackColor = Color.Lime;
        }
        private void chodci_hlavni_red()    //funkce pro chodce na hlavní - červená
        {
            hp1.BackColor = Color.Black;
            hp2.BackColor = Color.Red;
            hp3.BackColor = Color.Red;
            hp4.BackColor = Color.Black;
            hp5.BackColor = Color.Black;
            hp6.BackColor = Color.Red;
            hp7.BackColor = Color.Red;
            hp8.BackColor = Color.Black;

        }
        private void chodci_vedlejsi_red()  //funkce pro chodce na vedlejší - červená
        {
            vp1.BackColor = Color.Red;
            vp2.BackColor = Color.Black;
            vp3.BackColor = Color.Red;
            vp4.BackColor = Color.Black;
            vp5.BackColor = Color.Black;
            vp6.BackColor = Color.Red;
            vp7.BackColor = Color.Black;
            vp8.BackColor = Color.Red;

        }
        private void red_all()             //Červená všude
        {
	    SetCurrentDevice(0);
	    WriteAllDigital(137);
	    SetCurrentDevice(1);
	    WriteAllDigital(10);
            h1.BackColor = Color.Red;
            h4.BackColor = Color.Red;
            h7.BackColor = Color.Red;
            h10.BackColor = Color.Red;
            v1.BackColor = Color.Red;
            v4.BackColor = Color.Red;
            hp2.BackColor = Color.Red;
            hp3.BackColor = Color.Red;
            hp6.BackColor = Color.Red;
            hp7.BackColor = Color.Red;
            vp1.BackColor = Color.Red;
            vp3.BackColor = Color.Red;
            vp6.BackColor = Color.Red;
            vp8.BackColor = Color.Red;
        }
        private void chodci_vedlejsi_green()   //funkce pro chodce na vedlejší - zelená
        {
            vp1.BackColor = Color.Black;
            vp2.BackColor = Color.Lime;
            vp3.BackColor = Color.Black;
            vp4.BackColor = Color.Lime;
            vp5.BackColor = Color.Lime;
            vp6.BackColor = Color.Black;
            vp7.BackColor = Color.Lime;
            vp8.BackColor = Color.Black;
        }
        private void easy_Click(object sender, EventArgs e)
        {
            if (mode == 1)              //pokud je zvolen režim den
            {
                jednoducha++;           //inkrementace        
                for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;  //vymazání všech stavů v simulaci
                timer1.Enabled = true;      //zapnutí timeru1
                mode = 1;                   //režim den
                sek = 0;                    //nastavení sekund
            }
            if (jednoducha % 2 == 0)        //if pro signalizaci pokud je zvoleno jednoducha
            {
                easy_signal.BackColor = Color.Lime;
                full_signal.BackColor = Color.Black;
            }
            else
            {
                easy_signal.BackColor = Color.Black;
                full_signal.BackColor = Color.Lime;
            }
        }
        private void dennoc_Click(object sender, EventArgs e)
        {           
            decider++;                  //inkrementace
            if(decider%2==0)            //pokud je decider sudý            
            {
                for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                mode = 1;               //den 
                timer1.Enabled = true;
                easy.Enabled = true;
                sek = 0;
                den_signalizace.BackColor = Color.Lime;
                noc_signalizace.BackColor = Color.Black;
            }
            else                       
            {
                mode = 2;               //noc
                timer1.Enabled = true;
                sek = 0;
                easy.Enabled = false;
                den_signalizace.BackColor = Color.Black;
                noc_signalizace.BackColor = Color.Lime;
                easy_signal.BackColor = Color.Black;
                full_signal.BackColor = Color.Black;
            }
        }
        private void start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {           
            if (ReadDigitalChannel(2) == false) //A     //pokud je stisknuto "A"
            {
                decider++;
                if (decider % 2 == 0)                   //pokud je decider sudý
                {
                    for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                    mode = 1;                           //den
                    sek = 0;
                    den_signalizace.BackColor = Color.Lime;
                    noc_signalizace.BackColor = Color.Black;
                }
                else
                {
                    for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                    mode = 2;
                    sek = 0;
                    den_signalizace.BackColor = Color.Black;
                    noc_signalizace.BackColor = Color.Lime;
                    easy_signal.BackColor = Color.Black;
                    full_signal.BackColor = Color.Black;
                }
            }        
            
            if (mode == 1)
            {
                if (ReadDigitalChannel(3) == false) //B
                {
                    for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                    jednoducha++;
                    if (jednoducha % 2 == 0)        //pokud je jednoducha sudý
                    {
                        easy_signal.BackColor = Color.Lime;
                        full_signal.BackColor = Color.Black;
                    }
                    else
                    {
                        easy_signal.BackColor = Color.Black;
                        full_signal.BackColor = Color.Lime;
                    }
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {      
            sek++;
            if (mode == 1)          //rezim den
            {
                switch (sek)        //timer tiká vykonávájí se casy dle sekund. 
                {
                    case 1:         
                        red_all();
                        break;
                    case 3:
                        if(jednoducha%2==0)     //pokud je rezim jednoduchá
                        {
				            SetCurrentDevice(0);
	    			        WriteAllDigital(141);
				            SetCurrentDevice(1);
	    			        WriteAllDigital(38);
                            h5.BackColor = Color.Yellow;
                            h11.BackColor = Color.Yellow;
                        }
			            SetCurrentDevice(0);
	    		        WriteAllDigital(141);
			            SetCurrentDevice(1);
	    		        WriteAllDigital(6);
                        h2.BackColor = Color.Yellow;
                        h8.BackColor = Color.Yellow;
                        chodci_vedlejsi_green();
                        break;
                    case 5:
                        if(jednoducha%2==0)     //pokud je rezim jednoduchá
                        {
			                SetCurrentDevice(0);
	    		            WriteAllDigital(130);
			                SetCurrentDevice(1);
	    		            WriteAllDigital(22);
                            h4.BackColor = Color.Black;
                            h10.BackColor = Color.Black;
                            h5.BackColor = Color.Black;
                            h11.BackColor = Color.Black;
                            h6.BackColor = Color.Lime;
                            h12.BackColor = Color.Lime;
                        }
			            SetCurrentDevice(0);
	    		        WriteAllDigital(131);
                        h1.BackColor = Color.Black;
                        h2.BackColor = Color.Black;
                        h3.BackColor = Color.Lime;
                        h7.BackColor = Color.Black;
                        h8.BackColor = Color.Black;
                        h9.BackColor = Color.Lime;
                        break;
                    case 9:
                        if (jednoducha % 2 == 0)        //pokud je rezim jednoduchá
                        {   SetCurrentDevice(0);
	    		            WriteAllDigital(132);
			                SetCurrentDevice(1);
	    		            WriteAllDigital(42);
                            h5.BackColor = Color.Yellow;
                            h11.BackColor = Color.Yellow;
                            h6.BackColor = Color.Black;
                            h12.BackColor = Color.Black;
                        }
			            SetCurrentDevice(0);
	    		        WriteAllDigital(133);
			            SetCurrentDevice(1);
	    		        WriteAllDigital(10);
                        h2.BackColor = Color.Yellow;
                        h3.BackColor = Color.Black;
                        h8.BackColor = Color.Yellow;
                        h9.BackColor = Color.Black;
                        chodci_vedlejsi_red();
                        break;
                    case 11:
                        if (jednoducha % 2 == 0)        //pokud je rezim jednoduchá
                        {
                            h5.BackColor = Color.Black;
                            h11.BackColor = Color.Black;
                            red_all();
                            sek = 20;
                        }
                        h2.BackColor = Color.Black;
                        h8.BackColor = Color.Black; 
                        red_all();
                        break;
                    case 12:			
			            SetCurrentDevice(1);
	    		        WriteAllDigital(42);
                        h5.BackColor = Color.Yellow;
                        h11.BackColor = Color.Yellow;
                        break;
                    //obr2
                    case 14:
			            SetCurrentDevice(0);
	    		        WriteAllDigital(168);
			            SetCurrentDevice(1);
	    		        WriteAllDigital(26);
                        vs1.BackColor = Color.Lime;
                        vs2.BackColor = Color.Lime;
                        h4.BackColor = Color.Black;
                        h10.BackColor = Color.Black;
                        h5.BackColor = Color.Black;
                        h11.BackColor = Color.Black;
                        h6.BackColor = Color.Lime;
                        h12.BackColor = Color.Lime;
                        break;
                    case 18:
			            SetCurrentDevice(0);
	    		        WriteAllDigital(136);
                        SetCurrentDevice(1);
                        WriteAllDigital(42);
                        vs1.BackColor = Color.Black;
                        vs2.BackColor = Color.Black;
                        h5.BackColor = Color.Yellow;
                        h11.BackColor = Color.Yellow;
                        h6.BackColor = Color.Black;
                        h12.BackColor = Color.Black;
                        break;
                    case 20:
                        h5.BackColor = Color.Black;
                        h11.BackColor = Color.Black;
                        red_all();
                        break;
                    case 21:
                        SetCurrentDevice(0);
                        WriteAllDigital(201);
                        SetCurrentDevice(1);
                        WriteAllDigital(9);
                        v5.BackColor = Color.Yellow;
                        v2.BackColor = Color.Yellow;
                        chodci_hlavni_green();
                        break;
                    case 23:                    //obr3
                        SetCurrentDevice(0);
                        WriteAllDigital(25);
                        v1.BackColor = Color.Black;
                        v2.BackColor = Color.Black;
                        v4.BackColor = Color.Black;
                        v5.BackColor = Color.Black;
                        v6.BackColor = Color.Lime;
                        v3.BackColor = Color.Lime;                      
                        break;
                    case 26:
                        SetCurrentDevice(0);
                        WriteAllDigital(73);
                        SetCurrentDevice(1);
                        WriteAllDigital(10);
                        v6.BackColor = Color.Black;
                        v5.BackColor = Color.Yellow;
                        v2.BackColor = Color.Yellow;
                        v3.BackColor = Color.Black;
                        chodci_hlavni_red();
                        break;
                    case 28:
                        v5.BackColor = Color.Black;
                        v2.BackColor = Color.Black;
                        red_all();
                        sek = 0;
                        break;
                }
            }

            else if (mode == 2)         //rezim noc
            {
                for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                if (sek % 2 == 0)
                {
                     SetCurrentDevice(0);
	                 WriteAllDigital(68);
	                 SetCurrentDevice(1);
	                 WriteAllDigital(32);
                    v2.BackColor = Color.Yellow;
                    v5.BackColor = Color.Yellow;
                    h2.BackColor = Color.Yellow;
                    h5.BackColor = Color.Yellow;
                    h8.BackColor = Color.Yellow;
                    h11.BackColor = Color.Yellow;
                }
                else
                {
                    SetCurrentDevice(0);
                    WriteAllDigital(0);
                    SetCurrentDevice(1);
                    WriteAllDigital(0);
                    for (int a = 0; a < 36; a++) all.Controls[a].BackColor = Color.Black;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //ovladani pres pc
        {
            timer2.Enabled = false;     //timer2 vypnut
            dennoc.Enabled = true;
            easy.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)  //ovladani pres modul
        {
            timer2.Enabled = true;      //timer2 zapnut
            dennoc.Enabled = false;
            easy.Enabled = false;

        }
    }
}
