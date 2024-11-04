using ZeroElectric.Vinculum;

public class Game
{
    private PlayerCharacter player;
    private Map level01;


    const int screen_width = 240;
    const int screen_height = 136;
    Color bg_color = Raylib.YELLOW;


    public void Start()
    {
        Raylib.InitWindow(screen_width, screen_height, "Raylib");
        Raylib.SetTargetFPS(60);




        Console.CursorVisible = false;

        Initializemap();
        SetupConsole();

        player = CreateNewPlayer();
        Console.Clear();

        player = new PlayerCharacter('@', ConsoleColor.Blue);

        player.position = new Point2D(level01.mapWidth / 2, level01.mapTiles.Length / level01.mapWidth / 2);

        bool isGameRunning = true;
        while (isGameRunning && Raylib.WindowShouldClose() == false)



        {
            Raylib.BeginDrawing();
            DrawLevel();

            Console.SetCursorPosition(player.position.x, player.position.y);
            player.Draw();
            Raylib.EndDrawing();

            ConsoleKeyInfo inputKey = Console.ReadKey();
            HandlePlayerInput(inputKey, ref isGameRunning);

            Console.Clear();

        }
        Raylib.CloseWindow();
    }

    private void Initializemap()
    {
        MapLoader loader = new MapLoader();
        level01 = loader.LoadTestMap();
    }

    private void SetupConsole()
    {
        Console.WindowWidth = 60;
        Console.WindowHeight = 26;
    }

    private PlayerCharacter CreateNewPlayer()
    {
        string playerName = GetPlayerName();
        PlayerCharacter.Race playerRace = GetPlayerRace();
        PlayerCharacter.Class playerClass = GetPlayerClass();

        return new PlayerCharacter(playerName, playerRace, playerClass);
    }

    private string GetPlayerName()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Enter the player character's name");

        string playerName;
        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Your name: ");
            Console.ForegroundColor = ConsoleColor.White;
            playerName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter a valid name.");
            }
            else if (ContainsNumbers(playerName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot contain numbers. Please enter a valid name.");
            }
        } while (string.IsNullOrWhiteSpace(playerName) || ContainsNumbers(playerName));

        Console.ResetColor();
        return playerName;
    }

    private bool ContainsNumbers(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }
        return false;
    }

    private PlayerCharacter.Race GetPlayerRace()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Choose the player character's race");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("1. Human");
        Console.WriteLine("2. Elf");
        Console.WriteLine("3. Dwarf");
        Console.WriteLine("4. Penguin");

        int choice = GetValidIntegerInput("Your Race? ", 1, 4);

        Console.ResetColor();

        switch (choice)
        {
            case 1:
                return PlayerCharacter.Race.Human;
            case 2:
                return PlayerCharacter.Race.Elf;
            case 3:
                return PlayerCharacter.Race.Dwarf;
            case 4:
                return PlayerCharacter.Race.Penguin;
            default:
                throw new InvalidOperationException("Invalid race.");
        }
    }

    private PlayerCharacter.Class GetPlayerClass()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Choose the player character's class");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("1. Warrior");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Rogue");
        Console.WriteLine("4. Cleric");

        int choice = GetValidIntegerInput("Your Class? ", 1, 4);

        Console.ResetColor();

        switch (choice)
        {
            case 1:
                return PlayerCharacter.Class.Warrior;
            case 2:
                return PlayerCharacter.Class.Mage;
            case 3:
                return PlayerCharacter.Class.Rogue;
            case 4:
                return PlayerCharacter.Class.Cleric;
            default:
                throw new InvalidOperationException("Invalid class.");
        }
    }

    private int GetValidIntegerInput(string prompt, int minValue, int maxValue)
    {
        int choice;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.White;

            if (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid choice. Please enter a number between {minValue} and {maxValue}.");
            }
            else
            {
                break;
            }
        }

        Console.ResetColor();
        return choice;
    }

    private void DrawLevel()
    {
        level01.Draw();
    }

    private void HandlePlayerInput(ConsoleKeyInfo key, ref bool isRunning)
    {

        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) { MovePlayer(0, -1); }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN)) { MovePlayer(0, 1); }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT)) { MovePlayer(-1, 0); }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)) { MovePlayer(1, 0); }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) ;


        }
    }

    private void MovePlayer(int deltaX, int deltaY)
    {
        int newX = player.position.x + deltaX;
        int newY = player.position.y + deltaY;

        if (newX >= 0 && newX < level01.mapWidth && newY >= 0 && newY < level01.mapTiles.Length / level01.mapWidth)
        {
            int index = newX + newY * level01.mapWidth;
            int tileId = level01.mapTiles[index];

            if (tileId != 2)
            {
                player.Move(deltaX, deltaY);
            }
        }
    }
}