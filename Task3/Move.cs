namespace Task3;
public class Move
{
    private static int Counter = 1;
    public int Id { get; }
    public string Name { get; set; }

    public Move()
    {
        Id = Counter++;
    }
}
