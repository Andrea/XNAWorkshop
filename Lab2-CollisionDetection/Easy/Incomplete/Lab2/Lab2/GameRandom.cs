using System;

namespace Lab2
{
	public static class GameRandom
	{
		private static readonly Random _random;
		
		static GameRandom()
		{
			_random = new Random((int) DateTime.Now.Ticks);
		}

		public static Random Random { get { return _random; }  }
	}
}