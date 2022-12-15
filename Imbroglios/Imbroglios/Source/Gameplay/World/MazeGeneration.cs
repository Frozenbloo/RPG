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

	public class MazeGeneration 
	{
		private SquareGrid grid;
		private int[,] mazeGrid;

		public const int rowDimension = 0;
		public const int columnDimension = 1;

		public SquareGrid Grid{ get; set; }
		public int[,] MazeGrid { get; set; }

		public int StartX = 0;
		public int StartY = 0;

		private int EndX = 0;
		private int EndY = 0;

		public MazeGeneration()
		{

		}

		public virtual void Generate(int[,] GRID, int ROWS, int COLUMNS)
		{
			/**
			Random rand = new Random();
			this.StartX = rand.Next(0, COLUMNS);
			this.StartY = rand.Next(0, ROWS);
			this.EndX = rand.Next(0, COLUMNS);
			this.EndY = rand.Next(0, ROWS);
			**/
			StartX = 0;
			StartY = 0;
			EndX = 5;
			EndY = 5;
		}
	}
}
