using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

		public AudioClip[] audioClips;
		public Sprite[] sprite;
		public SpriteRenderer spriteRender;
		public GameObject gameCtrl;

		private GameController bubbleMatrix;
		private AudioSource audioSource;

		void Awake ()
		{
				audioSource = GetComponent<AudioSource> ();
				bubbleMatrix = gameCtrl.GetComponent<GameController> ();
				if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5") {
						bubbleMatrix.bubblesVariety = 3;
						bubbleMatrix.rows = 5;
						bubbleMatrix.bubblesAmount = 25;
						bubbleMatrix.bubblesLimit = 20;
				} else if (Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10" || Application.loadedLevelName == "Level11") {
						bubbleMatrix.bubblesVariety = 4;
						bubbleMatrix.rows = 6;
						bubbleMatrix.bubblesAmount = 25;
						bubbleMatrix.bubblesLimit = 15;
				} else if (Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
						bubbleMatrix.bubblesVariety = 5;
						bubbleMatrix.rows = 7;
						bubbleMatrix.bubblesAmount = 35;
						bubbleMatrix.bubblesLimit = 10;
				} else {
						bubbleMatrix.bubblesVariety = 5;
						bubbleMatrix.rows = 8;
						bubbleMatrix.bubblesAmount = 50;
						bubbleMatrix.bubblesLimit = 5;
				}

				if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5" || Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10") {
						audioSource.clip = audioClips [0];
						audioSource.Play ();
						FindObjectOfType<Shoot> ().level = 1;
						spriteRender.sprite = sprite [0];
				} else if (Application.loadedLevelName == "Level11" || Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
						audioSource.clip = audioClips [1];
						audioSource.Play ();
						FindObjectOfType<Shoot> ().level = 2;
						spriteRender.sprite = sprite [1];
				} else {
						audioSource.clip = audioClips [2];
						audioSource.Play ();
						FindObjectOfType<Shoot> ().level = 3;
						spriteRender.sprite = sprite [2];
				}
		}
}