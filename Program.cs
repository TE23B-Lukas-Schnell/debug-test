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

Gör så att initializeHitbox funktionen kan köras från moveableobjects constructorn, det är för de körs först och det gör att värdet är null
samma sak med hp = maxHP för alla fightable objects     ok båt      ig 




















*///---------------------------------------------------------------------------------------------------------------------------------


