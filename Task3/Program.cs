
using Task3;

Console.WriteLine("Enter moves (e.g., 'rock paper scissors'):");
var movesString = Console.ReadLine();
var moves = GetMoves(movesString ?? "");
if (moves == null) return; // Exit if invalid moves

var rng = new Random(); // Random number generator for computer move

while (true)
{
    // Computer randomly selects a move
    int computerMoveIndex = rng.Next(moves.Count);
    var computerMove = moves[computerMoveIndex];

    Console.WriteLine("\nMake your move (choose a number, '0' to quit, or '-1' for help):");
    DisplayMoves(moves);

    if (!int.TryParse(Console.ReadLine(), out int userMoveIndex))
    {
        Console.WriteLine("Invalid input! Please enter a valid number.");
        continue;
    }

    if (userMoveIndex == 0)
    {
        Console.WriteLine("Thanks for playing!");
        break; // Exit the game
    }
    else if (userMoveIndex == -1)
    {
        DisplayHelp(moves); // Display help table when user enters '-1'
        continue;
    }

    if (userMoveIndex < 1 || userMoveIndex > moves.Count)
    {
        Console.WriteLine("Invalid move! Please select from the list.");
        continue;
    }

    var userMove = moves[userMoveIndex - 1]; // Subtract 1 to get zero-based index

    Console.WriteLine($"Computer chose: {computerMove.Name}");
    Console.WriteLine($"You chose: {userMove.Name}");

    var result = DetermineWinner(computerMove, userMove, moves.Count);
    if (result == 0)
    {
        Console.WriteLine("It's a draw!");
    }
    else if (result == -1)
    {
        Console.WriteLine("You lose!");
        break; // End the game if the user loses
    }
    else
    {
        Console.WriteLine("You win!");
    }
}

// Get the list of moves from user input
static List<Move> GetMoves(string movesString)
{
    var moveNames = movesString.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    if (moveNames.Count % 2 == 0 || moveNames.Count < 3)
    {
        Console.WriteLine("Invalid move count! Please enter an odd number of unique moves (e.g., 'rock paper scissors').");
        return null; // Return null to indicate an error
    }

    var moves = new List<Move>();
    foreach (var moveName in moveNames.Distinct())
    {
        moves.Add(new Move { Name = moveName });
    }
    return moves;
}

// Display available moves with corresponding numbers
static void DisplayMoves(List<Move> moves)
{
    Console.WriteLine("Available moves:");
    for (int i = 0; i < moves.Count; i++)
    {
        Console.WriteLine($"{i + 1}: {moves[i].Name}");
    }
    Console.WriteLine("0: Exit");
    Console.WriteLine("-1: Help");
}

// Determine the winner of the game
static int DetermineWinner(Move computerMove, Move userMove, int moveCount)
{
    if (computerMove.Id == userMove.Id) return 0; // It's a draw

    int half = moveCount / 2;
    if ((userMove.Id - computerMove.Id + moveCount) % moveCount <= half)
    {
        return 1; // User wins
    }
    return -1; // User loses
}
// Display help table showing win/lose/draw
static void DisplayHelp(List<Move> moves)
{
    int moveCount = moves.Count;
    
    Console.Write("+-----------+");
    for (int i = 0; i < moveCount; i++)
    {
        Console.Write("-----------+");
    }
    Console.WriteLine();

    Console.Write("| PC\\User   |");
    foreach (var move in moves)
    {
        Console.Write($" {move.Name,-9} |");
    }
    Console.WriteLine();

    Console.Write("+-----------+");
    for (int i = 0; i < moveCount; i++)
    {
        Console.Write("-----------+");
    }
    Console.WriteLine();

    for (int i = 0; i < moveCount; i++)
    {
        Console.Write($"| {moves[i].Name,-9} |");
        for (int j = 0; j < moveCount; j++)
        {
            if (i == j)
            {
                Console.Write(" Draw      |");
            }
            else if ((j - i + moveCount) % moveCount <= moveCount / 2)
            {
                Console.Write(" Win       |");
            }
            else
            {
                Console.Write(" Lose      |");
            }
        }
        Console.WriteLine();

        Console.Write("+-----------+");
        for (int k = 0; k < moveCount; k++)
        {
            Console.Write("-----------+");
        }
        Console.WriteLine();
    }
}
