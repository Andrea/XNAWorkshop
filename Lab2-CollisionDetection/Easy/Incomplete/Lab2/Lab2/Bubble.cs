using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab2
{
	public class Bubble : DrawableGameComponent
	{
		private const float DefaultSpeed =5;

		private readonly SpriteBatch _spriteBatch;
		private readonly ContentManager _content;
		private readonly Rectangle _titleSafeArea;
		
		private Texture2D _bubbleTexture;
		private Vector2 _direction;
		private Rectangle _boundingBox;
		private int _divider;
		private float[] _sizes = new[] { 1f, 2f, 4f, 8f };

		private Color _colour;
		private Vector2 _position;

		public Bubble(Game game, SpriteBatch spriteBatch, Rectangle titleSafeArea)
			: base(game)
		{
			_spriteBatch = spriteBatch;
			_titleSafeArea = titleSafeArea;
			_content = game.Content;
			_direction = new Vector2((float)(GameRandom.Random.NextDouble() * 2 - 1));
			// if we wanted all the bubbles at the same speed we should do:
			//			_direction.Normalize();
			// this is because when we create a vector with a float, we could create something like a new vector with 0.1, 0.1 ,
			// that vector would be shorter than a vector that  is closer to 1,1
			// Because random duble gets you a value between 0 and 1

			
			_divider = GameRandom.Random.Next(0,3);
			_colour = Color.CornflowerBlue;
		}

		protected override void LoadContent()
		{
			base.LoadContent();
			_bubbleTexture = _content.Load<Texture2D>("bubble");
			_position = new Vector2(GameRandom.Random.Next(_titleSafeArea.Width - _bubbleTexture.Width),
				GameRandom.Random.Next(_titleSafeArea.Height - _bubbleTexture.Height - 200));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			/* TODO: Update the position using the _direction vector and the DefaultSpeed
			 */
			this._position += _direction*DefaultSpeed*(float)gameTime.ElapsedGameTime.TotalSeconds;

			_boundingBox = new Rectangle((int)_position.X, (int)_position.Y,
				(int)(_bubbleTexture.Width / _sizes[_divider]), (int)(_bubbleTexture.Height / _sizes[_divider]));

			if (_position.X < 0)
			{
				_position.X = 0;
				_direction = Vector2.Reflect(_direction, new Vector2(1, 0));
			}
			else if (_position.X + _bubbleTexture.Width > _titleSafeArea.Width)
			{
				_position.X = _titleSafeArea.Width - _bubbleTexture.Width;
				_direction = Vector2.Reflect(_direction, new Vector2(-1, 0));
			}

			if (_position.Y < 0)
			{
				_position.Y = 0;
				_direction = Vector2.Reflect(_direction, new Vector2(0, 1));
			}
			else if (_position.Y + _bubbleTexture.Height > _titleSafeArea.Height)
			{
				_position.Y = _titleSafeArea.Height - _bubbleTexture.Height;
				_direction = Vector2.Reflect(_direction, new Vector2(0, -1));
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			_spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
			_spriteBatch.Draw(_bubbleTexture, _boundingBox, _colour);
			_spriteBatch.End();
		}

		public Rectangle BoundingBox
		{
			get { return _boundingBox; }
		}
	}
}