using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public class MoveService
    {
        public List<Move> GetMoves(string[] args)
        {
            List<string> moveNames = new List<string>();

            // Check if command-line arguments are provided
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Please provide moves via command-line arguments (e.g., 'rock paper scissors').");
                return null;
            }

            moveNames = args.ToList();

            // Check for duplicate moves
            if (moveNames.Count != moveNames.Distinct().Count())
            {
                InputError.DisplayDuplicateInputError();
                return null;
            }

            // Validate move count
            if (!IsValidMoveCount(moveNames.Count))
            {
                InputError.DisplayInvalidMoveCountError();
                return null;
            }

            return CreateMoves(moveNames);
        }

        private bool IsValidMoveCount(int count) => count >= 3 && count % 2 != 0;

        private List<Move> CreateMoves(List<string> moveNames)
        {
            var moves = new List<Move>();
            for (int i = 0; i < moveNames.Count; i++)
            {
                moves.Add(new Move(i, moveNames[i]));
            }
            return moves;
        }
    }
}
