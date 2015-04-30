using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
		public static AudioController instance = null;
		public AudioSource[] SoundFX;
		public AudioClip[] AmbientMusic;
		public bool FX, Music;
		// Use this for initialization
		void Start ()
		{
				
		}

		void Awake ()
		{	
			if (instance == null){
				instance = this;
			} else if (instance!=this){
				Destroy(gameObject);
			}

				DontDestroyOnLoad (gameObject);

			AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
				foreach(AudioSource sound in sounds){
				sound.panStereo = 0f;
			}
			
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				SoundFX = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
				if (!FX) {
						foreach (AudioSource soundFX in SoundFX) {
								if (soundFX.clip != AmbientMusic [0] && soundFX.clip != AmbientMusic [1] && soundFX.clip != AmbientMusic [2] && soundFX.clip != AmbientMusic [3]) {
										soundFX.volume = 0;	
								}
						}
				}
				if (FX) {
						foreach (AudioSource soundFX in SoundFX) {
								if (soundFX.clip != AmbientMusic [0] && soundFX.clip != AmbientMusic [1] && soundFX.clip != AmbientMusic [2] && soundFX.clip != AmbientMusic [3]) {
										soundFX.volume = 1f;	
								}
						}
				}
				if (!Music) {
						foreach (AudioSource soundFX in SoundFX) {
								if (soundFX.clip == AmbientMusic [0] || soundFX.clip == AmbientMusic [1] || soundFX.clip == AmbientMusic [2] || soundFX.clip == AmbientMusic [3]) {
										soundFX.volume = 0;	
								}
						}

				}
				if (Music) {
						foreach (AudioSource soundFX in SoundFX) {
								if (soundFX.clip == AmbientMusic [0] || soundFX.clip == AmbientMusic [1] || soundFX.clip == AmbientMusic [2] || soundFX.clip == AmbientMusic [3]) {
										soundFX.volume = 0.7f;	
								}
						}
				}
		}
}
