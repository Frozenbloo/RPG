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
	public class Wall : Basic2D
	{
		public bool isFilled, imPassable /**You can't walk through it and it counts as being filled**/ , unPathable /**You can't walk through it but isnt filled**/, beenUsed, isViewable;

		public Wall(string filePATH, Vector2 POS, Vector2 DIMS, bool FILLED, bool PASSABLE, bool PATHABLE, bool VIEWABLE) : base(filePATH, POS, DIMS)
		{
			
		}

		public override void Update(Vector2 OFFSET)
		{
			base.Update(OFFSET);
		}

		public override void Draw(Vector2 OFFSET)
		{
			base.Draw(OFFSET);
		}
	}
}
