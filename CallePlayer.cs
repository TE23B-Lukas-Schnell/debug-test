class CallePlayer : Player
{
    public CallePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 85;
        height = 80;
        maxHP = 20; 
        hp = maxHP;

        spriteFilePath = @"./Sprites/carlengman-scaled-600x600.jpg";

        
        
        // Inventory.Add(startItem);
    }
}