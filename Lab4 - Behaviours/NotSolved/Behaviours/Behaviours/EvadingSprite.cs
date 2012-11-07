using Microsoft.Xna.Framework;

namespace Behaviours
{
	public class EvadingSprite
	{
		public Vector2 Position { get; set; }
		private Vector2 _direction;
		public int Speed { get; set; }

		public EvadingSprite(Vector2 initialPosition, Vector2 direction)
		{
			_direction = direction;
			Position = initialPosition;
			Speed = GameRandom.Random.Next(3, 6);
		}

		public void Update(Vector2 playerPosition, Vector2 playerDirection)
		{
			// We used this before :D. We are just updating the position based on distance and speed
			Position += _direction * Speed;

		}
	}
}