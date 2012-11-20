using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab2
{
	public class Bullet
	{
		public Rectangle BoundingBox { get; private set; }
		public Texture2D BulletTexture { get; private set; }

		public Bullet(Texture2D bulletTexture)
		{
			BulletTexture = bulletTexture;
			BoundingBox = new Rectangle((int) Position.X, (int) Position.Y,
			                            BulletTexture.Width, BulletTexture.Height);
		}

		public Vector2 Position { get; set; }
		public Vector2 Direction { get; set; }

		public void Update()
		{
			BoundingBox = new Rectangle((int) Position.X, (int) Position.Y,
			                            BulletTexture.Width, BulletTexture.Height);
		}
	}
}