using System;
using System.Collections.Generic;
using System.Text;

namespace SparrowEditor.Sparrow
{
	/// <summary>
	/// Main Head of parsing SparrowAtlas elements and nodes
	/// </summary>
	class SparrowFormat
	{
		/*
			BASE DATA SOURCE!

		```
			<TextureAtlas imagePath="spritesheet.png">
				<SubTexture name="BF HEY!!0000" x="1150" y="0" width="395" height="416" pivotX="-298.8" pivotY="206.95" frameX="0" frameY="0" frameWidth="415" frameHeight="418"/>
			</TextureAtlas>
		```

		NOTE: imagePath seems not useful at all, and Idk where it is used lol
		Since most engines ignores this element btw
		*/

		/// <summary>
		/// Frame Name
		/// </summary>
		public static string name = ""; 

		/// <summary>
		/// Left Sprite position in whole image
		/// </summary>
		public static double x = 0;

		/// <summary>
		/// Top sprite position in whole image
		/// </summary>
		public static double y = 0;

		/// <summary>
		/// Sprite Width
		/// </summary>
		public static double width = 0;

		/// <summary>
		/// Sprite Height
		/// </summary>
		public static double height = 0;

		/// <summary>
		/// 90º Rotated Sprite
		/// </summary>
		public static bool rotated = false;

		/// <summary>
		/// Horizontal flipped Sprite?
		/// </summary>
		public static bool flipX = false;

		/// <summary>
		/// Vertical flipped Sprite?
		/// </summary>
		public static bool flipY = false;

		// --- FRAME ---

		/// <summary>
		/// Rendered frame position X - Named as "pivotX" in SparrowV1
		/// </summary>
		public static double frameX = 0;

		/// <summary>
		/// Rendered frame position Y - Named as "pivotY" in SparrowV1
		/// </summary>
		public static double frameY = 0;

		/// <summary>
		/// Rendered Frame Width - NESCESSARY IF FRAME(X/Y) IS SET
		/// </summary>
		public static double frameWidth = 0;

		/// <summary>
		/// Rendered Frame Height - NESCESSARY IF FRAME(X/Y) IS SET
		/// </summary>
		public static double frameHeight = 0;

	}
}
