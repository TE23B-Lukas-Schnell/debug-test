using System.Data;
using System.Net.WebSockets;

class ControlLayout
{
    public static List<ControlLayout> controlLayouts = new List<ControlLayout>();

    public Dictionary<string, KeyboardKey> keybinds = new Dictionary<string, KeyboardKey>();

    public string name;

    public string nameControlLayout()
    {
        Console.WriteLine("write the name of the control layout");
        string tryName = Console.ReadLine();

        //fixa riktig logik så att den inte har samma namn som en annan controlLayout någon gång
        /* while(tryName != "" || tryName == controlLayouts[0].name )
         {

         }*/
        return tryName;
    }

    public KeyboardKey bindKey()
    {
        int keyCode = Raylib.GetKeyPressed();

        while (keyCode == 0)
        {
            keyCode = Raylib.GetKeyPressed();
             Raylib.EndDrawing();
        }
        Console.WriteLine((KeyboardKey)keyCode);
        return (KeyboardKey)keyCode;
    }

    // inte färdig, använd inte
    Dictionary<string, KeyboardKey> CreateContolLayout()
    {
        int windowWidth = 1600;
        int windowHeight = 900;
        int textSize = 40;
        Dictionary<string, KeyboardKey> output = new Dictionary<string, KeyboardKey>();


        Raylib.InitWindow(windowWidth, windowHeight, "keybinding");
        bool completetdControlLayout = false;
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText("press your preferred up key", windowHeight / 2, windowWidth / 2, textSize, Color.Black);
            output.Add("up", bindKey());

            /* Raylib.DrawText("press your preferred up key", windowHeight / 2, windowWidth / 2, textSize, Color.Black);

             while (keyCode == 0)
             {
                 keyCode = Raylib.GetKeyPressed();
             }*/

            Raylib.EndDrawing();
        }
        Console.WriteLine(output["up"]);
        Console.WriteLine(output["down"]);

        return output;
    }

    public ControlLayout()
    {
        name = nameControlLayout();
        keybinds = CreateContolLayout();
        controlLayouts.Add(this);
    }

    public ControlLayout(Dictionary<string, KeyboardKey> premadeKeybinds, string name)
    {
        keybinds = premadeKeybinds;
        this.name = name;
        controlLayouts.Add(this);
    }
}