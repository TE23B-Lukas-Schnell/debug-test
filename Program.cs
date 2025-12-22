global using Raylib_cs;
global using System;

Raylib.SetTraceLogLevel(TraceLogLevel.Error); // gör så att raylib inte skriver till konsolen

// while(1==1)
// GibbManager.GetIntFromConsole(0,5);

GibbManager.Setup();

// fixa getint from console inte får rätt index på båtiga ställen
GibbManager.MainMenu();

//main loop
GibbManager.GameLoop();

Console.Clear();