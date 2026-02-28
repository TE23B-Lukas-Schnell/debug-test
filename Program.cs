global using Raylib_cs;
global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Text.Json;
global using System.Text.Json.Serialization;


GibbManager.Setup();

while (true)
{
    GibbManager.ExecuteMenu(GibbManager.currentMenu);
}


//saker att fixa:

/*---------------------------------------------------------------------------------------------------------------------------------
fixa så att hitbox debug funkar på bullets

lära sig lerp

lägg till up shoot makro

gör en cirkel som visar hur lång reload spelaren har

fixa bättre interface med console specter någon gång

kanske borde göra animations spelet någon gång 🧐

fixa trail funktionen någon gång


*///---------------------------------------------------------------------------------------------------------------------------------