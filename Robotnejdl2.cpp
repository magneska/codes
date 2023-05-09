#include <stdio.h>
#include <stdlib.h>
#include <conio.h>

void vystup(unsigned char Vystup)
{
    outportb(0x301,0);
    outportb(0x300,Vystup);
    outportb(0x301,1);
}
int main()
{
    int control[4][6]=
    {   //od  K  motor sensor  limit  akt
        {'A','B',0xFE,0X01,2300,0},
        {'W','S',0xFD,0X02,1300,0},
        {'F','R',0xFB,0X04,1700,0},
        {'Q','E',0xF7,0X08,520,0},
    };
    unsigned char input;
    unsigned char output=0xFF;
    unsigned char odzavory=0x10;
    unsigned char kzavore=0xEF;
    char klavesa;

    while(1)    //start nulove polohy
    {
        for(int i = 0; i<4; i++)
        {
            inputs=inportb(Ox300);
            inputs=inputs&control[i][3];
            if(inputs!=0)
                {
                    output=output&control[i][2];
                    vystup(output);
                    output=output|control[i][3];
                }
        }
        inputs=inportb(Ox300);
        inputs=inputs & 0XF
        if(inputs==0)
        {
            break;
        }

    }
    while(1)
    {
        klavesa = getch();
        printf("%c",klavesa);
        for(int i = 0; i<4; i++)
        {
            if (klavesa==control[i][0])
            {
                if(control[i][4]!=control[i][5])
                {
                    output=output&control[i][2];
                    output=output|odzavory;
                    vystup(output);
                    control[i][5]++;
                    output=output|control[i][3];
                }
            }
            else if(klavesa==control[i][1])
            {
                inputs=inportb(Ox300);
                inputs=inputs&control[i][3];
                if(inputs!=0)
                {
                    output=output&control[i][2];
                    output=output&kzavore;
                    vystup(output);
                    control[i][5]--;
                    output=output|control[i][3];
                }
            }
        }
    }
    return 0;
}
