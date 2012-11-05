using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab2
{
	public class Lab2 : Game
	{
		private const int NumberOfBubbles = 20;
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _turret;
		private Texture2D _bulletTexture;

		private Vector2 _turretPosition;
		private List<Bullet> _bullets = new List<Bullet>();
		private List<Bullet> _bulletsToBeRemoved = new List<Bullet>();
		private List<Bubble> _bubblesToBeRemoved = new List<Bubble>();

		private float _turretRotation;
		private List<Bubble> _bubbles;
		private readonly Vector2 _turretOrigin = new Vector2(53, 29);
		private bool _bulletFired = false;

		public Lab2()
		{
			//NOTE: Why is are the properties below prepended with "Prefered"
			_graphics = new GraphicsDeviceManager(this)
							{
								PreferredBackBufferWidth = 1280,
								PreferredBackBufferHeight = 720,
							};
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_bubbles = new List<Bubble>();
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			/*TODO: Create NumberOfBubbles bubbles. 
			 * There is a bubbles collection used to keep track of all bubbles, make sure you take
			 * this into account.
			 * 
			 * Hint: Bubble is a drawable component, needs to be added to a components collection
			 */

			

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_bulletTexture = Content.Load<Texture2D>("bullet");

			_turret = Content.Load<Texture2D>("turret");
			
			/* TODO: change _turretPosition so that it sets the turret exactly in the middle 
			 * of the screen regarding X. And 50 pixels  off the bottom of the screen
			 */
			_turretPosition = new Vector2();
			
			/* TODO: change the _turretRotation value so that it points up. Rotation is an angle 
			 * uses radians (as opposed to degrees)
			 */
			_turretRotation = 0;
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				_turretRotation += 0.05f;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				_turretRotation -= 0.05f;
			}


			/* The code below limits the turret rotation
			 * TODO: change it so that it has an amplitude of 180 degrees
			 */

			if (_turretRotation > MathHelper.PiOver4 * 3)
			{
				_turretRotation = MathHelper.PiOver4 * 3;
			}
			else
			{
				if (_turretRotation < MathHelper.PiOver4)
					_turretRotation = MathHelper.PiOver4;
			}


			if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_bulletFired)
			{
				Fire();
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Space))
			{
				_bulletFired = false;
			}

			//Remove bullets we can't see anymore
			for (var i = _bullets.Count - 1; i >= 0; i--)
			{
				var bullet = _bullets[i];
				Vector2 position = bullet.Position + bullet.Direction * 5;
				bullet.Position = position;

				if (bullet.Position.X < 0 || bullet.Position.X > GraphicsDevice.Viewport.Width ||
					bullet.Position.Y < 0)
					_bullets.RemoveAt(i);
			}

			/* TODO: Check for collisions between bubles and bullets
			 * 
			 * To achieve this you need to check every bullet against every bubble.
			 * Use XNA Rectangle intersects method to determine whether the bullet and bubble are on the same space
			 * 
			 * You also will have to update the *ToBeRemoved collections.
			 * 
			 * Hint: dont forget to update each of the bullets so that their Boundingbox is updated
			 */
			

			foreach (var bubble in _bubblesToBeRemoved)
			{
				Components.Remove(bubble);
				_bubbles.Remove(bubble);
			}

			foreach (var bullet in _bulletsToBeRemoved)
			{
				_bullets.Remove(bullet);
			}

			_bubblesToBeRemoved.Clear();
			_bulletsToBeRemoved.Clear();

			base.Update(gameTime);
		}

		private void Fire()
		{
			_bulletFired = true;

			/* TODO using trigonometric functions find the unitRotationVector using
			 * the turret rotation value. 
			 * This value is used to determine the direction of the bullet.
			 */
			var unitRotationVector = new Vector2();

			var turretTipPosition = (unitRotationVector * _turretOrigin.X) + _turretPosition -
				 new Vector2(_bulletTexture.Width / 2, _bulletTexture.Height / 2);
			_bullets.Add(new Bullet(_bulletTexture)
							{
								Position = turretTipPosition,
								Direction = unitRotationVector
							});
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.LightBlue);

			_spriteBatch.Begin();
			/*
			 * TODO: Draw the turret.
			 * 
			 * You will need to use the origin, the origin is where the turret moves from, imagine 
			 * a fixed pivot where the turret moved around
			 */
			

			foreach (var bullet in _bullets)
			{
				_spriteBatch.Draw(_bulletTexture, bullet.Position, Color.Purple);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
