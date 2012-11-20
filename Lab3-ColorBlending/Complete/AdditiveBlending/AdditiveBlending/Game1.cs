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

		private float blueAngle = 0;
		private float greenAngle = 0;
		private float redAngle = 0;

		private float blueSpeed = 0.025f;
		private float greenSpeed = 0.017f;
		private float redSpeed = 0.022f;

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

			blueAngle += blueSpeed;
			greenAngle += greenSpeed;
			redAngle += redSpeed;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			Vector2 bluePosition = new Vector2(
				(float)Math.Cos(blueAngle) * distance,
				(float)Math.Sin(blueAngle) * distance);
			Vector2 greenPosition = new Vector2(
							(float)Math.Cos(greenAngle) * distance,
							(float)Math.Sin(greenAngle) * distance);
			Vector2 redPosition = new Vector2(
							(float)Math.Cos(redAngle) * distance,
							(float)Math.Sin(redAngle) * distance);

			Vector2 center = new Vector2(300, 140);


			/* TODO: use different blend states to blend the textures when they overlap
			 */
			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

			_spriteBatch.Draw(_blue, center + bluePosition, Color.White);
			_spriteBatch.Draw(_green, center + greenPosition, Color.White);
			_spriteBatch.Draw(_red, center + redPosition, Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
