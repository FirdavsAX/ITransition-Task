using ConsoleTables;

namespace Task3.Services
{
    public class GameService
    {
        public int DetermineWinner(Move computerMove, Move userMove, int moveCount)
        {
            if (IsDraw(computerMove, userMove))
                return 0; // Draw

            return IsUserWin(computerMove, userMove, moveCount) ? 1 : -1;
        }

        private bool IsDraw(Move computerMove, Move userMove) => computerMove.Id == userMove.Id;

        private bool IsUserWin(Move computerMove, Move userMove, int moveCount)
        {
            int half = moveCount / 2;
            return (userMove.Id - computerMove.Id + moveCount) % moveCount <= half;
        }

        public void DisplayHelp(List<Move> moves)
        {
            var table = CreateHelpTable(moves);
            table.Write();
        }

        private ConsoleTable CreateHelpTable(List<Move> moves)
        {
            var headers = new List<string> { "PC\\User" };
            headers.AddRange(moves.Select(m => m.Name));

            var table = new ConsoleTable(headers.ToArray());

            foreach (var move in moves)
            {
                var row = GenerateRow(moves, move);
                table.AddRow(row.ToArray());
            }

            return table;
        }

        private List<string> GenerateRow(List<Move> moves, Move currentMove)
        {
            var row = new List<string> { currentMove.Name };

            foreach (var move in moves)
            {
                row.Add(GetOutcome(move, currentMove, moves.Count));
            }

            return row;
        }

        private string GetOutcome(Move move, Move currentMove, int moveCount)
        {
            if (move.Id == currentMove.Id)
                return "Draw";

            return IsUserWin(currentMove, move, moveCount) ? "Lose" : "Win";
        }

        public void DisplayMoves(List<Move> moves)
        {
            for (int i = 0; i < moves.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {moves[i].Name}");
            }
            Console.WriteLine("0: Exit\n-1: Help");
        }
    }
}
