global using Raylib_cs;
global using System;

GibbManager.Setup();

ControlLayout båt = new ControlLayout();

GibbManager.WriteDictionary(båt.keybinds);

//main loop
GibbManager.GameLoop();