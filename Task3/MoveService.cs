
namespace Task3
{
    public class MoveService
    {
        public List<Move> ParseMoves(string movesString)
        {
            var moveNames = movesString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

            if (!IsValidMoveCount(moveNames.Count))
            {
                Console.WriteLine("Invalid move count! Enter an odd number of unique moves (e.g., 'rock paper scissors').");
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
