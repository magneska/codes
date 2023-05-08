using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tenis_nig
{
    public partial class Form1 : Form
    {   
        //proměnné
        bool doprava = true; 
        bool doleva;
        bool nahoru;
        bool dolu;
        bool inkrement;
        int skoremax;
        int obtiznost;
        int i = 0;
        int a = 0;
        int countdown = 3;
        int skoreprava = 0;
        int skoreleva = 0;
        //pole s hodnotami X
        int[] tabulka = new int[2] { 700, 30 }; //0 doprava 1 doleva
        int[] tabgol = new int[2] { 740, 0 }; //0 prava  1 leva
        //pole s hodnotami Y
        int[] tabmeze = new int[2] { 440, 0 }; //0 dole 1 nahore

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            popupokno();
        }

        private void popupokno()
        {
            PopUpForm popUp = new PopUpForm(); //vytvoření nove "proměnné"
            if (popUp.ShowDialog() == DialogResult.OK) //pokud popUp okno vrati DR.OK
            {
                label2.SendToBack();    //prostredni cara
                //ziskani hodnot proměnných z popup okna
                skoremax = popUp.Skoremax;  
                obtiznost = popUp.Rychlost_micku;
                inkrement = popUp.inkrement;
                skore_L.Text = "0";
                skore_R.Text = "0";
                label3.Text = "Skore pro výhru: " + skoremax;
                //nastaveni rychlosti mičku dle volby
                if (obtiznost == 0) clock.Interval = 50;
                if (obtiznost == 1) clock.Interval = 40;
                if (obtiznost == 2) clock.Interval = 30;
            }
            else this.Close(); //pokud popUp okno nevrati DR.OK program se vypne
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            int micekY = micek.Location.Y;
            int micekX = micek.Location.X;
            int Ylokace1 = 0;
            int hit = 0; 
            int hittop = 0;
            int hitmid = 0;
            int hitbot = 0;
            
            //Pohyb micku + ziskání hodnoty Y palky
            if (doprava == true)
            {
                micekX = micekX + 10;
                Ylokace1 = palka1.Location.Y;
            }
            if (doleva == true)
            {
                micekX = micekX - 10;
                Ylokace1 = palka2.Location.Y;           
            }
            if (nahoru == true)
            {
                micekY = micekY - 10;
            }
            if (dolu == true)
            {
                micekY = micekY + 10;
            }
            //Kontrola zdali micek narazil do horni/dolni casti hriste
            //Podle indexu "a" vime jakym smerem mic jede + kterou mez potrebujeme
            //a=1 nahoru | a=0 = dolu
            if (micekY == tabmeze[a]) 
            {
                switch (a)
                {
                    case 0:
                        dolu = false;
                        nahoru = true;
                        a = 1;
                        break;
                    case 1:
                        nahoru = false;
                        dolu = true;
                        a = 0;
                        break;
                }
            }
            //Kontrola zdali micek narazil do palky
            //Podle indexu "i" vime kterou mez potrebujeme
            if (micekX == tabulka[i])  //Je micek na urovni kde ma byt palka
            {
                for (int a = 0; a < 30; a++)    //kontrola narazu micku do palky
                {
                    if (micekY == Ylokace1 + a)
                    {
                        hittop++;
                        hit++;
                    }
                    if (micekY == Ylokace1+30 + a)
                    {
                        hitmid++;
                        hit++;
                    }
                    if (micekY == Ylokace1+60 + a)
                    {
                        hitbot++;
                        hit++;
                    }
                }
                if (hit == 1) //pokud se micek nachazel v miste palky hit=1
                {
                    switch (i)  //zmena chovani micku(odrazeni)
                    {
                        case 0:
                            doprava = false;
                            doleva = true;
                            i = 1;
                            break;
                        case 1:
                            doprava = true;
                            doleva = false;
                            i = 0;
                            break;
                    }
                    //kontrola jaké části pálky se míček dotkl
                    if (hittop == 1)
                    {
                        nahoru = true;
                        dolu = false;
                        a = 1;
                    }
                    if (hitmid == 1)
                    {
                        nahoru = false;
                        dolu = false;
                    }
                    if (hitbot == 1)
                    {
                        nahoru = false;
                        dolu = true;
                        a = 0;
                    }
                }
            }
            this.micek.Location = new Point(micekX, micekY);
            //Kontrola zdali micek proletel za palku
            //Podle indexu "i" vime kterou mez potrebujeme
            if (micekX == tabgol[i])
            {
                this.micek.Location = new Point(350, 200); //vraceni micku na vychozi pozici
                countdown = 3;
                clock.Enabled = false;
                start.Enabled = true;
                //pokud bylo zvoleno se zvysovanim skore zrychlovat micek
                if (inkrement == true)
                {
                    if (clock.Interval > 25)
                    {
                        for(int r =0;r<2;r++)
                        if (obtiznost == r) clock.Interval = clock.Interval - 5;
                        if (obtiznost == 2) clock.Interval = clock.Interval - 1;
                    }
                }
                //pricteni skore a vypsání stavu.
                switch (i)
                {
                    case 0:
                        label1.Text = "Bod pro hráče 1!";
                        skoreleva++;
                        skore_L.Text = skoreleva.ToString();
                        break;
                    case 1:
                        label1.Text = "Bod pro hráče 2!";
                        skoreprava++;
                        skore_R.Text = skoreprava.ToString();
                        break;
                }
                //kontrola zdali hrac1-2 nevyhral.
                //pokud ano - vymazani proměnných pro skore, zobrazeni messageboxu, otevreni popUp okna pro nastaveni nove hry
                if (skoremax == skoreleva)
                {
                    skoreleva = 0;
                    skoreprava = 0;
                    MessageBox.Show("WINNER WINNER CHICKEN DINNER! Hráč 1 vyhrál!");
                    popupokno();
                }
                if (skoremax == skoreprava)
                {
                    skoreleva = 0;
                    skoreprava = 0;
                    MessageBox.Show("WINNER WINNER CHICKEN DINNER! Hráč 2 vyhrál!");
                    popupokno();
                }
            }               
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e) //Pohyb pálek
        {
            //Zjisteni hodnot X,Y pálek
            int Ylokace1 = palka1.Location.Y;
            int Xlokace1 = palka1.Location.X;
            int Ylokace2 = palka2.Location.Y;
            int Xlokace2 = palka2.Location.X;

            //Kontrola stisku tlačítek Num2,Num8,S,W
            if (e.KeyCode == Keys.NumPad2)
            {
                if (!(Ylokace1 >= 350)) //hranice palky
                {
                    //pohyb dané pálky
                    Ylokace1 = Ylokace1 + 30;
                    this.palka1.Location = new Point(Xlokace1, Ylokace1);
                    Ylokace1 = Ylokace1 + 30;
                    this.palka1mid.Location = new Point(Xlokace1, Ylokace1);
                    Ylokace1 = Ylokace1 + 30;
                    this.palka1bot.Location = new Point(Xlokace1, Ylokace1);
                }
            }
            if (e.KeyCode == Keys.NumPad8)
            {
                if (!(Ylokace1 <= 0))
                {
                    Ylokace1 = Ylokace1 - 30;
                    this.palka1.Location = new Point(Xlokace1, Ylokace1);
                    Ylokace1 = Ylokace1 + 30;
                    this.palka1mid.Location = new Point(Xlokace1, Ylokace1);
                    Ylokace1 = Ylokace1 + 30;
                    this.palka1bot.Location = new Point(Xlokace1, Ylokace1);
                }
            }
            if (e.KeyCode == Keys.S)
            {
                if (!(Ylokace2 >= 350))
                {
                    Ylokace2 = Ylokace2 + 30;
                    this.palka2.Location = new Point(Xlokace2, Ylokace2);
                    Ylokace2 = Ylokace2 + 30;
                    this.palka2mid.Location = new Point(Xlokace2, Ylokace2);
                    Ylokace2 = Ylokace2 + 30;
                    this.palka2bot.Location = new Point(Xlokace2, Ylokace2);
                }
            }
            if (e.KeyCode == Keys.W)
            {
                if (!(Ylokace2 <= 0))
                {
                    Ylokace2 = Ylokace2 - 30;
                    this.palka2.Location = new Point(Xlokace2, Ylokace2);
                    Ylokace2 = Ylokace2 + 30;
                    this.palka2mid.Location = new Point(Xlokace2, Ylokace2);
                    Ylokace2 = Ylokace2 + 30;
                    this.palka2bot.Location = new Point(Xlokace2, Ylokace2);
                }
            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            odpocet.Enabled = true;
        }

        private void odpocet_Tick(object sender, EventArgs e)
        {
            //zobrazeni odpoctu a zapnutí hlavniho timeru(hry)
            label1.Text = countdown.ToString();
            if (countdown == 0)
            {
                odpocet.Enabled = false;
                clock.Enabled = true;
                start.Enabled = false;
                label1.Text = "";
            }
            countdown--;
        }
    }
}
