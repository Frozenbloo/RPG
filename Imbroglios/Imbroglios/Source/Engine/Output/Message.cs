#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Imbroglios
{
	public class Message
	{
		public bool canRemove, lockScreen;
		public Vector2 position, dimensions;
		public Color fontColour;

		public TextBox textBox;

		public Basic2D background;

		public TBTimer messageDuration;

		public Message(Vector2 POS, Vector2 DIMS, string MSG, int DUR, Color COLOR, bool LOCK, Basic2D BACKGROUND) 
		{
			this.position = POS;
			this.dimensions = DIMS;
			this.fontColour = COLOR;
			this.lockScreen = LOCK;
			canRemove = false;

			textBox = new TextBox(new Vector2(0, 0), MSG, (int)(dimensions.X *.9), 22, "Fonts\\Kenney Mini", fontColour);

			this.background = BACKGROUND;

			messageDuration = new TBTimer(DUR);
		}

		public virtual void Update()
		{
			messageDuration.UpdateTimer();
			if (messageDuration.Test())	
			{
				canRemove = true;
			}
			textBox.fontColour = fontColour * (float)(0.9 * (float)(messageDuration.MSec - (float)messageDuration.Timer) / (float)messageDuration.MSec);
		}

		public virtual void Draw()
		{
			textBox.Draw(new Vector2(position.X-textBox.dimensions.X/2, position.Y));
			background.Draw(new Vector2(position.X - textBox.dimensions.X / 2, position.Y));
		}
	}
}
