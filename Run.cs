class Run
{
    //lista för alla objekt som ska hanteras, det är lista för att den kan öka och minska under runtime
    public List<GamePlayObject> gameList = new List<GamePlayObject>();

    //objekt som ska läggas till i main listan efter varje iteration,
    public readonly List<GamePlayObject> pendingAdds = new List<GamePlayObject>();
    readonly object gameListLock = new object();

    // kan användas säkert i alla threads
    public void AddToGameList(GamePlayObject obj)
    {
        lock (gameListLock)
        {
            pendingAdds.Add(obj);
        }
    }

    //lägger till alla objekt som väntar
    public void AddPendingObjects()
    {
        lock (gameListLock)
        {
            if (pendingAdds.Count > 0)
            {
                for (int i = 0; i < pendingAdds.Count; i++)
                {
                    pendingAdds[i].BeginDraw(); //kör alla begin draw funktion så att spriterna funkar
                }
                gameList.AddRange(pendingAdds);
                pendingAdds.Clear();
            }
        }
    }

    public List<Hitbox> hitboxes = [];

    public readonly List<Hitbox> hitboxPendingAdds = [];
    public readonly List<Hitbox> pendingRemoves = [];
    readonly object listLock = new object();

    //lägger till alla objekt som väntar
    public void AddPendingHitboxes()
    {
        lock (listLock)
        {
            if (hitboxPendingAdds.Count > 0)
            {
                hitboxes.AddRange(hitboxPendingAdds);
                hitboxPendingAdds.Clear();
            }
        }
    }

    // kan användas säkert i alla threads
    public void AddToHitboxList(Hitbox obj)
    {
        lock (listLock)
        {
            hitboxPendingAdds.Add(obj);
        }
    }

    public Player playerReference;

    public bool deadRun = false;

    //bestämmer rng för runnet
    public int seed;

    //sparar alla bossar man ska möta, boolen av gör om man har klarat den. falsk i början, sann när man har klarat den
    public Dictionary<Boss, bool> bossesToFight = new();

    public int currentBossIndex = 0;
    public Boss currentBoss;

    //möjliga items att få på ett run, oftast en kopia listan med alla items, när ett item plockas från listan så borde försvinna ur den
    public List<Item> availableItems = [];

    // innehåller items som alla bossar ska ha
    public List<Item> bossItems = [];

    // hur många items man får välja
    public int amountOfItemsToChooseFrom = 2;

    public void PrintRunStats()
    {
        Console.WriteLine(@$"
    current boss    {currentBossIndex}
    seed            {seed}
    ");

        ShowBosses();
        Console.WriteLine();
    }

    public void ShowBosses()
    {
        System.Console.WriteLine("bosses you will fight this run:");
        for (int i = 0; i < bossesToFight.Count; i++)
        {
            string myString = "";
            if (bossesToFight.Values.ToArray()[i]) myString = "defeated";
            else myString = "not defeated";
            Console.WriteLine($"boss {i + 1}: {bossesToFight.Keys.ToArray()[i].name} {myString}");
        }
    }

    public void ShowBossInventory()
    {
        System.Console.WriteLine("boss inventory:");
        System.Console.WriteLine(GibbManager.ListToString(bossItems));
    }

    public void ShowAvailableitems()
    {
        for (int i = 0; i < availableItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {availableItems[i].name} \n {availableItems[i].description}");
        }
    }

    List<Item> GetRandomItems(int amount, List<Item> items)
    {
        amount = Math.Clamp(amount, 0, items.Count);
        List<Item> output = new();
        Random random = Random.Shared;

        for (int i = 0; i < amount; i++)
        {
            int index = random.Next(0, items.Count);
            output.Add(items[index]);
            items.Remove(items[index]);
        }
        return output;
    }
    //denna funktion måste få en lista som redan är lika lång som amount, den gör inte det själv
    void GiveItem(int amount, List<Item> availableItems, List<Item> playerInventory, List<Item> bossInventory)
    {
        if (availableItems.Count != 0)
        {
            string correctGrammar;
            if (amount < 3) correctGrammar = "items"; else correctGrammar = "item";
            Console.WriteLine($"Choose an item, the {correctGrammar} you don't choose will be used by all the following bosses!");

            for (int i = 0; i < availableItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {availableItems[i].name} \n {availableItems[i].description}");
            }

            int itemToChoose = GibbManager.GetIntFromConsole(1, availableItems.Count) - 1;

            playerInventory.Add(availableItems[itemToChoose]);
            availableItems.Remove(availableItems[itemToChoose]);
            bossInventory.AddRange(availableItems);
        }
        else
        {
            Console.WriteLine("there are no items left!!1 :(");
        }
        Console.WriteLine("player: " + GibbManager.ListToString(playerInventory));
        Console.WriteLine("boss: " + GibbManager.ListToString(bossInventory));

        playerReference.ApplyBuffsFromItem();
        /// kommer detta att funka??? 🧐🧐🧐
    }

    public List<Type> GenerateBossList(List<Type> availableBosses, int amountOfBosses)
    {
        // amountOfBosses = Math.Clamp(amountOfBosses, 0, availableBosses.Count);
        Random random = Random.Shared;
        List<Type> output = [];
        List<Type> tempList = new List<Type>(availableBosses);

        for (int i = 0; i < amountOfBosses; i++)
        {
            int index = random.Next(0, tempList.Count);
            output.Add(tempList[index]);
            tempList.Remove(tempList[index]);
            if (tempList.Count == 0)
            {
                tempList.AddRange(availableBosses);
            }
        }
        return output;
    }

    bool CheckBossesBeaten(bool[] values)
    {
        return values.All(v => v);
    }

    public void GibbigtVärre()
    {
        Boss bossToFight = bossesToFight.Keys.ToArray()[currentBossIndex];

        bossToFight.Inventory.AddRange(bossItems.Select(i => i.CopyItem()));

        bossToFight.ApplyBuffsFromItem();

        currentBoss = bossToFight;

        GamePlayObject objectThatDied = ActualGibbNoWay(bossToFight);
        // Console.WriteLine(objectThatDied + " died a deathly death");

        bossesToFight[bossToFight] = true;

        if (CheckBossesBeaten(bossesToFight.Values.ToArray()))
        {
            Console.WriteLine("köttigt run klarat");
            Console.ReadLine();
            currentBossIndex++;
            EndRun();
        }
        else
        {
            if (objectThatDied == playerReference)
            {
                Console.WriteLine("YOU DIED!!!111");
                Console.ReadLine();
                EndRun();
            }
            else
            {
                currentBossIndex++;
                GiveItem(amountOfItemsToChooseFrom, GetRandomItems(amountOfItemsToChooseFrom, availableItems), playerReference.Inventory, bossItems);
            }
        }
    }

    // this is the actual game!!!11 veri important
    public GamePlayObject ActualGibbNoWay(Boss enemy)
    {
        // playerReference.InitializePlayer();
        // ClearGameList();
        enemy.InitializeBoss();

        GibbManager.currentlyGibbing = true;

        Raylib.InitWindow(enemy.screenSizeX, enemy.screenSizeY, "Game");

        for (int i = 0; i < gameList.Count; i++)
        {
            gameList[i].BeginDraw();
        }

        FightableObject loser = playerReference;
        bool pause = false;

        float endTimer = 1f;

        while (!Raylib.WindowShouldClose() && endTimer >= 0)
        {
            if (GibbManager.currentlyGibbing == false)
            {
                endTimer -= Raylib.GetFrameTime();
            }

            Raylib.SetExitKey(KeyboardKey.Null);

            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                pause = !pause;
            }

            Raylib.BeginDrawing();

            if (!pause)
            {
                Raylib.ClearBackground(GibbManager.backgroundColor);

                //lägg till alla objekt som behöver läggas till utan att ändra på listan medans den itereras
                AddPendingObjects();
                AddPendingHitboxes();



                for (int i = 0; i < gameList.Count; i++)
                {
                    //först uppdatera alla värden
                    gameList[i].Update();
                    gameList[i].Draw(); // sen ritar man ut allt till skärmen
                }
                Hitbox.ShowHitboxes();


                //denna rad skrevs av mikael 
                gameList.RemoveAll(obj => obj.remove == true);
                hitboxes.RemoveAll(obj => obj.remove == true);

                // gör det enklare att debugga
                /*for (int i = 0; i < MoveableObject.gameList.Count; i++)
                {
                    Console.WriteLine(MoveableObject.gameList[i]); 
                }*/

                Raylib.DrawText(Raylib.GetFPS().ToString(), 0, 0, 30, Color.Black);
                if (Raylib.IsKeyDown(KeyboardKey.Tab))
                {
                    Raylib.DrawText(playerReference.PrintPlayerStats(), 10, 40, 30, Color.Black);
                }
            }
            else // pause logic here
            {
                Raylib.DrawText("Game Paused", Raylib.GetScreenWidth() / 2 - 250, Raylib.GetScreenHeight() / 2 - 45, 70, Color.Black);
                Raylib.DrawText("The pause function is horribly broken but im too lazy", Raylib.GetScreenWidth() / 2 - 700, Raylib.GetScreenHeight() / 2 + 60, 50, Color.Black);
                Raylib.DrawText("to fix it, use at own risk", Raylib.GetScreenWidth() / 2 - 690, Raylib.GetScreenHeight() / 2 + 110, 50, Color.Black);
            }

            Raylib.EndDrawing();
        }

        if (gameList.Contains(playerReference))
        {
            loser = enemy;

            while (!Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                Raylib.BeginDrawing();

                Raylib.DrawRectangle(enemy.screenSizeX / 2 - 230, enemy.screenSizeY / 2 - 5, 400, 80, Color.Yellow);
                Raylib.DrawText("You won!!", enemy.screenSizeX / 2 - 200, enemy.screenSizeY / 2 - 5, 70, Color.Black);

                Raylib.DrawText("Press enter to continue", enemy.screenSizeX / 2 - 200, enemy.screenSizeY / 2 + 200, 30, Color.Black);

                Raylib.EndDrawing();
            }

        }
        else
        {
            loser = playerReference;
            while (!Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                Raylib.BeginDrawing();

                Raylib.DrawRectangle(enemy.screenSizeX / 2 - 230, enemy.screenSizeY / 2 - 5, 400, 80, Color.Black);
                Raylib.DrawText("You died", enemy.screenSizeX / 2 - 200, enemy.screenSizeY / 2 - 5, 70, Color.Red);

                Raylib.DrawText("Press enter to continue", enemy.screenSizeX / 2 - 200, enemy.screenSizeY / 2 + 200, 30, Color.Black);

                Raylib.EndDrawing();
            }

        }

        gameList.RemoveAll(obj => !(obj is Player));

        Raylib.CloseWindow();

        return loser;
    }

    void EndRun()
    {
        deadRun = true;
        Console.WriteLine(@$"Run stats:
bosses killed            {currentBossIndex}
");
        Console.WriteLine(playerReference.PrintPlayerStats());

        System.Console.WriteLine("boss items: ");
        System.Console.WriteLine(GibbManager.ListToString(bossItems));
    }

    List<Boss> GetBossesFromTypes(List<Type> list)
    {
        List<Boss> output = [];

        for (int i = 0; i < list.Count; i++)
        {
            Boss boss = (Boss)Activator.CreateInstance(list[i]);
            output.Add(boss);
        }

        return output;
    }

    public Run(int seed, List<Type> bossList, List<Item> items)
    {
        this.seed = seed;

        availableItems.AddRange(items.Select(i => i.CopyItem()));

        List<Type> typeList = GenerateBossList(bossList, 7);

        List<Boss> newBossList = GetBossesFromTypes(typeList);

        for (int i = 0; i < newBossList.Count; i++)
        {
            bossesToFight.Add(newBossList[i], false);
        }

        ShowBosses();

    }
}