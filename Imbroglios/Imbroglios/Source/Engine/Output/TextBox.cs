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
using System.Linq;
#endregion

namespace Imbroglios
{
	public class TextBox
	{
		public int maxWidth, lineHeight;
		string text;
		public Vector2 position, dimensions;
		public Color fontColour;
		public SpriteFont font;

		public List<string> splitText = new List<string>();

		/// <summary>
		/// Creates a new Text Box
		/// </summary>
		/// <param name="POS">The text box location</param>
		/// <param name="STR">The text</param>
		/// <param name="MAXWIDTH">The maximum width the text can stretch to</param>
		/// <param name="LINEHEIGHT">The line height</param>
		/// <param name="FONT">The string to the font location</param>
		/// <param name="FONTCOLOUR">The font colour</param>
		public TextBox(Vector2 POS, string STR, int MAXWIDTH, int LINEHEIGHT, string FONT, Color FONTCOLOUR)
		{
			this.position = POS;
			this.maxWidth = MAXWIDTH;
			this.lineHeight = LINEHEIGHT;
			this.text = STR;
			this.fontColour = FONTCOLOUR;

			font = Globals.content.Load<SpriteFont>(FONT);

			if (text != "")
			{
				ParseText();
			}
		}

		#region Properties
		public string Text
		{
			get { return text; }
			set 
			{ 
				text = value;
				ParseText();
			}
		}
		#endregion

		/// <summary>
		/// Parses text to ensure it fits inside the dimensions of the text box
		/// </summary>
		public virtual void ParseText()
		{
			this.splitText.Clear();

			List<string> wordList = new List<string>();
			string tempString = string.Empty;

			int biggestWidth = 0, currentWidth = 0;

			if (text != "" && text != null)//error protection if init poorly
			{
				wordList = text.Split(' ').ToList<string>();

				for (int i = 0; i < wordList.Count; i++)
				{
					if (tempString != "")
					{
						tempString += " ";//adds space after it was removed above
					}
					currentWidth = (int)(font.MeasureString(tempString + wordList[i]).X);

					if (currentWidth > biggestWidth && currentWidth <= maxWidth)
					{
						biggestWidth = currentWidth;
					}

					if (currentWidth <= maxWidth)
					{
						tempString += wordList[i];
					}
					else
					{
						splitText.Add(tempString);
						tempString = wordList[i];//stops infinite loop
					}
				}
				if (tempString != "")
				{
					splitText.Add(tempString);
				}
				SetDimensions(biggestWidth);//resizes to the largest width if not the max width
			}
		}

		public virtual void SetDimensions(int MAXWIDTH)
		{
			dimensions = new Vector2(this.maxWidth, this.lineHeight * splitText.Count);
		}

		public virtual void Draw(Vector2 OFFSET)
		{
			for (int i = 0; i < splitText.Count; i++)
			{
				Globals.spriteBatch.DrawString(font, splitText[i], OFFSET + new Vector2(position.X, position.Y + (lineHeight * i)), fontColour);
			}
		}
	}
}
