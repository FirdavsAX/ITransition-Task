namespace Task3
{
    public class MoveService
    {
        public List<Move> GetMoves()
        {
            List<string> moveNames = new List<string>();
            string movesString = "";

            while (string.IsNullOrWhiteSpace(movesString))
            {
                Console.WriteLine("Enter moves (e.g., 'rock paper scissors'):");
                movesString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(movesString))
                {
                    InputError.DisplayEmptyInputError();
                    continue;
                }

                moveNames = movesString.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (moveNames.Count != moveNames.Distinct().Count())
                {
                    InputError.DisplayDuplicateInputError();
                    movesString = "";
                    continue;
                }

                if (!IsValidMoveCount(moveNames.Count))
                {
                    InputError.DisplayInvalidMoveCountError();
                    movesString = "";
                }
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
