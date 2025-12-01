static class GibbManager
{
    public static int targetFrameRate;
    public static int windowWidth = 1600;
    public static int windowHeight = 900;
    public static bool currentlyGibbing = false;
    public static bool fullscreen = false;
    static string scoreFilePath = "./scores.txt";
    public static int amountOfItemsToChooseFrom = 2;
    static Dictionary<string, int> highscores = new Dictionary<string, int>();
    public static bool playerDead = false;
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
    //action menu
    static Dictionary<string, Action> mainMenuActions = new Dictionary<string, Action>()
    {
        {"Start playing", GameLoop},
        {"Show high scores", () =>  WriteDictionary(highscores)},
        {"select controll layout", ControlMenu},
        {"quit game", () => {Console.WriteLine("quitting game");}},
    };

    static Dictionary<string, Action> controlMenuActions = new Dictionary<string, Action>()
    {
        {"select control layout", SelectControlLayout},
        {"create control layout", () => new ControlLayout(playerReference.keyPressed.Keys.ToArray())},
        {"go back to main menu", () => {}},
    };

    static Dictionary<string, Action> gameMenuActions = new Dictionary<string, Action>()
    {
        {"Next boss", StartGame},
        {"Show your score", () =>   Console.WriteLine($"Your score is: {Player.score}")},
        {"Show player stats", () =>  playerReference.PrintPlayerStats()},
        {"Apply item stats (temporary)", () =>  playerReference.ApplyBuffsFromItem()}
    };

    public static ControlLayout currentControlLayout = defaultKeybindsWASD;

    public static Player playerReference = new Player(currentControlLayout);

    static List<Boss> PeakBossPeakBoss = new List<Boss>()
    {

    };

    public static List<Items> AvailableItems = new List<Items>()
    {
        new Items("delegate test", "båtig item", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.bulletxSpeed *= 5;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        }),

        new Items("martins fönster öppnare", "gör att estetare hoppar ut ur fönstret", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.jumpForce *= 2;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        })
    };

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
        int båt = GetIntFromConsole();

        if (båt > maxValue)
        {
            Console.WriteLine("input is too big");
            GetIntFromConsole(minValue, maxValue);
        }
        else if (båt < minValue)
        {
            Console.WriteLine("input is too small");
            GetIntFromConsole(minValue, maxValue);
        }
        return båt;
    }

    static void GiveItem(int amount, Player player, Boss nextboss)
    {
        string correctGrammar;
        if (amountOfItemsToChooseFrom < 2) correctGrammar = "items"; else correctGrammar = "item";
        Console.WriteLine($"Choose an item, the {correctGrammar} you don't will be used the next boss!");
        Items[] choosableItems = GetRandomItems(amountOfItemsToChooseFrom, AvailableItems);
        for (int i = 0; i < choosableItems.Length; i++)
        {
            Console.WriteLine($"{i}: {choosableItems[i].name} \n {choosableItems[i].description}");
        }
        int itemToChoose;
        while (!int.TryParse(Console.ReadLine(), out itemToChoose))
        {
            Console.WriteLine("Invalid input, try again");
        }
        player.Inventory.Add(choosableItems[itemToChoose]);
        nextboss.Inventory.AddRange(choosableItems);
        /// kommer detta att funka??? 🧐🧐🧐


    }

    static List<Boss> GenerateBossList(List<Boss> availableBosses, int amountOfBosses)
    {
        amountOfBosses = Math.Clamp(amountOfBosses, 0, availableBosses.Count);
        Random random = Random.Shared;
        List<Boss> output = new List<Boss>();

        for (int i = 0; i < amountOfBosses; i++)
        {
            int index = random.Next(0, availableBosses.Count);
            output.Add(availableBosses[index]);
            availableBosses.Remove(availableBosses[index]);
        }
        return output;
    }

    static Items[] GetRandomItems(int amount, List<Items> items)
    {
        amount = Math.Clamp(amount, 0, items.Count);
        Items[] output = new Items[amount];
        Random random = Random.Shared;

        for (int i = 0; i < amount; i++)
        {
            int index = random.Next(0, items.Count);
            output.Append(items[index]);
            items.Remove(items[index]);
        }

        return output;
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

    static void Intructions()
    {
        Console.WriteLine("Do you want to see the instructions? [Y/N]");
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine(@"Controls:

    Outdated!!!!11111 båt

    WASD or arrow keys to move
    Space or Z to jump
    L or X to shoot
    Left shift or C to dash
Objective: 
    kill the green cube!!!11");
            Console.ReadLine();
        }
        else
        {
            return;
        }
    }

    static int ChooseFPS()
    {
        Console.WriteLine("How much FPS do you want? (0 for uncapped)\nrecommended: 120 to 600");

        while (!int.TryParse(Console.ReadLine(), out targetFrameRate) /*|| targetFrameRate < 1*/)
        {
            Console.WriteLine("Invalid input, try again");
        }
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
        return ["köttig micke", "10000"];
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
        LoadSave();
        Raylib.SetTargetFPS(ChooseFPS());
        // Intructions();
    }

    static void ExecuteMenu(string menuName, Dictionary<string, Action> menuActions)
    {
        Console.WriteLine("--------------------" + menuName + "--------------------------------------------");

        Console.WriteLine("choose an action");

        string[] actions = menuActions.Keys.ToArray();
        for (int i = 0; i < actions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {actions[i]}");
        }
        menuActions[actions[GetIntFromConsole(1, actions.Length + 1)-1]]();
    }

    static void ExecuteMenu(Dictionary<string, Action> menuActions)
    {
        Console.WriteLine("choose an action");

        string[] actions = menuActions.Keys.ToArray();
        for (int i = 0; i < actions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {actions[i]}");
        }
        menuActions[actions[GetIntFromConsole(1, actions.Length + 1) - 1]]();
    }

    public static void MainMenu()
    {
        while (true) ExecuteMenu(" Main menu ", mainMenuActions);

    }

    static void ControlMenu()
    {
        while (true)
        {
            ExecuteMenu(" Controls ", controlMenuActions);
        }
    }

    static void SelectControlLayout()
    {
        for (int i = 0; i < ControlLayout.controlLayouts.Count; i++)
        {
            Console.Write(i + ": ");
            ControlLayout.controlLayouts[i].PrintControlLayout();
        }

        currentControlLayout = ControlLayout.controlLayouts[GetIntFromConsole()]; // dålig kod kan vara utanför list index båt

    }

    static void StartGame()
    {
        MoveableObject survivor = WindowGame();
        Console.WriteLine(survivor + " died a deathly death");
        // bossesBeaten++;
        // GiveItem(2, playerReference, bossesToFightThisRun[bossesBeaten]);
    }

    public static void GameLoop()
    {

        List<Boss> bossesToFightThisRun = GenerateBossList(PeakBossPeakBoss, 2);

        int bossesBeaten = 0;

        for (int i = 0; i < bossesToFightThisRun.Count; i++)
        {
            Console.WriteLine("köttig boss: " + bossesToFightThisRun[i]);
        }

        while (currentlyGibbing == false)
        {
            ExecuteMenu(" Game ", gameMenuActions);
        }
    }

    // this is the actual game!!!11 veri important
    static MoveableObject WindowGame(/*Boss bossToFight*/)
    {
        currentlyGibbing = true;
        Raylib.InitWindow(GibbManager.windowWidth, GibbManager.windowHeight, "Game");

        Boss enemy = new Karim();

        for (int i = 0; i < MoveableObject.gameList.Count; i++)
        {
            MoveableObject.gameList[i].BeginDraw();
        }


        FightableObject loser = playerReference;

        while (!Raylib.WindowShouldClose() && currentlyGibbing)
        {
            // Console.Clear();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            for (int i = 0; i < MoveableObject.gameList.Count; i++)
            {
                MoveableObject.gameList[i].Update(); //först uppdatera alla värden
                MoveableObject.gameList[i].Draw(); // sen ritar man ut allt till skärmen

                /*if (MoveableObject.gameList[i].remove == true)
                {
                    MoveableObject.gameList[i].Despawn();
                }*/
            }

            //denna rad skrevs av mikael 
            MoveableObject.gameList.RemoveAll(obj => obj.remove == true);

            // gör det enklare att debugga
            /*for (int i = 0; i < MoveableObject.gameList.Count; i++)
            {
                Console.WriteLine(MoveableObject.gameList[i]); 
            }*/

            Raylib.DrawText(Raylib.GetFPS().ToString(), 0, 0, 30, Color.Black);

            Raylib.EndDrawing();
        }

        if (MoveableObject.gameList.Contains(playerReference))
        {
            loser = enemy;
        }
        else loser = playerReference;

        Raylib.CloseWindow();

        return loser;
    }
}
