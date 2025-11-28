global using Raylib_cs;
global using System;

GibbManager.Setup();

// fixa så att spelarens control layout manager
ControlLayout båt = new ControlLayout();

GibbManager.WriteDictionary(båt.keybinds);

//main loop
GibbManager.GameLoop();