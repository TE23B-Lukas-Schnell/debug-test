global using Raylib_cs;
global using System;

GibbManager.Setup();

// ControlLayout båt = new ControlLayout(GibbManager.playerReference.keyPressed.Keys.ToArray());

// ControlLayout.PrintControlLayout(båt);

// fixa getint from console inte får rätt index på båtiga ställen
GibbManager.MainMenu();

//main loop
GibbManager.GameLoop();

Console.Clear();