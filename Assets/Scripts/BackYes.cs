using UnityEngine;
using System.Collections;

public class BackYes : MonoBehaviour
{
		public Sprite Hover, NotHover;
		public AudioClip HoverSound;

		void OnMouseEnter ()
		{
				GetComponent<AudioSource>().PlayOneShot (HoverSound, 1f);
				gameObject.GetComponent<SpriteRenderer> ().sprite = Hover;
		}
		void OnMouseExit ()
		{
				gameObject.GetComponent<SpriteRenderer> ().sprite = NotHover;
		}
		void OnMouseDown ()
		{
				if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5" || Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10") {
						Application.LoadLevel ("Lobby1");
				} else if (Application.loadedLevelName == "Level11" || Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
						Application.LoadLevel ("Lobby2");
				} else {
						Application.LoadLevel ("Lobby3");
				}
		}
}
