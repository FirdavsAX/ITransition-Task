namespace Task3
{
    public static class InputError
    {
        public static void DisplayEmptyInputError()
        {
            Console.WriteLine("Error: No input provided. Please enter valid moves.");
        }

        public static void DisplayDuplicateInputError()
        {
            Console.WriteLine("Error: Duplicate moves detected. Please enter unique moves.");
        }

        public static void DisplayInvalidMoveCountError()
        {
            Console.WriteLine("Error: Invalid move count! Enter an odd number of unique moves (e.g., 'rock paper scissors').");
        }

        public static void DisplayInvalidSelectionError()
        {
            Console.WriteLine("Error: Invalid selection! Please enter a valid number.");
        }
    }
}
