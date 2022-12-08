#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Security.AccessControl;

#endregion

namespace Imbroglios
{
	public class RecursiveBacktrack : MazeGeneration
	{
		Wall mazeWall = new Wall("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(100,100), true, false, false, true);
		public RecursiveBacktrack(SquareGrid GRID, int ROWS, int COLUMNS)
		{
			Grid = GRID;
			MazeGrid = new int[(int)GRID.totalPhysicalDims.X, (int)GRID.totalPhysicalDims.Y];
			MazeGrid = Initialise(ROWS, COLUMNS);
			Generate(MazeGrid, ROWS, COLUMNS);
			GenerateMazeWalls(MazeGrid);
		}

		public override void Generate(int[,] GRID, int ROWS, int COLUMNS)
		{
			CarvePassageFrom(StartX, StartY, GRID);
		}

		public int MinSize { get; private set; }
		public int MaxSize { get; private set; }

		private void CarvePassageFrom(int X, int Y, int[,] GRID)
		{
			var directions = new List<Directions>
			{
				Directions.N,
				Directions.S,
				Directions.E,
				Directions.W
			}
			.OrderBy(x => Guid.NewGuid()); //GUID is a global unique ident, microsofts version of UUID

			foreach (var direction in directions)
			{
				var nextX = X + DirectionX[direction];
				var nextY = Y + DirectionY[direction];

				if (IsOutOfBounds(nextX, nextY, GRID))
					continue;

				if (GRID[X,Y] != 0) // has been visited
					continue;

				GRID[Y,X] |= (int)direction;
				GRID[Y,X] |= (int)Opposite[direction];

				CarvePassageFrom(nextX, nextY, GRID);
			}
		}

		private Dictionary<Directions, int> DirectionX = new Dictionary<Directions, int>
		{
			{ Directions.N, 0 },
			{ Directions.S, 0 },
			{ Directions.E, 1 },
			{ Directions.W, -1 }
		};

		private Dictionary<Directions, int> DirectionY = new Dictionary<Directions, int>
		{
			{ Directions.N, -1 },
			{ Directions.S, 1 },
			{ Directions.E, 0 },
			{ Directions.W, 0 }
		};

		private Dictionary<Directions, Directions> Opposite = new Dictionary<Directions, Directions>
		{
			{ Directions.N, Directions.S },
			{ Directions.S, Directions.N },
			{ Directions.E, Directions.W },
			{ Directions.W, Directions.E }
		};

		private bool IsOutOfBounds(int x, int y, int[,] grid)
		{
			if (x < 0 || x > grid.GetLength(rowDimension) - 1)
			{
				return true;
			}

			if (y < 0 || y > grid.GetLength(columnDimension) - 1) 
			{
				return true; 
			}

			return false;
		}

		public int[,] Initialise(int rows, int columns)
		{
			if (rows < MinSize)
				rows = MinSize;

			if (columns < MinSize)
				columns = MinSize;

			if (rows > MaxSize)
				rows = MaxSize;

			if (columns > MaxSize)
				columns = MaxSize;

			var grid = new int[rows, columns];

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					grid[i, j] = 0;
				}
			}

			return grid;
		}

		private Vector2 Position(int ROWNODE, int COLUMNNODE)
		{
			return new Vector2(100 * ROWNODE, 100 * COLUMNNODE);
		}

		private void GenerateMazeWalls(int[,] GRID)
		{
			int rows = GRID.GetLength(rowDimension);
			int colums = GRID.GetLength(columnDimension);
			//Top Line
			for (int i = 0; i < colums; i++)
			{
				//mazeWall = new Wall("2D\\Misc\\shade", Position(i, 0), new Vector2(100,100), true, false, false, true);
				new Wall("2D\\Misc\\shade", Position(i, 0), new Vector2(100, 100), true, false, false, true);
			}
		}

		public void Draw(Vector2 OFFSET)
		{
			mazeWall.Draw(OFFSET);
		}

		
	}
}
