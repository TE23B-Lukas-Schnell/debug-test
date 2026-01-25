class Run()
{
    //bestämmer rng för runnet
    int seed;

    //lista eller kö på kanske 5 random bossar, ordingen är viktig
    Queue<Boss> bossesToFight;


    //möjliga items att få på ett run, oftast en kopia listan med alla items, när ett item plockas från listan så borde försvinna ur den
    List<Items> availableItems;

    // innehåller items som alla bossar ska ha
    List<Items> bossItems;

    public static int amountOfItemsToChooseFrom = 2;

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

    List<Boss> GenerateBossList(List<Boss> availableBosses, int amountOfBosses)
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


    // public Run()
    // {

    // }

    public static Run ConfigureRun()
    {



        
        Run run = new Run()
        {

        };

        return run;
    }



}