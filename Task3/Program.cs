using Task3;
using Task3.Services;

class Program
{
    static void Main(string[] args)
    {
        var moveService = new MoveService();
        var moves = moveService.GetMoves(args); // Pass the command-line arguments

        if (moves == null) return;

        var gameService = new GameService();
        var rng = new Random();
        var hmacCalculator = new HmacCalculator();
        var keyGenerator = new RandomKeyGenerator();

        var game = new Game(moves, gameService, hmacCalculator, keyGenerator, rng);
        game.Start();
    }
}
