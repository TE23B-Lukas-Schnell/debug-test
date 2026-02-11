global using Raylib_cs;
global using System;
global using System.Threading;
global using System.Threading.Tasks;

GibbManager.Setup();

while (true)
{
    GibbManager.ExecuteMenu(GibbManager.currentMenu);
}



//saker att fixa:

/*---------------------------------------------------------------------------------------------------------------------------------


gör så att player bullet flyger uppåt baserat på spelarens input

gör den indicator som visar vilket håll spelaren siktar åt 
gör en cirkel som visar hur lång reload spelaren har

fixa bättre interface med console specter någon gång


kanske borde göra animations spelet någon gång 🧐

GÖR så att spelaren kan skjuta åt båda håll,




*///---------------------------------------------------------------------------------------------------------------------------------


