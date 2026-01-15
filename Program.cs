global using Raylib_cs;
global using System;
global using System.Threading;
global using System.Threading.Tasks;

GibbManager.Setup();

while (true)
{
    GibbManager.ExecuteMenu(GibbManager.currentMenu);
}


//frågor till micke 

// menyer med while loopar i while loopar

// kan man göra attacker bättre

// hur gör man en bra game manager och hantera att man stänger raylib

// hur fixar man paus och task.delay

// kan han klara karim
/*
void köttigSpel(int åt)
{
    //köttig
}
delegate void Båtigtvärre();

Båtigtvärre båt = köttigSpel;

båt();

// båt += båtiggSpel;

// båt += köttigSpel;


void båtiggSpel(int åt)
{
    //köttig
}*/
