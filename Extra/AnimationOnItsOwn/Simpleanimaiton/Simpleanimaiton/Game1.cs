using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simpleanimaiton
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		
		private Texture2D _spriteSheet;
		private Vector2 _position;
		private int _currentY;
		private int _currentX;
		private double _timeSinceLastFrame;
		private const int Width = 48;
		private const int Height = 65;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_spriteSheet = Content.Load<Texture2D>("mario2");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			_currentY = 1;

			_position +=new Vector2(3,0);
			var elapsedtime = (int)gameTime.ElapsedGameTime.TotalMilliseconds;

			var milisecondsPerFrame = 60;

			_timeSinceLastFrame += elapsedtime;
			if(_timeSinceLastFrame > milisecondsPerFrame)
			{
				_currentX++;
				_timeSinceLastFrame -= milisecondsPerFrame;
				if (_currentX > 3)
					_currentX = 0;
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			
			var sourceRectangle = new Rectangle(_currentX*Width, _currentY*Height, Width, Height);
			spriteBatch.Begin();
			spriteBatch.Draw(_spriteSheet, _position, sourceRectangle, Color.White);
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
