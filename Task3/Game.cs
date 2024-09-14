using System;
using Task3.Services;

namespace Task3
{
    public class Game
    {
        private readonly List<Move> _moves;
        private readonly GameService _gameService;
        private readonly Random _rng;
        private readonly HmacCalculator _hmacCalculator;
        private readonly RandomKeyGenerator _keyGenerator;
        private byte[] _generatedKey;
        private string _hmac;

        public Game(List<Move> moves, GameService gameService, HmacCalculator hmacCalculator, RandomKeyGenerator keyGenerator, Random rng)
        {
            _moves = moves;
            _gameService = gameService;
            _hmacCalculator=hmacCalculator;
            _keyGenerator=keyGenerator;
            _rng=rng;
        }

        public void Start()
        {
            bool gameIsRunning = true;

            while (gameIsRunning)
            {
                // Step 1: Generate a cryptographically strong random key
                _generatedKey = _keyGenerator.GenerateKey(256); // 256 bits = 32 bytes key

                // Step 2: Computer selects a move
                var computerMove = GetComputerMove();

                // Step 3: Calculate HMAC using the computer's move and the generated key
                _hmac = _hmacCalculator.CalculateHmac(computerMove.Name, _generatedKey);
                Console.WriteLine($"HMAC: {_hmac}");

                // Step 4: Display user menu for selecting their move
                _gameService.DisplayMoves(_moves);

                int userMoveIndex = GetUserMoveIndex();

                if (HandleSpecialUserInput(userMoveIndex, ref gameIsRunning)) continue;

                var userMove = _moves[userMoveIndex - 1];

                // Step 5: Reveal the computer's move and the generated key after the user makes their move
                DisplayMoves(computerMove, userMove);

                // Step 6: Determine the result
                gameIsRunning = ProcessGameResult(computerMove, userMove);
            }
        }

        private Move GetComputerMove()
        {
            int computerMoveIndex = _rng.Next(_moves.Count);
            return _moves[computerMoveIndex];
        }

        private int GetUserMoveIndex()
        {
            Console.WriteLine("Make your move (choose a number, '0' to quit, '-1' for help):");
            if (int.TryParse(Console.ReadLine(), out int moveIndex))
            {
                return moveIndex;
            }

            Console.WriteLine("Invalid input! Please enter a valid number.");
            return -2; // Invalid
        }

        private bool HandleSpecialUserInput(int userMoveIndex, ref bool gameIsRunning)
        {
            if (userMoveIndex == 0)
            {
                Console.WriteLine("Thanks for playing!");
                gameIsRunning = false;
                return true;
            }

            if (userMoveIndex == -1)
            {
                _gameService.DisplayHelp(_moves);
                return true;
            }

            if (userMoveIndex < 1 || userMoveIndex > _moves.Count)
            {
                Console.WriteLine("Invalid move! Please select from the list.");
                return true;
            }

            return false;
        }

        private void DisplayMoves(Move computerMove, Move userMove)
        {
            Console.WriteLine($"Computer chose: {computerMove.Name}");
            Console.WriteLine($"You chose: {userMove.Name}");
        }

        private bool ProcessGameResult(Move computerMove, Move userMove)
        {
            var result = _gameService.DetermineWinner(computerMove, userMove, _moves.Count);
            return EvaluateGameResult(result);
        }

        private bool EvaluateGameResult(int result)
        {
            if (result == 0)
            {
                Console.WriteLine("It's a draw!");
                return true;
            }
            else if (result == -1)
            {
                Console.WriteLine("You lose!");
                return false;
            }

            Console.WriteLine("You win!");
            return true;
        }
    }
}
