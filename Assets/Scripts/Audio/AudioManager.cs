using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
	/// <summary>
	/// Singleton class used for all audio requisitions.
	/// </summary>
	public class AudioManager : MonoBehaviour
	{
		/// <summary>
		/// The instance of AudioManager.
		/// </summary>
		public static AudioManager Instance;

		/// <summary>
		/// A pool of AudioSources to be pre-loaded at the start of runtime.
		/// </summary>
		private Queue<AudioSource> _sourcePool;
		/// <summary>
		/// The size of the pool of AudioSources.
		/// </summary>
		private const int SourcePoolSize = 10;
		
		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
		
			_sourcePool = new Queue<AudioSource>();
			for (var i = 0; i < SourcePoolSize; i++)
			{
				EnqueueNewSource();
			}
		
		}

		private void EnqueueNewSource()
		{
			var s = gameObject.AddComponent<AudioSource>();
			_sourcePool.Enqueue(s);
		}

		/// <summary>
		/// Function used to play a sound.
		/// </summary>
		/// <param name="audioEvent">The audio event of the desired sound to be played.</param>
		public void Play(AudioEvent audioEvent)
		{
			if (_sourcePool.Count == 0)
			{
				EnqueueNewSource();
			}
		
			var source = _sourcePool.Dequeue();
			audioEvent.Play(source);
			StartCoroutine(ReturnSource(source));
		}
	
		private IEnumerator ReturnSource(AudioSource source)
		{
			yield return new WaitForSeconds(source.clip.length);
		
			_sourcePool.Enqueue(source);
		}
	
	}
}