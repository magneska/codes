.NOLIST 
.INCLUDE "m128def.inc"
.LIST

//Definice 
.DEF temp = R16
.EQU baud = 95 ;baud_rate 9600
.CSEG
.ORG 0x0000 JMP INIT


INIT:
//Nastaveni zasobniku
	LDI temp, LOW(RAMEND)
	STS SPL, temp
	LDI temp, HIGH(RAMEND)
	OUT SPH, temp

//Nastaveni usartu
	LDI temp,low(baud)
	STS UBRR1L, temp
	LDI temp,high(baud)	
	STS UBRR1H, temp
                
	LDI temp, 0b00011000   //TXENn: Transmitter Enable 
	STS UCSR1B, temp
	LDI temp, 0b00000110   //Nastaveni size 8 bit
	STS UCSR1C, temp

//Nastaveni periferie
	LDI temp, 0X0F
	OUT DDRB, temp

START:  
;Prvni sloupec
	LDI temp, 0B11111110	//Aktivace sloupce/øádku
	OUT PORTB, temp
	RCALL DELAY
		IN R19, PINB		//Vstup z klávesnice
		CPI R19, 0B11101110	//Porovnávání hodnoty ze vstupu
		BREQ Jedna			//Z flag => skok na Jedna
		CPI R19, 0B11011110
		BREQ Ctyr
		CPI R19, 0B10111110
		BREQ Sedm
	RJMP LOOP2

LOOP2: 
;Druhej sloupec
	LDI temp, 0B11111101
	OUT PORTB, temp
	RCALL DELAY
		IN R19, PINB
		CPI R19, 0B11101101
		BREQ Dva
		CPI R19, 0B11011101
		BREQ Pet
		CPI R19, 0B10111101
		BREQ Osm
		CPI R19, 0B01111101
		BREQ Nula
	RJMP LOOP3

LOOP3: 
;Treti sloupec
	LDI temp, 0B11111011
	OUT PORTB, temp
	RCALL DELAY
		IN R19, PINB
		CPI R19, 0B11101011
		BREQ Tri
		CPI R19, 0B11011011
		BREQ Sest
		CPI R19, 0B10111011
		BREQ Devet
	RJMP start

Jedna:
	LDI R18, 0X01     
	RJMP Send
Dva:
	LDI R18, 0X02
	RJMP Send
Tri:
	LDI R18, 0X03
	RJMP Send
Ctyr:
	LDI R18, 0X04
	RJMP Send
Pet:
	LDI R18, 0X05
	RJMP Send
Sest:
	LDI R18, 0X06
	RJMP Send
Sedm:
	LDI R18, 0X07
	RJMP Send
Osm:
	LDI R18, 0x08
	RJMP Send
Devet:
	LDI R18, 0x09
	RJMP Send
Nula:
	LDI R18, 0X00

Send:
	LDS temp,UCSR1A
    ANDI temp,0x20 	//UDRE1=1 =>prazdny buffer, maska AND
    CPI temp,0		//Pokud je 0 skok na SEND(èekame na prazdny buffer)
	BREQ Send
	STS UDR1, R18	//Uložení hodnoty do UDR1
	JMP START		//Skok na start - cykleni programu

Delay:	
;500us	
    LDI  R25, 11
    LDI  R23, 99
L2: DEC  R23
    BRNE L2
    DEC  R25
    BRNE L2
RET   
