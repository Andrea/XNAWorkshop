using Microsoft.Xna.Framework.Audio;

namespace Lab5
{
	public class TimedSound
	{
		private int _lastPlayedTime;
		private SoundEffect _sound;
		private int _soundInterval;

		public TimedSound(SoundEffect soundEffect, int soundInterval)
		{
			_sound = soundEffect;
			_soundInterval = soundInterval;
		}

		public void Play(int totalMilliseconds)
		{
			if(totalMilliseconds - _lastPlayedTime > _soundInterval)
			{
				_lastPlayedTime = totalMilliseconds;
				_sound.Play();
			}
		}
	}
}