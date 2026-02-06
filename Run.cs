class Run
{
    public Player playerReference = new CallePlayer(GibbManager.currentLayout);

    public bool deadRun = false;

    //bestämmer rng för runnet
    public int seed;

    //sparar alla bossar man ska möta, boolen av gör om man har klarat den. falsk i början, sann när man har klarat den
    public Dictionary<Boss, bool> bossesToFight = new();

    public int currentBoss = 0;

    //möjliga items att få på ett run, oftast en kopia listan med alla items, när ett item plockas från listan så borde försvinna ur den
    public List<Items> availableItems = [];

    // innehåller items som alla bossar ska ha
    public List<Items> bossItems = [];

    // hur många items man får välja
    public static int amountOfItemsToChooseFrom = 2;

    public void WriteBossList()
    {
        for (int i = 0; i < bossesToFight.Count; i++)
        {
            Console.WriteLine($"boss {i + 1}: {bossesToFight.Keys.ToArray()[i].name} defeated: {bossesToFight.Values.ToArray()[i]}");
        }
    }

    Items[] GetRandomItems(int amount, List<Items> items)
    {
        amount = Math.Clamp(amount, 0, items.Count);
        Items[] output = new Items[amount];
        Random random = Random.Shared;

        for (int i = 0; i < amount; i++)
        {
            int index = random.Next(0, items.Count);
            output.Append(items[index]);
            // items.Remove(items[index]);
        }
        return output;
    }

    void GiveItem(int amount, Player player, Boss nextboss)
    {
        string correctGrammar;
        if (amountOfItemsToChooseFrom < 2) correctGrammar = "items"; else correctGrammar = "item";

        Console.WriteLine($"Choose an item, the {correctGrammar} you don't will be used the next boss!");

        Items[] choosableItems = GetRandomItems(amountOfItemsToChooseFrom, availableItems);
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

    public static List<Boss> GenerateBossList(List<Boss> availableBosses, int amountOfBosses)
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

    bool CheckBossesBeaten(bool[] values)
    {
        return values.All(v => v);
    }

    public void GibbigtVärre()
    {
        Boss bossToFight = bossesToFight.Keys.ToArray()[currentBoss];

        MoveableObject objectThatDied = ActualGibbNoWay(bossToFight);
        Console.WriteLine(objectThatDied + " died a deathly death");

        bossesToFight[bossToFight] = true;

        if (CheckBossesBeaten(bossesToFight.Values.ToArray()))
        {
            Console.WriteLine("köttigt run klarat");
            EndRun();
        }
        else
        {
            if (objectThatDied == playerReference)
            {
                deadRun = true;
                Console.WriteLine("YOU DIED!!!111");
                EndRun();
            }
            else
            {
                currentBoss++;
                // GiveItem(2, playerReference, bossesToFightThisRun[bossesBeaten]);
            }
        }

    }

    // this is the actual game!!!11 veri important
    public MoveableObject ActualGibbNoWay(Boss enemy)
    {
        // playerReference.InitializePlayer();
        enemy.InitializePlayableBoss();
        GibbManager.currentlyGibbing = true;

        Raylib.InitWindow(enemy.screenSizeX, enemy.screenSizeY, "Game");

        for (int i = 0; i < MoveableObject.gameList.Count; i++)
        {
            MoveableObject.gameList[i].BeginDraw();
        }

        FightableObject loser = playerReference;
        bool pause = false;

        while (!Raylib.WindowShouldClose() && GibbManager.currentlyGibbing)
        {
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
                MoveableObject.AddPendingObjects();
                Hitbox.AddPendingHitboxes();


                for (int i = 0; i < MoveableObject.gameList.Count; i++)
                {
                    //först uppdatera alla värden
                    MoveableObject.gameList[i].Update();
                    MoveableObject.gameList[i].Draw(); // sen ritar man ut allt till skärmen
                }

                Hitbox.ShowHitboxes();

                //denna rad skrevs av mikael 
                MoveableObject.gameList.RemoveAll(obj => obj.remove == true);
                Hitbox.hitboxes.RemoveAll(obj => obj.remove == true);

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

    void EndRun()
    {

        Console.WriteLine(@$"Run stats:
bosses killed            {currentBoss}");
    }


    public Run(int seed, List<Boss> bossList, List<Items> items)
    {
        this.seed = seed;
        availableItems = items;
        for (int i = 0; i < bossList.Count; i++)
        {
            bossesToFight.Add(bossList[i], false);
        }
    }
}