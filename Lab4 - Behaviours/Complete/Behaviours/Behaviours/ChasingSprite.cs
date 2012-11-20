using System;
using Microsoft.Xna.Framework;

namespace Behaviours
{
	public class ChasingSprite
	{
		private Vector2 _direction;
		private int _speed = 3;

		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		
		public void Update(Vector2 playerPosition)
		{

			/* When we substract the position(of the evading sprite) from the player position
			 * we get the vector in between the two. 
			 * So if we want to know how far away one from the other is, then we can just use Lenght on the
			 * resulting vector
			*/
			var vectorToPlayer = playerPosition - Position;
			if (vectorToPlayer.Length() < 10)
			{
				//+++Question 1: Why we return here? 
				return;
			}
			// ++++Question 2:  Why we normalize here (use the web to check what normalize does)
			vectorToPlayer.Normalize();


			/* This is known as the half vector. Given two vectors, if you add them and divided by two we get 
			 * the vector exactly in between two vectors. This is really handy in this situation so when the fish is 
			 * turning it doesnt change sudenly
			 */
			_direction = (vectorToPlayer + _direction) / 2;

			// We used this before :D. We are just updating the position based on distance and speed
			Position += _direction * _speed;

			/* The rotation can be calculated using ATan2 on the position. 
			 * ++++Super Bonus Question 3: There is something very unconventional about this Math function (compared to the other ones
			 * we used). What is it?			 * 
			*/
			Rotation = (float) Math.Atan2(_direction.Y, _direction.X);
		}
	}
}