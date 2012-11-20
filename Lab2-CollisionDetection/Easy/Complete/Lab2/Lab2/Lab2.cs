using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab2
{
	public class Lab2 : Game
	{
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
		private readonly Vector2 _turretOrigin = new Vector2(29, 29);
		private bool _bulletFired = false;

		public Lab2()
		{
			_graphics = new GraphicsDeviceManager(this)
							{
								PreferredBackBufferWidth = 1280,
								PreferredBackBufferHeight = 720
							};
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_bubbles = new List<Bubble>();
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			for (int i = 0; i < 20; i++)
			{
				var bubble = new Bubble(this, _spriteBatch, _graphics.GraphicsDevice.Viewport.TitleSafeArea);
				_bubbles.Add(bubble);
				Components.Add(bubble);
			}

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_bulletTexture = Content.Load<Texture2D>("bullet");

			_turret = Content.Load<Texture2D>("turret");
			_turretPosition = new Vector2((GraphicsDevice.Viewport.Width - _turret.Width) / 2, GraphicsDevice.Viewport.Height - 50);
			_turretRotation = 0;// MathHelper.PiOver2;
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
				_turretRotation -= 0.05f;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				_turretRotation += 0.05f;
			}

			if (_turretRotation > MathHelper.PiOver4 * 3)
			{
				_turretRotation = MathHelper.PiOver4 * 3;
			}
			else
			{
				if (_turretRotation < MathHelper.PiOver4)
				{
					_turretRotation = MathHelper.PiOver4;
				}
			}


			if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_bulletFired)
			{
				Fire();
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Space))
			{
				_bulletFired = false;
			}

			for (var i = _bullets.Count - 1; i >= 0; i--)
			{
				var bullet = _bullets[i];
				Vector2 position = bullet.Position + bullet.Direction * 5;
				bullet.Position = position;

				//Remove bullets we can't see anymore
				if (bullet.Position.X < 0 || bullet.Position.X > GraphicsDevice.Viewport.Width ||
					bullet.Position.Y < 0)
					_bullets.RemoveAt(i);
			}

			//Check for collisions between bubles and bullets

			foreach (var bullet in _bullets)
			{
				bullet.Update();
				foreach (var bubble in _bubbles)
				{
					if (bubble.BoundingBox.Intersects(bullet.BoundingBox))
					{
						_bubblesToBeRemoved.Add(bubble);
						_bulletsToBeRemoved.Add(bullet);
					}
				}
			}

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
			var unitRotationVector = new Vector2(
								(float)Math.Cos(_turretRotation),
								(float)-Math.Sin(_turretRotation)
							);

			var turretTipPosition = (unitRotationVector * _turretOrigin.X) + _turretPosition -
				 new Vector2((float)_bulletTexture.Width / 2, (float)_bulletTexture.Height / 2);
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

			_spriteBatch.Draw(_turret, _turretPosition, null, Color.White, -_turretRotation, _turretOrigin, 1, SpriteEffects.None, 0);

			foreach (var bullet in _bullets)
			{
				_spriteBatch.Draw(bullet.BulletTexture, bullet.Position, Color.Purple);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
