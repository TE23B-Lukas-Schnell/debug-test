global using Raylib_cs;
global using System;
global using System.Threading;
global using System.Threading.Tasks;

GibbManager.Setup();

while (true)
{
    GibbManager.ExecuteMenu(GibbManager.currentMenu);
}