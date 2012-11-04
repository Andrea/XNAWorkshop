using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdditiveBlending
{
	public class Game1 : Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		private Texture2D _red;
		private Texture2D _green;
		private Texture2D _blue;

		private float _blueAngle = 0;
		private float _greenAngle = 0;
		private float _redAngle = 0;

		private const float BlueSpeed = 0.025f;
		private const float GreenSpeed = 0.017f;
		private const float RedSpeed = 0.022f;

		private float distance = 100;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_red = Content.Load<Texture2D>("red");
			_green = Content.Load<Texture2D>("green");
			_blue = Content.Load<Texture2D>("blue");
			// TODO: use this.Content to load your game content here
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			_blueAngle += BlueSpeed;
			_greenAngle += GreenSpeed;
			_redAngle += RedSpeed;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			Vector2 bluePosition = new Vector2(
				(float)Math.Cos(_blueAngle) * distance,
				(float)Math.Sin(_blueAngle) * distance);
			Vector2 greenPosition = new Vector2(
							(float)Math.Cos(_greenAngle) * distance,
							(float)Math.Sin(_greenAngle) * distance);
			Vector2 redPosition = new Vector2(
							(float)Math.Cos(_redAngle) * distance,
							(float)Math.Sin(_redAngle) * distance);

			Vector2 center = new Vector2(300, 140);


			/* TODO: use different blend states to blend the textures when they overlap
			 */

			base.Draw(gameTime);
		}
	}
}
