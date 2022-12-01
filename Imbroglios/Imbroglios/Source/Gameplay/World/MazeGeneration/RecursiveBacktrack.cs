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
	public enum Directions
	{
		N = 1,
		S = 2,
		E = 4,
		W = 8
	}

	public class RecursiveBacktrack 
	{
		private SquareGrid grid;

		private const int _rowDimension = 0;
		private const int _columnDimension = 1;

		private int StartX = 0;
		private int StartY = 0;

		private int EndX = 0;
		private int EndY = 0;

		public RecursiveBacktrack(SquareGrid GRID) 
		{
			grid = GRID;
		}

		private void CarvePassageFrom(int X, int Y, SquareGrid GRID)
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

				if (IsOutOfBounds(nextX, nextY, grid))
					continue;

				if (GRID.slots[X][Y] != 0) // has been visited
					continue;

				GRID[Y][X] |= (int)direction;
				GRID[Y][X] |= (int)Opposite[direction];

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

		private bool IsOutOfBounds(int x, int y, SquareGrid grid)
		{
			if (x < 0 || x > grid.gridDims.X - 1)
			{
				return true;
			}


			if (y < 0 || y > grid.gridDims.Y - 1) 
			{
				return true; 
			}

			return false;
		}
	}
}
