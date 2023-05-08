//Alfanumerick� displej - Nigrin Tom� A4
#include <stdio.h>
#include <conio.H>
#include <dos.H>
								//0x301 - DATA
								//0x300 - CLK
int tempstart;					//prom�nn� pro o�et�en� startbitu
int counter;					//counter pro numerick� re�im
int acounter;					//counter pro alfanumerick� re�im	
int index_radek;				//prom�nn� pro index ��dku v tabulce
int sloupec;					//prom�nn� pro index sloupce v tabulk�ch
int a,b,c,d;					//prom�nn� pro pomocn� tabulky

int tab1[15];					//pomocn� tabulka pro hodnoty 1. stisku
int tab2[15];					//pomocn� tabulka pro hodnoty 2. stisku
int	tab3[15];					//pomocn� tabulka pro hodnoty 3. stisku
int tab4[15];					//pomocn� tabulka pro hodnoty 4. stisku

char vypistab[26]={				//tabulka pro vyps�n� znak� na obrazovku (alfanum)
'A','B','C','D','E','F','G','H',
'I','J','K','L','M','N','O','P',
'Q','R','S','T','U','V','W','X',
'Y','Z'
};

int tab[10][7] = {				//tabulka pro numerick� re�im
{1,1,1,1,1,1,0}, //0
{0,1,1,0,0,0,0}, //1
{1,1,0,1,1,0,1}, //2
{1,1,1,1,0,0,1}, //3
{0,1,1,0,0,1,1}, //4
{1,0,1,1,0,1,1}, //5
{0,0,1,1,1,1,1}, //6
{1,1,1,0,0,0,0}, //7
{1,1,1,1,1,1,1}, //8
{1,1,1,0,0,1,1}, //9
};

int alftab[26][15]= {			 //tabulka pro alfanumerick� re�im
{1,1,1,0,1,1,0,0,1,0,0,0,1,0,0}, //a
{1,1,1,1,0,0,1,0,1,0,1,0,0,0,0}, //b
{1,0,0,1,1,1,0,0,0,0,0,0,0,0,0}, //c
{1,1,1,1,0,0,1,0,0,0,1,0,0,0,0}, //d
{1,0,0,1,1,1,0,0,1,0,0,0,1,0,0}, //e
{1,0,0,0,1,1,0,0,1,0,0,0,1,0,0}, //f
{1,0,1,1,1,1,0,0,1,0,0,0,0,0,0}, //g
{0,1,1,0,1,1,0,0,1,0,0,0,1,0,0}, //h
{1,0,0,1,0,0,1,0,0,0,1,0,0,0,0}, //i
{0,1,1,1,1,0,0,0,0,0,0,0,0,0,0}, //j
{0,0,0,0,1,1,0,1,0,1,0,0,1,0,0}, //k
{0,0,0,1,1,1,0,0,0,0,0,0,0,0,0}, //l
{0,1,1,0,1,1,0,1,0,0,0,0,0,1,0}, //m
{0,1,1,0,1,1,0,0,0,1,0,0,0,1,0}, //n
{1,1,1,1,1,1,0,0,0,0,0,0,0,0,0}, //o
{1,1,0,0,1,1,0,0,1,0,0,0,1,0,0}, //p
{1,1,1,1,1,1,0,0,0,1,0,0,0,0,0}, //q
{1,1,0,0,1,1,0,0,1,1,0,0,1,0,0}, //r
{1,0,1,1,0,0,0,0,1,0,0,0,0,1,0}, //s
{1,0,0,0,0,0,1,0,0,0,1,0,0,0,0}, //t (20 znak�)
{0,1,1,1,1,1,0,0,0,0,0,0,0,0,0}, //u
{0,0,0,0,1,1,0,1,0,0,0,1,0,0,0}, //v
{0,1,1,0,1,1,0,0,0,1,0,1,0,0,0}, //w
{0,0,0,0,0,0,0,1,0,1,0,1,0,1,0}, //x
{0,0,0,0,0,0,0,1,0,0,1,0,0,1,0}, //y
{1,0,0,1,0,0,0,1,0,0,0,1,0,0,0}, //z
};

void takt()  //zasunuti do posuv. registru		
{
	outportb(0x300, 0);
	outportb(0x300, 1);
}

void DATAalfa()													//nahrani dat do pomocnych tabulek
{
	if(acounter==1)												//pro prvni stisk
	{
		a=0;
		for(index_radek=0;index_radek<15;index_radek++)			//cyklus pro nahr�ni bit� z tabulky do pomocny tabulky
		{
			tab1[a] = alftab[sloupec][index_radek];
			a++;				
		}
		printf("%c",vypistab[sloupec]);							//vyps�n� stisknut�ho znaku na obrazovku
	}
	if(acounter==2)												//pro druhy stisk
	{
		a=0;
		for(index_radek=0;index_radek<15;index_radek++)			//cyklus pro nahr�ni bit� z tabulky do pomocny tabulky
		{
			tab2[a] = alftab[sloupec][index_radek];
			a++;				
		}
		printf("%c",vypistab[sloupec]);							//vyps�n� stisknut�ho znaku na obrazovku
	}
	
	if(acounter==3)			//pro treti stisk
	{
		a=0;
		for(index_radek=0;index_radek<15;index_radek++)			//cyklus pro nahr�ni bit� z tabulky do pomocny tabulky
		{
			tab3[a] = alftab[sloupec][index_radek];
			a++;				
		}
		printf("%c",vypistab[sloupec]);							//vyps�n� stisknut�ho znaku na obrazovku
	}
	if(acounter==4)			//pro ctvrty stisk
	{
		a=0;
		for(index_radek=0;index_radek<15;index_radek++)			//cyklus pro nahr�ni bit� z tabulky do pomocny tabulky
		{
			tab4[a] = alftab[sloupec][index_radek];
			a++;				
		}
		printf("%c",vypistab[sloupec]);							//vyps�n� stisknut�ho znaku na obrazovku
	}	
}
	
void DATA()														// vysilani bit� pro numericky displej
{
	for(index_radek=0;index_radek<7;index_radek++)			
	{
		outportb(0x301, tab[sloupec][index_radek]);				//vyps�ni bit� z tabulky po jednom -> pak takt
		takt();	
	}
}

void start()  													//vysl�ni startbitu	
{
	outportb(0x301, 1);
	takt();
}

void numerak()													//funkce pro numerick� re�im
{	
	printf("Byl vybran numericky displej, zkontrolujte jumper!\n");
	printf("Zadejte 5 cisel pro zobrazen�.(0-9)\n");
	
	while (1)  		
	{
		if (tempstart==0)  										//o�et�en� startbitu
		{
			tempstart=1;
			start();				
		}
		
		if (kbhit())											//pokud stisknuta klavesnice
		{			
			switch(getch())										//podle ASCII hodnot se vybere case
			{			
			case 48:			//0					
				sloupec = 0;									//nahr�n� hodnoty do prom�nn� pro tabulku
				counter++;										//inkrementace counteru
				DATA();											//vol�n� funkce
				break;								
			case 49:			//1
				sloupec=1;
				counter++;
				DATA();
				break;
			case 50:			//2
				sloupec=2;
				counter++;
				DATA();
				break;
			case 51:			//3
				sloupec=3;
				counter++;
				DATA();
				break;
			case 52:			//4
				sloupec=4;
				counter++;
				DATA();
				break;
			case 53:			//5
				sloupec=5;
				counter++;
				DATA();
				break;
			case 54:			//6
				sloupec=6;
				counter++;
				DATA();
				break;
			case 55:			//7
				sloupec=7;
				counter++;
				DATA();
				break;
			case 56:			//8
				sloupec=8;
				counter++;
				DATA();
				break;
			case 57:			//9
				sloupec=9;
				counter++;
				DATA();
				break;
			case 58:			//10
				sloupec=10;
				counter++;
				DATA();
				break;
			default:
			printf("Spatne zadany znak\n");	
			break;						
			}
			
			if(counter==5)								//counter=5 odpov�d� stisku kl�vesnice po 5
			{
				printf("\n");
				printf("--------------------------------------------------------------------------------\n");
				printf("Spravne nacteno \n");
				printf("--------------------------------------------------------------------------------\n");
				printf("Pokud chcete pokracovat v numerickem rezimu stisknete pismeno |N| \n");
				printf("Pokud chcete zmenit na alfanumericky rezim stiknete |A| \n");
				printf("--------------------------------------------------------------------------------\n");
				tempstart=0;							//nulov�n� prom�nn� pro startbit
				counter=0;								//nulov�n� counteru
				break;									//ukon�en� funkce "n�vrat" zp�t do main
			}
				
		}
	}
}

void alfanum()											//funkce pro alfanumerick� re�im
{
	printf("Byl vybran alfanumericky displej, zkontrolujte jumper!\n");
	printf("Zadejte 4 znaky pro zobrazeni.(A-Z)\n");
	printf("Pouzijte pouze velka pismena.\n");
	
	while (1)  
	{
		
		if (kbhit())									//pokud stisknuta kl�vesnice
		{
			switch(getch())								//podle ASCII hodnoty se vybere case		
			{			
			case 65:			//a						
				sloupec = 0;							//nahr�n� hodnoty do prom�nn� pro tabulku
				acounter++;								//inkrementace counteru pro alfanumericky displej
				DATAalfa();								//vol�n� funkce pro nahr�n� hodnot znaku do pomocn� tabulky
				break;
			case 66:			//b
				sloupec=1;
				acounter++;
				DATAalfa();
				break;
			case 67:			//c
				sloupec=2;
				acounter++;
				DATAalfa();
				break;
			case 68:			//d
				sloupec=3;
				acounter++;
				DATAalfa();
				break;
			case 69:			//e
				sloupec=4;
				acounter++;
				DATAalfa();
				break;
			case 70:			//f
				sloupec=5;
				acounter++;
				DATAalfa();
				break;
			case 71:			//g
				sloupec=6;
				acounter++;
				DATAalfa();
				break;
			case 72:			//h
				sloupec=7;
				acounter++;
				DATAalfa();
				break;
			case 73:			//i
				sloupec=8;
				acounter++;
				DATAalfa();
				break;
			case 74:			//j
				sloupec=9;
				acounter++;
				DATAalfa();
				break;
			case 75:			//j
				sloupec=10;
				acounter++;
				DATAalfa();
				break;
			case 76:			//k
				sloupec=11;
				acounter++;
				DATAalfa();
				break;
			case 77:			//m
				sloupec=12;
				acounter++;
				DATAalfa();
				break;
			case 78:			//n
				sloupec=13;
				acounter++;
				DATAalfa();
				break;
			case 79:			//o
				sloupec=14;
				acounter++;
				DATAalfa();
				break;
			case 80:			//p
				sloupec=15;
				acounter++;
				DATAalfa();
				break;
			case 81:			//q
				sloupec=16;
				acounter++;
				DATAalfa();
				break;
			case 82:			//r
				sloupec=17;
				acounter++;
				DATAalfa();
				break;
			case 83:			//s
				sloupec=18;
				acounter++;
				DATAalfa();
				break;
			case 84:			//t
				sloupec=19;
				acounter++;
				DATAalfa();
				break;
			case 85:			//u
				sloupec=20;
				acounter++;
				DATAalfa();
				break;
			case 86:			//v
				sloupec=21;
				acounter++;
				DATAalfa();
				break;
			case 87:			//w
				sloupec=22;
				acounter++;
				DATAalfa();
				break;
			case 88:			//x
				sloupec=23;
				acounter++;
				DATAalfa();
				break;
			case 89:			//y
				sloupec=24;
				acounter++;
				DATAalfa();
				break;
			case 90:			//z
				sloupec=25;
				acounter++;
				DATAalfa();
				break;
				
			default:
			printf("Spatne zadany znak\n");	
			break;						
			}
		}
					
		if(acounter==4)									//acounter=4 odpov�d� 4. stisku kl�vesnice
		{	start();									//vol�n� funkce pro vysl�n� startbitu
			for(a=0;a<15;a++)							//cyklus pro vysl�n� hodnot na port pro 1. pozici
			{
				outportb(0x301,tab1[a]);				//odesl�n� na port pomoc� pomocn� tabulky
				takt();
			}
			for(c=0;c<15;c++)							//cyklus pro vysl�n� hodnot na port pro 3. pozici
			{
				outportb(0x301,tab3[c]);				//odesl�n� na port pomoc� pomocn� tabulky
				takt();
			}
			outportb(0x301,1);							//o�et�en� aktivace displeje pro 1. a 3. m�sto
			takt();
			outportb(0x301,0);								
			takt();			
			takt();										//3x vol�n� funkce takt pro zapln�n� posuvn�ho registru
			takt();										//a n�sledn�mu odesl�n�.
			takt();	
//-------------------------------------------------------------------------							
			start();									//vol�n� funkce pro vysl�n� startbitu
			for(b=0;b<15;b++)							//cyklus pro vysl�n� hodnot na port pro 2. pozici
			{
				outportb(0x301,tab2[b]);				//odesl�n� na port pomoc� pomocn� tabulky
				takt();
			}					
			for(d=0;d<15;d++)							//cyklus pro vysl�n� hodnot na port pro 4. pozici
			{
				outportb(0x301,tab4[d]);				//odesl�n� na port pomoc� pomocn� tabulky
				takt();
			}
			
			outportb(0x301,0);								
			takt();
			outportb(0x301,1);							//o�et�en� aktivace displeje pro 2. a 4. m�sto
			takt();
				
			takt();										//3x vol�n� funkce takt pro zapln�n� posuvn�ho registru				
			takt();										//a n�sledn�mu odesl�n�.
			takt();	
		}	
		
		if(acounter==5) 								//acounter=5 odpov�d� 5. stisku kl�vesnice
		{
			printf("\n");
			printf("--------------------------------------------------------------------------------\n");
			printf("Pokud chcete pokracovat v alfanumerickem rezimu stisknete pismeno |A| \n");
			printf("Pokud chcete zmenit na numericky rezim stiknete |N| \n");
			printf("--------------------------------------------------------------------------------\n");
			acounter=0;
			break;		 								//ukon�en� funkce "n�vrat" zp�t do main
		}	
	}
}




int main()												
{ 
	printf("--------------------------------------------------------------------------------\n");
	printf("|Nigrin Tom� - A4|");
	printf("Pro jednodussi zachazeni s programem zapnete CapsLock.\n");
	printf("Vyberte displej:\n");
	printf("Pro alfanumericky displej stisknete A \n");
	printf("Pro numericky displej stisknete N \n");
	printf("--------------------------------------------------------------------------------\n");
	
	while(1)					//v�b�r re�imu numerick�/alfanumerick�
	{
		if (kbhit())			//A - 65  N - 78 -	pokud stiknuta kl�vesnice
		{
			switch(getch())		//podle ASCII hodnot v�b�r casu
			{
				case 78:		//N 
				numerak();		//vol�n� funkce pro numerick� re�im
				break;
				
				case 65:		//A 
				alfanum();		//vol�n� funkce pro alfanumerick�
				break;						
			}
		}
	}		
}
