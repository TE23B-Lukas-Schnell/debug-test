class Menu
{
    public static Dictionary<string,Menu> Menus = [];

    public Dictionary<string, Action> menuActions;

    public string name;


    public Menu(string name, Dictionary<string, Action> menuActions)
    {
        this.name = name;
        this.menuActions = menuActions;
        Menus.Add(name,this);
    }
}