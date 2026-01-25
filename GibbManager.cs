static class GibbManager
{
    public static int targetFrameRate;
    public static bool currentlyGibbing = false;
    public static bool fullscreen = false;
    static string scoreFilePath = "./scores.txt";
    static Dictionary<string, int> highscores = new Dictionary<string, int>();
    public static bool playerDead = false;
    public static Menu currentMenu;

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
    //för att kontrollerna ska funka så måste spelaren skapas när ett runs startas inte när programmet startas, consider att göra run klassen inom snar framtid
    public static Player playerReference = new Player(defaultKeybindsWASD);
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
        {"create control layout", () => new ControlLayout(playerReference.keyPressed.Keys.ToArray())},
        {"go back to main menu", () => currentMenu = mainMenu},
    });

    static Menu gameMenu = new("game", new Dictionary<string, Action>()
    {
        {"Next boss", StartGame},
        {"Show your score", () =>   Console.WriteLine($"Your score is: {Player.score}")},
        {"Show player stats", () =>  playerReference.PrintPlayerStats()},
        {"Apply item stats (temporary)", () =>  playerReference.ApplyBuffsFromItem()},
        {"retry boss (temporary)", () => {
            ControlLayout temp =  playerReference.currentLayout;
            playerReference = new Player(temp);
            MoveableObject.gameList.Clear();
            StartGame();
                }
            }
        });

    static Menu configureRunMenu = new Menu("configure", new Dictionary<string, Action>()
    {
        {"Start run", () => currentMenu = gameMenu},
        {"Change seed", () =>  WriteDictionary(highscores)},
        {"Change available items (not implemented)", () => WriteDictionary(highscores)},
        {"Change available bosses (not implemented)", () => WriteDictionary(highscores)},
    });


    static List<Boss> PeakBossPeakBoss = new List<Boss>()
    {
        // new Karim(), new Nathalie(),
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
        }),
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

        playerReference.currentLayout = ControlLayout.controlLayouts[GetIntFromConsole(1, ControlLayout.controlLayouts.Count) - 1];
        Console.WriteLine("Your new control layout is " + playerReference.currentLayout.name);
    }

    static void StartGame()
    {
        MoveableObject survivor = WindowGame(new Karim());
        Console.WriteLine(survivor + " died a deathly death");
        // bossesBeaten++;
        // GiveItem(2, playerReference, bossesToFightThisRun[bossesBeaten]);
    }

    // this is the actual game!!!11 veri important
    static MoveableObject WindowGame(Boss enemy)
    {
        currentlyGibbing = true;

        Raylib.InitWindow(enemy.screenSizeX, enemy.screenSizeY, "Game");

        for (int i = 0; i < MoveableObject.gameList.Count; i++)
        {
            MoveableObject.gameList[i].BeginDraw();
        }

        FightableObject loser = playerReference;
        bool pause = false;
        while (!Raylib.WindowShouldClose() && currentlyGibbing)
        {
            Raylib.SetExitKey(KeyboardKey.Null);

            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                pause = !pause;
            }

            Raylib.BeginDrawing();

            if (!pause)
            {
                Raylib.ClearBackground(Color.White);

                //lägg till alla objekt som behöver läggas till utan att ändra gamelist medans den itereras
                MoveableObject.AddPendingObjects();

                for (int i = 0; i < MoveableObject.gameList.Count; i++)
                {
                    //först uppdatera alla värden
                    MoveableObject.gameList[i].Update();
                    MoveableObject.gameList[i].Draw(); // sen ritar man ut allt till skärmen
                }
                //denna rad skrevs av mikael 
                MoveableObject.gameList.RemoveAll(obj => obj.remove == true);

                // gör det enklare att debugga
                /*for (int i = 0; i < MoveableObject.gameList.Count; i++)
                {
                    Console.WriteLine(MoveableObject.gameList[i]); 
                }*/

                Raylib.DrawText(Raylib.GetFPS().ToString(), 0, 0, 30, Color.Black);
            }
            else // pause logic here
            {
                Raylib.DrawText("Game Paused", Raylib.GetScreenWidth() / 2 - 250, Raylib.GetScreenHeight() / 2 - 45, 70, Color.Black);
                Raylib.DrawText("The pause function is horribly broken but im too lazy", Raylib.GetScreenWidth() / 2 - 700, Raylib.GetScreenHeight() / 2 + 60, 50, Color.Black);
                Raylib.DrawText("to fix it, use at own risk", Raylib.GetScreenWidth() / 2 - 690, Raylib.GetScreenHeight() / 2 + 110, 50, Color.Black);
            }

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