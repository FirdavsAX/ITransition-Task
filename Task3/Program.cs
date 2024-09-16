    using Task3;
    using Task3.Services;

    var moveService = new MoveService();
    var moves = moveService.GetMoves();

    var gameService = new GameService();
    var rng = new Random();
    var hmacCalculator = new HmacCalculator();
    var keyGenerator = new RandomKeyGenerator();

    if (moves == null) return;

    var game = new Game(moves,gameService,hmacCalculator,keyGenerator,rng);
    game.Start();