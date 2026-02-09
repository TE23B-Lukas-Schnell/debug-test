class Item
{
    static Dictionary<string, Item> AllItems = [];

    public static Item GetItem(string name)
    {
        foreach(string itemName in AllItems.Keys)
        {
            if(itemName == name)
            {
                return AllItems[name];
            }
        }
        return new Item("EROOR: non existent item"," this item was returned because the desired item that was searched was not found, ok batig",applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        });
    }

    public string name;
    public string description;
    public bool buffActivated = false;

    public delegate void ApplyStatChanges(FightableObject objectToBuff);
    public ApplyStatChanges ApplyStatChangesFunction;

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
    }
    
    public Item(string name, string description, ApplyStatChanges applier)
    {
        ApplyStatChangesFunction = applier;
        this.name = name;
        this.description = description;
        AllItems.Add(this.name,this);
    }

    static Item[] dags_att_skapa_alla_items_här = { 

        
    new Item("Mickes hjälp", "båtig item, gör bullet snabbare", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.bulletxSpeed *= 1.5f;
                p.setShootCooldown -= 0.1f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        }),

        new Item("Martins fönster öppnare", "gör att estetare hoppar ut ur fönstret", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.jumpForce *= 1.5f;
                p.gravity *= 1.1f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        }),

        new Item("Skolmaten", "från alexander: man blir liksom tjock så man får gravity", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.bulletDamage++;
                p.bulletDamage *= 1.5f;
                p.bulletGravity += 1067;
                p.bulletxSpeed *= 0.67f;
                p.bulletySpeed += 367f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                
            }
        }),

        new Item("Kemibok", "från dante: när man träffar bossen så blir det en acid effekt!!111", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                //fixa någon gång efter du har gjort partikel systemet
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        }),

        new Item("Calles krona", "Gör dig större men du tar mindre damage och gör lite mer damage, precis som Calle!", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.damageMultiplier = 0.8f;
                p.width += 50;
                p.height += 10;
                p.bulletDamage *= 1.1f;
                p.maxHP += 3;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
            }
        }),
    };
}