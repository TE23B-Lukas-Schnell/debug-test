class ControlLayout
{
    public static List<ControlLayout> controlLayouts = new List<ControlLayout>();

    public Dictionary<string, KeyboardKey> keybinds = new Dictionary<string, KeyboardKey>();

    public string name;

    string nameControlLayout()
    {
        Console.WriteLine("write the name of the control layout");
        string tryName = Console.ReadLine();

        //fixa riktig logik så att den inte har samma namn som en annan controlLayout någon gång
        /* while(tryName != "" || tryName == controlLayouts[0].name )
         {

         }*/
        return tryName;
    }

    KeyValuePair<string, KeyboardKey> BindKey(string desiredKey)
    {
        int windowWidth = 1000;
        int windowHeight = 600;
        int textSize = 40;
        KeyboardKey checkKey = KeyboardKey.Null;
        Raylib.InitWindow(windowWidth, windowHeight, "keybinding");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText("press your preferred " + desiredKey + " key", windowWidth / 10, windowHeight / 2, textSize, Color.Black);

            checkKey = (KeyboardKey)Raylib.GetKeyPressed();
            if (checkKey != KeyboardKey.Null)
            {
                break;
            }

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
        return new KeyValuePair<string, KeyboardKey>(desiredKey, checkKey);
    }

    Dictionary<string, KeyboardKey> CreateContolLayout()
    {
        Dictionary<string, KeyboardKey> output = new Dictionary<string, KeyboardKey>();

        KeyValuePair<string, KeyboardKey> upBinding = BindKey("up");
        output.Add(upBinding.Key, upBinding.Value);
        Console.WriteLine("up key was binded to " + upBinding.Value.ToString());

        KeyValuePair<string, KeyboardKey> downBinding = BindKey("down");
        output.Add(downBinding.Key, downBinding.Value);
        Console.WriteLine("down key was binded to " + downBinding.Value.ToString());

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

    public static void PrintControlLayout(ControlLayout controlLayout)
    {
        if (controlLayout != null)
        {
            Console.WriteLine(controlLayout.name);
            foreach (KeyValuePair<string, KeyboardKey> entry in controlLayout.keybinds)
            {
                Console.WriteLine($"action: {entry.Key}, keybind: {entry.Value}");
            }
        }
        else
        {
            Console.WriteLine("empty keybinds");
        }
    }
}