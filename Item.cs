class Item
{
    static Dictionary<string, Item> AllItems = [];

    public static Item GetItem(string name)
    {
        foreach (string itemName in AllItems.Keys)
        {
            if (itemName == name)
            {
                return AllItems[name].CopyItem(AllItems[name]);
            }
        }
        return new Item()
        {
            name = "EROOR: non existent item",
            description = " this item was returned because the desired item that was searched was not found, ok batig",
        };
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

    Item CopyItem(Item itemToCopy)
    {
        Item kopieratItem = new Item()
        {
            name = itemToCopy.name,
            description = itemToCopy.description,
            ApplyStatChangesFunction = itemToCopy.ApplyStatChangesFunction
        };
        return kopieratItem;
    }

    public Item(string name, string description, ApplyStatChanges applier)
    {
        ApplyStatChangesFunction = applier;
        this.name = name;
        this.description = description;
        AllItems.Add(this.name, this);
    }

    //this constructor does not it add to AllItems 
    public Item()
    {

    }

    static Item[] dags_att_skapa_alla_items_här = {


        new Item("Mickes hjälp", "Köttiga Micke hjälper dig att optimisera din kod, det gör dina bullets snabbare   ZOOM", applier: (FightableObject objectToBuff) =>
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
                b.maxHP += 50;
                b.HealDamage(50, b);
            }

        }),

         new Item("Anton Faren", "Hej jag heter...", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.name = "Anton";
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                b.name = "Anton";
            }
        }),

        new Item("Martins fönster öppnare", "gör att estetare hoppar ut ur fönstret vilket gör att du hoppar högre", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.jumpForce *= 1.23f;
                p.gravity *= 1.1f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                b.jumpForce += 200;

            }
        }),

        new Item("Tung väska", "Martin tvingar dig att ta med dig 5 kontroller till skolan vilket gör väskan tung och dina bullets är nu påverkade av gravitation", applier: (FightableObject objectToBuff) =>
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
                b.bulletDamage += 2;

            }
        }),

        new Item("Kemibok", "Kemilabben gick fel och dina bullets gör poison damage", applier: (FightableObject objectToBuff) =>
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

        new Item("Smutje.se", "Gör att du båtar ner informationen", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;

                p.shoot += () =>
                {
                    Random random = new Random();
                    if(random.Next(0, 10) == 0)
                    {
                         KarimPlayer.BåtigBulletHorizontal(p.x,p.y,p.xSpeed,p.ySpeed,p.bulletxSpeed,p.bulletDamage);
                    }
                };

                p.upShoot += () =>
                {
                    Random random = new Random();
                    if(random.Next(0, 10) == 0)
                    {
                        KarimPlayer.BåtigBulletVertical(p.x,p.y,p.xSpeed,p.ySpeed,p.bulletxSpeed,p.bulletDamage);
                    }
                };

                p.setShootCooldown += 0.13f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                b.waitMultiplier += 0.1f;
            }
        }),

        new Item("Tu's Genomgång", "Ahn Tu Tran går igenom imaginära tal på tavlan vilket invertar ditt movement men ökar damage", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.bulletDamage *= 1.69f;
                p.moveSpeed = -p.moveSpeed;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                b.moveSpeed += 23f;
            }
        }),

         new Item("Skolmaten", "Gör att du blir tjock                               haku bygg", applier: (FightableObject objectToBuff) =>
        {
            if(objectToBuff is Player)
            {
                Player p = objectToBuff as Player;
                p.maxHP += 20;
                p.HealDamage(p.maxHP,p);
                p.width *= 2f;
                p.height *= 1.1f;
            }
            else if (objectToBuff is Boss)
            {
                Boss b = objectToBuff as Boss;
                b.width *= 1.3f;
            }
        }),



        new Item("Erikas Waifu Köttbåt",
"extremt båtig waifu-item: meaty michaels i hjärnan gör att kulor blir tyngre men snabbare men långsammare samtidigt",
applier: (FightableObject objectToBuff) =>
{
    if(objectToBuff is Player)
    {
        Player p = objectToBuff as Player;

        // 🚤 båtig movement logik
        p.jumpForce = 0.7f;
        p.gravity= 1.4f;
        p.jumpForce += 69;
        p.gravity -= 0.2f;

        // 🍖 köttig bullet-kaos
        p.bulletDamage += 2;
        p.bulletDamage = 0.8f;
        p.bulletDamage += 7;
        p.bulletxSpeed= 1.6f;
        p.bulletxSpeed = 0.5f;
        p.bulletySpeed += 420f;
        p.bulletySpeed -= 123f;
        p.bulletGravity += 999;
        p.bulletGravity -= 333;

        // 🧠 brainrot logik som inte betyder något
        p.width += 13;
        p.height -= 4;
        p.width -= 2;
        p.height += 9;

        // 💖 waifu survivability math
        p.maxHP += 2;
        p.damageMultiplier = 0.9f;
        p.damageMultiplier= 1.1f;
        p.damageMultiplier -= 0.05f;

        // 🚀 cooldown chaos
        p.setShootCooldown -= 0.2f;
        p.setShootCooldown += 0.11f;
        p.setShootCooldown -= 0.03f;

        // 🍖 final meaty michael blessing
        p.bulletxSpeed += p.width;
        p.bulletySpeed += p.height * 2;
        p.bulletGravity += p.maxHP * 10;
    }
    else if (objectToBuff is Boss)
    {
        Boss b = objectToBuff as Boss;
        b.maxHP += 5;
        b.damageMultiplier *= 0.95f;
    }
})

    };
}