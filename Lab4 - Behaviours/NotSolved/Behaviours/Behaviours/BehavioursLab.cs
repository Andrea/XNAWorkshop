using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Behaviours
{
	public class BehavioursLab : Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		
		private Texture2D _evadingFishTexture;
		private Texture2D _chasingFishTexture;
		private Texture2D _playerFishTexture;
		private Texture2D _backgroundTexture;
		
		private List<EvadingSprite> _evadingFish;
		private ChasingSprite _chasingSprite;

		private Vector2 _playerPosition;
		private Vector2 _playerDirection;
		private float _playerSpeed = 5;
		private float _playerRotation;
		private int _lastScardyFishSpawnTime;

		private const int FishySpawnInterval = 500;

		public BehavioursLab()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			_chasingSprite = new ChasingSprite();
			_evadingFish = new List<EvadingSprite>();

			_playerPosition = new Vector2(
									GraphicsDevice.PresentationParameters.BackBufferWidth/2, 
									GraphicsDevice.PresentationParameters.BackBufferHeight/2);
			_playerDirection = new Vector2(1, 0);
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_evadingFishTexture = Content.Load<Texture2D>(@"images\food");
			_chasingFishTexture = Content.Load<Texture2D>(@"images\fishbaddie_parts_fixed");
			_playerFishTexture = Content.Load<Texture2D>(@"images\fishsalmon_parts_fixed");
			
			_backgroundTexture = Content.Load<Texture2D>(@"images\background");
		}

		protected override void UnloadContent()
		{
			
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			
			var keyboardState = Keyboard.GetState();

			/* TODO: spawn evading fish every FishSpawnInterval
			 * 
			 * Hint you ll need to know the amount of time that pased since the _lastScardyFishSpawnTime.
			 * 
			 * When you know that you are spaning giving the interval, you will need to set an initial 
			 * position (for the evading fish), with that info create an EvadingSprite and add it to the 
			 * _evadingSprites list
			 * Finally set the _lastScardyFishSpawnTime to the elapsed totalMiliseconds 
			 */


			foreach (var evadingSprite in _evadingFish)
			{
				evadingSprite.Update(_playerPosition, _playerDirection);
			}

			/* Update the chasing sprite.
			 * +++Question 4: Why are we sending _playerPosition to the chasing sprite instance?
			*/
			_chasingSprite.Update(_playerPosition);

			var shouldMove = false;
			if(keyboardState.IsKeyDown(Keys.Left))
			{
				/*
				 * +++Question 5: Why are we adding the _playerDirection vector to Vector2(-1, 0) 
				 * hint: this same aproach is used elsewhere
				 */
				_playerDirection = (_playerDirection + new Vector2(-1, 0)) / 2;
				shouldMove = true;
			}
			else if(keyboardState.IsKeyDown(Keys.Right))
			{
				_playerDirection = (_playerDirection + new Vector2(1, 0)) / 2;
				shouldMove = true;
			}
			else if (keyboardState.IsKeyDown(Keys.Up))
			{
				_playerDirection = (_playerDirection + new Vector2(0, -1)) / 2;
				shouldMove = true;
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				_playerDirection = (_playerDirection + new Vector2(0, 1)) / 2;
				shouldMove = true;
			}

			if(shouldMove)
			{
				_playerPosition += _playerDirection * _playerSpeed;
			}
			
			_playerRotation = (float) Math.Atan2(_playerDirection.Y, _playerDirection.X);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(new Color(102, 204, 255));
			
			_spriteBatch.Begin();

			// Draw the background
			var backgroundDestination = new Rectangle(0, GraphicsDevice.PresentationParameters.BackBufferHeight - _backgroundTexture.Height, 
			                                         GraphicsDevice.PresentationParameters.BackBufferWidth
			                                         ,_backgroundTexture.Height);
			_spriteBatch.Draw(_backgroundTexture, backgroundDestination, Color.White);

			// Draw the player
			var playerRectangle = new Rectangle(0, 0, 33, 33);
			_spriteBatch.Draw(_playerFishTexture, _playerPosition, playerRectangle, Color.White, _playerRotation, Vector2.Zero, 1, SpriteEffects.None, 1);

			//Draw the evading fish
			var evadingRectangle = new Rectangle(0, 0, 25, 25);
			float evadingFishRotation = (float)Math.PI; // TODO: It could rotate like the other fish
			foreach (var evadingSprite in _evadingFish)
			{
				_spriteBatch.Draw(_evadingFishTexture, evadingSprite.Position, evadingRectangle , Color.White, evadingFishRotation,
					Vector2.Zero,
					1,
					SpriteEffects.FlipVertically,
					0);
			}

			//Draw the chasing fish
			_spriteBatch.Draw(_chasingFishTexture, _chasingSprite.Position, new Rectangle(0, 0, 40, 40), Color.White, _chasingSprite.Rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
			
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
