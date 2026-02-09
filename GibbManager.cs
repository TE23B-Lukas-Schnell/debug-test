static class GibbManager
{
    public static Color backgroundColor = Color.White;
    public static int targetFrameRate;
    public static bool currentlyGibbing = false;
    public static bool fullscreen = false;
    public static Menu currentMenu = mainMenu;
    public static Run? currentRun = null;

    static string scoreFilePath = "./scores.txt";
    static Dictionary<string, int> highscores = new Dictionary<string, int>();

    //control layouts
    public static ControlLayout defaultKeybindsWASD = new ControlLayout(new Dictionary<string, KeyboardKey>()
    {
        {"up", KeyboardKey.W},{"down",KeyboardKey.S},{"left", KeyboardKey.A},{"right",KeyboardKey.D},
        {"jump", KeyboardKey.Space },{"dash", KeyboardKey.LeftShift}, {"shoot",KeyboardKey.L}
    }, "wasd");

    public static ControlLayout defaultKeybindsArrowKeys = new ControlLayout(new Dictionary<string, KeyboardKey>()
    {
        {"up", KeyboardKey.Up},{"down",KeyboardKey.Down},{"left", KeyboardKey.Left},{"right",KeyboardKey.Right},
        {"jump", KeyboardKey.Z}, {"dash", KeyboardKey.C}, {"shoot", KeyboardKey.X}
    }, "arrow keys");

    public static ControlLayout currentLayout = defaultKeybindsWASD;

    readonly public static List<Type> PeakBossPeakBoss = new()
    {
        typeof(Karim), typeof(Karim), typeof(Karim), typeof(Karim), typeof(Karim)
    };

    readonly public static List<Item> allItems = new List<Item>()
    {
      Item.GetItem("Mickes hjälp"),
      Item.GetItem("Martins fönster öppnare"),
      Item.GetItem("Skolmaten"),
      Item.GetItem("Kemibok"),
      Item.GetItem("Calles krona"),
    };

    //local menu variables
    static int SetSeed;
    static List<Type> bossList = PeakBossPeakBoss;
    static List<Item> itemList = allItems;

    //action menus
    static Menu mainMenu = new Menu("main", new Dictionary<string, Action>()
    {
        {"Start playing", () => currentMenu = configureRunMenu},
        {"Show high scores", () =>  WriteDictionary(highscores)},
        {"select controll layout", () => currentMenu = controlMenu},
        {"quit game", () => {Console.WriteLine("quitting game");}},
    });

    static Menu controlMenu = new("controls", new Dictionary<string, Action>()
    {
        {"select control layout", SelectControlLayout},
        {"create control layout", () => new ControlLayout(Player.keyPressed.Keys.ToArray())},
        {"go back to main menu", () => currentMenu = mainMenu},
    });

    static Menu configureRunMenu = new Menu("configure", new Dictionary<string, Action>()
    {
        {"Start run", () => {StartRun(new Run(SetSeed,bossList,itemList));}},
        {"Change seed", () =>
            {
                Console.WriteLine("Enter your seed");
                SetSeed = GetIntFromConsole();
            }
        },
        {"Change available items (not implemented)", () => WriteDictionary(highscores)},
        {"Change available bosses (not implemented)", () => WriteDictionary(highscores)},
    });

    static Menu gameMenu = new("game", new Dictionary<string, Action>()
    {
        {"Next boss", () => {
            currentRun.GibbigtVärre();
            if(currentRun.deadRun)
            {
                currentRun = null;
                currentMenu = mainMenu;
            }
        }},
        {"Show your score", () =>  Console.WriteLine($"Your score is: {currentRun.playerReference.score}")},
        {"Show player stats", () =>  currentRun.playerReference.PrintPlayerStats()},
        {"Show bosses", () => currentRun.ShowBosses()},
        {"Show items left", () => currentRun.ShowAvailableitems()},
        {"Show seed", () =>   Console.WriteLine(currentRun.seed)},
        });

    public static int GetIntFromConsole()
    {
        int output;
        while (!int.TryParse(Console.ReadLine(), out output))
        {
            Console.WriteLine("input is not a integer");
        }
        return output;
    }

    public static int GetIntFromConsole(int minValue, int maxValue)
    {
        int output;
        while (1 == 1)
        {
            if (!int.TryParse(Console.ReadLine(), out output)) Console.WriteLine("input is not a integer");
            else
            {
                if (output > maxValue)
                {
                    Console.WriteLine("input is too big");
                }
                else if (output < minValue)
                {
                    Console.WriteLine("input is too small");
                }
                else return output;
            }

        }
    }

    public static void WriteDictionary(Dictionary<string, int> dictionary)
    {
        if (dictionary != null)
        {
            foreach (KeyValuePair<string, int> entry in dictionary)
            {
                Console.WriteLine($"Name: {entry.Key}, Score: {entry.Value}");
            }
        }
    }

    public static string ListToString(List<Boss> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += list[i].name + " ";
        }
        if (output == "") return "empty list";
        else return output;
    }

    public static string ListToString(List<Item> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += list[i].name + " ";
        }
        if (output == "") return "empty list";
        else return output;
    }

    public static string ListToString(List<MoveableObject> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += list[i] + " ";
        }
        if (output == "") return "empty list";
        else return output;
    }

    static int ChooseFPS()
    {
        Console.WriteLine("How much FPS do you want? (0 for uncapped)\nrecommended: 120 to 600");

        targetFrameRate = GetIntFromConsole(0, int.MaxValue);
        return targetFrameRate;
    }

    static string[] ReadSaveFile(string filePath)
    {
        if (filePath != null)
        {
            string content = File.ReadAllText(filePath);
            string[] dividedContent = content.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return dividedContent;
        }
        return ["köttigaste mikael", "10000", "anton", "5", "JackJackPegasusErgoLibraOndJävelSomVillFörstöraVärldenPegasusIgenAstroNovaMetiore", "-5"];
    }

    static Dictionary<string, int> ReadSaveData(string[] saveData)
    {
        Dictionary<string, int> highscores = new Dictionary<string, int>();

        for (int i = 0; i < saveData.Length; i += 2)
        {
            int score = 0;
            if (i + 1 < saveData.Length)
            {
                if (!int.TryParse(saveData[i + 1], out score))
                {
                    Console.WriteLine("your savedata is corrupt!!!11");
                }
            }

            highscores.Add(saveData[i], score);
        }
        return highscores;
    }

    static void LoadSave()
    {
        highscores = ReadSaveData(ReadSaveFile(scoreFilePath));
    }

    static void SaveGame()
    {
        throw new NotImplementedException();
    }

    public static void Setup()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Error); // gör så att raylib inte skriver till konsolen
        LoadSave();
        Raylib.SetTargetFPS(ChooseFPS());
        // Intructions();

        currentMenu = mainMenu;
    }

    public static void ExecuteMenu(Menu menu)
    {
        Console.WriteLine("--------------------" + menu.name + "--------------------------------------------");

        Console.WriteLine("choose an action");

        string[] actions = menu.menuActions.Keys.ToArray();
        for (int i = 0; i < actions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {actions[i]}");
        }
        menu.menuActions[actions[GetIntFromConsole(1, actions.Length) - 1]]();
    }

    static void SelectControlLayout()
    {
        for (int i = 0; i < ControlLayout.controlLayouts.Count; i++)
        {
            Console.Write(i + 1 + ": ");

            //måste bestämma om keybinds ska visas när man väljer control layout
            Console.WriteLine(ControlLayout.controlLayouts[i].name);
            // ControlLayout.controlLayouts[i].PrintControlLayout();
        }

        currentLayout = ControlLayout.controlLayouts[GetIntFromConsole(1, ControlLayout.controlLayouts.Count) - 1];
        Console.WriteLine("Your new control layout is " + currentLayout.name);
    }

    static void StartRun(Run run)
    {
        currentRun = run;
        currentMenu = gameMenu;
        currentRun.playerReference = new CallePlayer(GibbManager.currentLayout);
        currentRun.playerReference.InitializePlayer();
    }
}