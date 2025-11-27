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

    Dictionary<string, KeyboardKey> CreateContolLayout()
    {
        Dictionary<string, KeyboardKey> output = new Dictionary<string, KeyboardKey>();

        Console.WriteLine("press your preferred up key");

        KeyboardKey getToSet = Raylib.
        while (Raylib.GetKeyPressed() == 0)
        {
            
        }


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