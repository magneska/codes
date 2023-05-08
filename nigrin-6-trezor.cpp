//Trezor - Nigrin Tomáš A4
#include <stdio.h>
#include <dos.h>

#define port1 0x300
#define port2 0x301

int pozice[4]={0xFE,0xFD,0xFB,0xF7};	//tabulka pro pozice
int cisla[10]={0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09}; //kombinace pro zobrazeni cisel
int tabulka[4];	//tabulka pro zadani hesla
int pokushesla[4];	//tabulka pro otevreni

int nastaveni=1;	//promenna pro rezim zadavani kodu
int stisknuto;
int unlock;			//promenna pro rezim otevirani trezoru
int i;
int index;			//aktualni pozice displeje
int indexcis;		//index cisel zobrazovanych na displeji
int temp;

void start()		//podprogram pro cekani na stisk "SET"
{	
	stisknuto=1;
	i=0;
	while(stisknuto)
	{
		outportb(port1,pozice[i]);	//nastaveni aktivni pozice
		outportb(port2,0x1E);		//zobrazeni "-"
		i++;
		if(i==4)
		{
			temp = inportb(port2);	//cteni pinu klavesnice
			i=0;
			if(temp & 0x01 == 0)	//maska pro 0 bit
			{
				delay(300);			//dellay pro zákmit tlacitka
				stisknuto=0;
			}
		}
	}
}
void open()			//podprogram pro kontrolu hesel
{
	for(int a=0;a<4;a++)	
	{
		if(tabulka[a]==pokushesla[a]) vysledek++;	//porovnani obou kodu 
	}		
	if(vysledek==4)				//pokud se 4x shoduji = kod je spravne
	{
		printf("Spravny kod\n");
		outportb(port1,0xEF);	//otevreni trezoru
		nastaveni=1;			//aktivace rezimu pro zadani noveho kodu
	}	
	else
	{
		printf("Spatny kod\n");
		unlock=1;				//aktivace rezimu pro otevreni trezoru
		nastaveni=0;	
	}
	for(int a=0;a<4;a++) pokushesla[a]=0;	//nulovani pole pro zadavane heslo
}

int stisk(int i)	//hodnota i odpovídá aktualne aktivni pozici
{
	if(i==1) //sipka nahoru
	{
		if(!(indexcis>=9))indexcis++;		//kontrola preteceni tabulky
		if(nastaveni==1)tabulka[index] = cisla[indexcis];
		if(unlock==1)pokushesla[index] = cisla[indexcis]; 
	}
	if(i==2) //sipka dolu
	{
		if(!(indexcis<=0))indexcis--;		//kontrola podteceni tabulky
		if(nastaveni==1)tabulka[index] = cisla[indexcis];
		if(unlock==1)pokushesla[index] = cisla[indexcis];
	}
	if(i==3) //SET
	{
		index++;		//posunutí aktivni pozice pro zadavani kodu
		if(index==4) 	//kod zadan
		{
			if(unlock==1)	//rezim otevirani trezoru
			{
				unlock=0;
				open();
			}
			if(nastaveni==1)	//rezim zadavani kodu
			{
				printf("Kod nastaven.\n");
				printf("Pro rezim otevreni trezoru stisknete SET.\n");
				nastaveni=0;
				unlock=1;
				start();
			}
			indexcis=0;
			index=0;		
		}
	}
}

int main (void)
{
	printf("Pro zadani noveho kodu stiknete SET.\n");
	start();
	printf("Zadejte novy kod\n");
	while(1)
	{
		if(inportb(port1)& 0x01 == 0) //dvirka closed
		{
			outportb(port1,pozice[i]);	//aktivace pozice
			temp = inportb(port2);		//cteni klavesnice
			if(temp & 0x01 == 0) 		//maska pro 0 bit
			{
				delay(300);				//osetreni zakmitu
				stisk(i);				
			}
			outportb(port2,tabulka[i]);	//zobrazeni cisla na displeji
			i++;
			if(i==4) i=0;		
		}		
	}	
}

