namespace Wordle;

public static class SharedResources
{
    // String property to store the player name. It's static so it can be accessed directly from the class, 
    // it belongs to the class itself and not any instance of the class.
    public static string PlayerName { get; set; }
}