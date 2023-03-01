using System.Linq;

string input = string.Empty;
bool validInput = input.Contains('>') || input.Contains('<') || input.Contains('V') || input.Contains('^');

do
{
    Console.WriteLine("Enter the movement:  ");
    input = Console.ReadLine()!;
    validInput = input.Contains('>') || input.Contains('<') || input.Contains('V') || input.Contains('^');

    if (!validInput)
    {
        Color(ConsoleColor.Red);
        Console.WriteLine("INVALID INPUT!!");
    }
    Console.ResetColor();

} while (!validInput);
Console.WriteLine();

Directions(input);

Console.ReadKey();


void Directions(string input)
{
    var inputInList = new List<string>();

    while (input != string.Empty)
    {
        if (Char.IsDigit(input[0]))
        {
            var number = new String(input.TakeWhile(Char.IsDigit).ToArray());
            inputInList.Add(number);
            input = input.Substring(number.Length);
        }
        else
        {
            inputInList.Add(input.First().ToString());
            input = input.Substring(1);
        }
    }

    int countXPosition = 0;
    int countYPosition = 0;

    for (int i = 0; i < inputInList.Count; i++)
    {
        switch (inputInList.ElementAt(i))
        {
            case ">": countXPosition++; break;
            case "V": countYPosition--; break;
            case "<": countXPosition--; break;
            case "^": countYPosition++; break;
            default:

                if (i > 0)
                {
                    if (inputInList.ElementAt(i - 1) == Convert.ToString('>')) { countXPosition += Convert.ToInt32(inputInList.ElementAt(i)) - 1; }
                    else if (inputInList.ElementAt(i - 1) == Convert.ToString('<')) { countXPosition -= Convert.ToInt32(inputInList.ElementAt(i)) - 1; }
                    else if (inputInList.ElementAt(i - 1) == Convert.ToString('V')) { countYPosition -= Convert.ToInt32(inputInList.ElementAt(i)) - 1; }
                    else { countYPosition += Convert.ToInt32(inputInList.ElementAt(i)) - 1; }
                }
                break;
        }
    }

    string result = string.Empty;

    Console.Write("The rover is ");
    Color(ConsoleColor.Magenta);

    if (countXPosition == 0 && countYPosition == 0) { Console.WriteLine("in the base station"); }
    if (countXPosition > 0) { result += ResultingDirection("East", countXPosition); }
    else if (countXPosition < 0) { result += ResultingDirection("West", countXPosition*=-1); }
    if (countYPosition != 0 && countXPosition!=0) { result += " and "; }
    if (countYPosition > 0) { result += ResultingDirection("North", countYPosition); }
    else if (countYPosition < 0) { result += ResultingDirection("South", countYPosition*=-1); }

    double linearDistance = Math.Sqrt(Math.Pow(countXPosition, 2) + Math.Pow(countYPosition, 2));
    Console.WriteLine(result);
    Console.ResetColor();
    Console.Write($"Lineare distance = ");
    Color(ConsoleColor.Magenta);
    Console.Write($"{double.Round(linearDistance, 2)}m ");

    Console.ResetColor();

    Console.Write(", Manhatten distance = ");
    Color(ConsoleColor.Magenta);
    Console.WriteLine($"{countXPosition + countYPosition}m");

    Console.ResetColor();
}

string ResultingDirection(string direction, int metres) { return $"{metres}m to the {direction}"; }
void Color(ConsoleColor color) { Console.ForegroundColor = color; }