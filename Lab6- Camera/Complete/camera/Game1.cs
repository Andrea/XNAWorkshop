using cameraSpike;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace camera
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Texture2D _texture;
		private Vector2 _position;
		private Camera _camera;

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
			_position = new Vector2();
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

			_texture = Content.Load<Texture2D>("particle");
			_camera = new Camera(graphics.GraphicsDevice.Viewport);
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

			var keyboardState = Keyboard.GetState();
			if(keyboardState.IsKeyDown(Keys.Left))
			{
				_position -= new Vector2(5, 0);
			}
			if (keyboardState.IsKeyDown(Keys.Right))
			{
				_position += new Vector2(5, 0);
			}
			_camera.Update(gameTime, _position);
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			graphics.GraphicsDevice.Viewport = _camera.Viewport;
			spriteBatch.Begin(
				SpriteSortMode.Deferred, 
				BlendState.AlphaBlend, 
				SamplerState.PointClamp, 
				DepthStencilState.None, 
				RasterizerState.CullNone, null, _camera.Transform);
			spriteBatch.Draw(_texture, Vector2.Zero, Color.White);
			spriteBatch.Draw(_texture, _position, Color.Red);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
