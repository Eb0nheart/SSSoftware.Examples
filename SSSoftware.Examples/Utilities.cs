namespace SSSoftware.Examples;

public static class Utilities
{
    public static void WriteServerLine(string message)
    {
        var previousColour = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColour;
    }

    public static void WriteClientLine(string message)
    {
        var previousColour = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColour;
    }
}
