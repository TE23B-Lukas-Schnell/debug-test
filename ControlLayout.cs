class ControlLayout
{
    public static List<ControlLayout> controlLayouts = new List<ControlLayout>();

    public Dictionary<string, KeyboardKey> keybinds = new Dictionary<string, KeyboardKey>();

    public string name;

    public void PrintControlLayout()
    {
        Console.WriteLine(name);
        foreach (KeyValuePair<string, KeyboardKey> entry in keybinds)
        {
            Console.WriteLine($"action: {entry.Key}, keybind: {entry.Value}");
        }
    }

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

    void CreateKeyBind(string action, Dictionary<string, KeyboardKey> båtig)
    {
        KeyValuePair<string, KeyboardKey> actionBinding = BindKey(action);
        båtig.Add(actionBinding.Key, actionBinding.Value);
        Console.WriteLine(action + " key was binded to " + actionBinding.Value.ToString());
    }

    Dictionary<string, KeyboardKey> CreateContolLayout(string[] actionsToBind)
    {
        Dictionary<string, KeyboardKey> output = new Dictionary<string, KeyboardKey>();

        for (int i = 0; i < actionsToBind.Length; i++)
        {
            CreateKeyBind(actionsToBind[i], output);
        }
        return output;
    }

    public ControlLayout(string[] actionsToBind)
    {
        name = nameControlLayout();
        keybinds = CreateContolLayout(actionsToBind);
        controlLayouts.Add(this);
    }

    public ControlLayout(Dictionary<string, KeyboardKey> premadeKeybinds, string name)
    {
        keybinds = premadeKeybinds;
        this.name = name;
        controlLayouts.Add(this);
    }
}