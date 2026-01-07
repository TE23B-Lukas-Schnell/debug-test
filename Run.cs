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
}