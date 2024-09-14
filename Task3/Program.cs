using Task3;
using Task3.Services;

Console.WriteLine("Enter moves (e.g., 'rock paper scissors'):");
string movesString = Console.ReadLine();

var moveService = new MoveService();
var moves = moveService.ParseMoves(movesString ?? "");

var gameService = new GameService();
var rng = new Random();
var hmacCalculator = new HmacCalculator();
var keyGenerator = new RandomKeyGenerator();

if (moves == null) return;

var game = new Game(moves,gameService,hmacCalculator,keyGenerator,rng);
game.Start();