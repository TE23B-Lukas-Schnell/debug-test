global using Raylib_cs;
global using System;

Setup.StartGame();

//main loop
// Setup.GameLoop();

while (Setup.currentlyGibbing == false)
{

    Console.WriteLine(@"Choose an action
1. Start playing
2. Show your score
3. Show high scores");
    string answer = Console.ReadLine();
    if (answer == "1")
    {
        Console.WriteLine(Setup.WindowGame() + " died a deathly death");
        answer = "";
    }
    else if (answer == "2")
    {
        Console.WriteLine($"Your score is: {Player.score}");
    }
    else if (answer == "3")
    {
        Setup.WriteDictionary(Setup.highscores);
    }
    else
    {
        Console.WriteLine("invalid input");
    }
}

