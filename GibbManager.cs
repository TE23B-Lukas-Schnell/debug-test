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


    public struct Settings
    {
        [JsonInclude] public bool enableDamageNumbers;
        // [JsonInclude] public Color playerColor;
        [JsonInclude] public int fps;

    }

    public static Settings currentSettings;

    static void SaveSettings(Settings settings)
    {
        string path = "settings.json";

        string text = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(path, text);
        System.Console.WriteLine("settings saved");
        System.Console.WriteLine(text);
    }

    static Settings LoadSettings(string path)
    {
        if (File.Exists("settings.json"))
        {
            string data = File.ReadAllText(path);
            System.Console.WriteLine("file exists");
            System.Console.WriteLine(data);
            return JsonSerializer.Deserialize<Settings>(data);
            
        }
        else
        {
             System.Console.WriteLine("file doesnt exist");
            return new()
            {
                enableDamageNumbers = true,
                // playerColor = new Color(0, 0f, 235f, 254f)  
            };
        }

    }


    readonly public static List<Type> PeakBossPeakBoss = new()
    {
        typeof(CalleBoss), typeof(ChristianBoss), typeof(KarimBoss)
    };

    readonly public static List<Item> allItems = new List<Item>()
    {
        Item.GetItem("Mickes hjälp"),
        Item.GetItem("Martins fönster öppnare"),
        Item.GetItem("Tung väska"),
        // Item.GetItem("Kemibok"),
        Item.GetItem("Calles krona"),
        Item.GetItem("Smutje.se"),
        Item.GetItem("Anton Faren"),
        Item.GetItem("Maxburgare"),
        Item.GetItem("Tu's Genomgång"),
        Item.GetItem("Skolmaten"),
        Item.GetItem("Mobil låda"),
        Item.GetItem("Internationella relations klubben"),

        // Item.GetItem("Y8"),
       
        // Item.GetItem("Erikas Waifu Köttbåt")
    };

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

    //local menu variables
    static int SetSeed;
    static List<Type> bossList = PeakBossPeakBoss;
    static List<Item> itemList = allItems;
    static Player playerCharacter = new MickePlayer(currentLayout);

    //action menus
    static Menu mainMenu = new Menu("main", new Dictionary<string, Action>()
    {
        {"Start playing", () => currentMenu = configureRunMenu},
        {"Show high scores", () =>  WriteDictionary(highscores)},
        {"Select control layout", () => currentMenu = controlMenu},
        {"Change settings", () => currentMenu = settingsMenu},
        {"Quit game", () => {
            Console.WriteLine("quitting game");
            StartRun(new Run(SetSeed,bossList,itemList));
        }}
    });

    static Menu controlMenu = new("controls", new Dictionary<string, Action>()
    {
        {"Select control layout", SelectControlLayout},
        {"Create control layout", () => new ControlLayout(Player.keyPressed.Keys.ToArray())},
        {"Go back to main menu", () => currentMenu = mainMenu},
    });

    static Menu settingsMenu = new("settings", new Dictionary<string, Action>()
    {
        {"Enable damage numbers", EnableDamageNumbers},
        {"Change player color", () => EnableDamageNumbers()},
        {"Set FPS",ChooseFPS},
        {"Go back to main menu", () => {currentMenu = mainMenu; SaveSettings(currentSettings);}},
    });

    static Menu configureRunMenu = new Menu("configure", new Dictionary<string, Action>()
    {
        {"Start run", () => {StartRun(new Run(SetSeed,bossList,itemList));}},
        {"Choose Character",() => {SelectCharacter();}},
        {"Change seed", () =>{Console.WriteLine("Enter your seed"); SetSeed = GetIntFromConsole();}},
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
        {"Show your score", () => Console.WriteLine($"Your score is: {currentRun.playerReference.score}")},
        {"Show player stats", () => Console.WriteLine( currentRun.playerReference.PrintPlayerStats())},
        {"Show bosses", () => currentRun.ShowBosses()},
        {"Show boss inventory",() => currentRun.ShowBossInventory()},
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
            output += "\n   " + list[i].name;
        }
        if (output == "") return "empty list";
        else return output;
    }

    public static string ListToString(List<Item> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += "\n   " + list[i].name;
        }
        if (output == "") return "empty list";
        else return output;
    }

    public static string ListToString(List<Type> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += "\n   " + list[i];
        }
        if (output == "") return "empty list";
        else return output;
    }

    public static string ListToString(List<MoveableObject> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += "\n   " + list[i];
        }
        if (output == "") return "empty list";
        else return output;
    }

    static void ChooseFPS()
    {
        Console.WriteLine("How much FPS do you want? (0 for uncapped)\nrecommended: 120 to 600");

        targetFrameRate = GetIntFromConsole(0, int.MaxValue);
        currentSettings.fps = targetFrameRate;
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
        LoadSettings("settings.json");
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

    static void SelectCharacter()
    {
        System.Console.WriteLine("choose your character");
        Type type = typeof(Player);

        List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p != type).ToList();

        for (int i = 0; i < types.Count; i++)
        {
            Type playerType = types[i];
            Player player = (Player)Activator.CreateInstance(playerType, currentLayout);
            System.Console.WriteLine($"{i + 1}: {player.name}");
        }
        playerCharacter = (Player)Activator.CreateInstance(types[GetIntFromConsole(1, types.Count) - 1], currentLayout);

    }

    static void EnableDamageNumbers()
    {
        currentSettings.enableDamageNumbers = !currentSettings.enableDamageNumbers;
        if (currentSettings.enableDamageNumbers) System.Console.WriteLine("Damagenumbers is turned on");
        else System.Console.WriteLine("Damagenumbers is turned off");
    }

    static void SelectControlLayout()
    {
        for (int i = 0; i < ControlLayout.controlLayouts.Count; i++)
        {
            Console.Write(i + 1 + ": ");

            //måste bestämma om keybinds ska visas när man väljer control layout
            // Console.WriteLine(ControlLayout.controlLayouts[i].name);
            ControlLayout.controlLayouts[i].PrintControlLayout();
        }

        currentLayout = ControlLayout.controlLayouts[GetIntFromConsole(1, ControlLayout.controlLayouts.Count) - 1];
        Console.WriteLine("Your new control layout is " + currentLayout.name);
    }

    static void StartRun(Run run)
    {
        Raylib.SetTargetFPS(currentSettings.fps);
        currentRun = run;
        currentMenu = gameMenu;
        currentRun.playerReference = (Player)Activator.CreateInstance(playerCharacter.GetType(), currentLayout); ;
        currentRun.playerReference.InitializePlayer();
    }
}