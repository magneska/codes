#include <stdio.h>
#include<dos.h>

int temp;
int outp2;
int tlac=1;
int tlacpred;
int status;
int tab[5]={0x01,0x02,0x04,0x08,0x10};

void start()
{
 outportb(0x300,0x02);
  while(1)
  {
   temp = inportb(0x301);
   temp = temp & 0x02;
   if(temp==0)
   {
     outportb(0x300,0xff);
     break;
   }
   outportb(0x300,0x00);
  }
}
int main()
{
 start();
 while(1)
 {
   if(status==0)
   {
    for(int a=0;a<4;a++)
    {
     temp = inportb(0x300);
     temp = temp & tab[a];
     if(temp==0)
     {
       tlacpred=tlac;
       tlac=a+1;
       status=1;
     }
    }
   }
  if(status==1)
  {
    printf("tlac predtim je %d\n",tlacpred);
    printf("tlac je %d\n",tlac);
   while(1)
   {
    temp = inportb(0x301);
    temp = temp & tab[tlac];
    if(temp==0)
    {
     outportb(0x300,0xff);
     break;
    }

   if(tlacpred<tlac)
   {
    outportb(0x300,0x02);//nahoru
    outp2 = 0xFD;
    outportb(0x301,outp2);
   }
   else
   {
    outportb(0x300,0x00);//dolu
    outportb(0x301,0XFE);
   }
   }
   status=0;
  }
 }
}