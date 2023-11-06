namespace minesweep
{
	internal class Program
	{
		/// <summary>
		/// The main entry point for the Minesweeper game.
		/// </summary>
		static void Main(string[] args)
		{
			const int rows = 5;
			const int columns = 5;
			const int totalMines = 5;
			int[,] board = new int[rows, columns];
			int score = 0;

            Console.WriteLine("Welcome to MineSweeper: ");
            Console.WriteLine($"Sweep the coordinates from A1 to {(char)(rows-1+'A')}{columns}");
			Console.WriteLine($"1 point per clean sweep - Game ends when you hit a mine!");

            InitializeMines(board, totalMines, rows, columns);
			PlayGame(board, rows, columns, ref score);
			PrintMines(board, rows, columns);
            Console.ReadLine();
        }

		/// <summary>
		/// Initializes the mines on the board.
		/// </summary>
		/// <param name="board">The game board.</param>
		/// <param name="totalMines">The total number of mines to be placed on the board.</param>
		/// <param name="rows">The number of rows on the board.</param>
		/// <param name="columns">The number of columns on the board.</param>
		static void InitializeMines(int[,] board, int totalMines, int rows, int columns)
		{
			Random random = new Random();
			for (int i = 0; i < totalMines; i++)
			{
				int row, col;
				do
				{
					row = random.Next(0, rows);
					col = random.Next(0, columns);
				} while (board[row, col] == -1);

				board[row, col] = -1;
			}
		}

		/// <summary>
		/// Starts and controls the main gameplay loop of the Minesweeper game.
		/// </summary>
		/// <param name="board">The game board.</param>
		/// <param name="rows">The number of rows on the board.</param>
		/// <param name="columns">The number of columns on the board.</param>
		/// <param name="score">The current score of the player.</param>
		static void PlayGame(int[,] board, int rows, int columns, ref int score)
		{
			while (true)
			{
				Console.WriteLine("Enter coordinates (e.g. D3 or C5): ");
				string input = Console.ReadLine().ToUpper();

				if (!ValidateInput(input, rows, columns))
					continue;

				int row = input[0] - 'A';
				int col = input[1] - '1';

				if (board[row,col] == 1)
				{
                    Console.WriteLine("Coordinates already sweeped!");
                }
				else if (board[row, col] == -1)
				{
					Console.WriteLine("You hit a mine! Game over. Final score: " + score);
					break;
				}
				else
				{
					score++;
					Console.WriteLine("Miss! Your score: " + score);
					board[row, col] = 1; //mark the coordinates as checked!
				}
			}
        }

		static void PrintMines(int[,] board, int rows, int cols)
		{
			Console.Write("Mines Located: ");
			for (int row = 0; row < rows; row ++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (board[row,col] == -1)
					{
						Console.Write($"{(char)(row + 'A')}{col + 1} ");
					}
				}
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Validates the user input to ensure it is in the correct format and within the board's boundaries.
		/// </summary>
		/// <param name="input">The user input string.</param>
		/// <param name="rows">The number of rows on the board.</param>
		/// <param name="columns">The number of columns on the board.</param>
		/// <returns>True if the input is valid, otherwise false.</returns>
		static bool ValidateInput(string input, int rows, int columns)
		{
			if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
			{
				Console.WriteLine("Invalid input. Please try again.");
				return false;
			}

			int row = input[0] - 'A';
			int col = input[1] - '1';

			if (row <= 0 || row >= rows || col <= 0 || col >= columns)
			{
				Console.WriteLine("Coordinates out of range. Please try again.");
				return false;
			}

			return true;
		}
	}
}