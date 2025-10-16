static class Setup
{
    public static int targetFrameRate;
    public static int windowWidth = 1600;
    public static int windowHeight = 900;
    public static bool currentlyGibbing = false;
    public static bool fullscreen = false;

    static string scoreFilePath = "./scores.txt";

    static Player playerReference = new Player(800, 450);

    public static bool playerDead = false;

    public static Dictionary<string, int> highscores = new Dictionary<string, int>();

    public static void WriteDictionary(Dictionary<string, int> dictionary)
    {
        if (dictionary != null)
        {
            foreach (KeyValuePair<string, int> entry in highscores)
            {
                Console.WriteLine($"Name: {entry.Key}, Score: {entry.Value}");
            }
        }
    }

    public static void Intructions()
    {
        Console.WriteLine("Do you want to see the instructions? [Y/N]");
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine(@"Controls:
    WASD or arrow keys to move
    Space or Z to jump
    L or X to shoot
    Left shift or C to dash
Objective: 
    kill the green cube!!!11
            ");
            Console.ReadLine();
        }
        else
        {
            return;
        }
    }
    public static int ChooseFPS()
    {
        Console.WriteLine("How much FPS do you want?");

        while (!int.TryParse(Console.ReadLine(), out targetFrameRate) || targetFrameRate < 1)
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

    public static void LoadSave()
    {
        highscores = ReadSaveData(ReadSaveFile(scoreFilePath));
    }

    public static void SaveGame()
    {
        throw new NotImplementedException();
    }

    public static void StartGame()
    {
        LoadSave();
        Raylib.SetTargetFPS(ChooseFPS());
        Intructions();
    }

    public static void GameLoop()
    {

    }

    // this is the actual game!!!11 veri important
    public static MoveableObject WindowGame()
    {

        //fixa game manager!!!! 1
        // kommer inte fixa game manager!!!11
        currentlyGibbing = true;
        Raylib.InitWindow(Setup.windowWidth, Setup.windowHeight, "Game");

        Enemy enemy = new Enemy((int)(Raylib.GetScreenWidth() * 0.5f), 0);

        MoveableObject loser = playerReference;

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


            /*for (int i = 0; i < MoveableObject.gameList.Count; i++)
            {
                Console.WriteLine(MoveableObject.gameList[i]); // gör det enklare att debugga
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
