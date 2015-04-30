using UnityEngine;
using System.Collections;

public class TryAgain : MonoBehaviour
{
		public Sprite hover;
		public Sprite NotHover;
		public AudioClip HoverSound;

		void OnMouseOver ()
		{
				gameObject.GetComponent<SpriteRenderer> ().sprite = hover;
		
		}
		void OnMouseEnter ()
		{
				GetComponent<AudioSource>().PlayOneShot (HoverSound);
		}
		void OnMouseExit ()
		{
				gameObject.GetComponent<SpriteRenderer> ().sprite = NotHover;
		}
		void OnMouseDown ()
		{
				StartCoroutine (ChangeLevel ());
		}

		IEnumerator ChangeLevel ()
		{
				float fadeTime = GameObject.Find ("Fade").GetComponent<Fading> ().BeginFade (1);
				yield return new WaitForSeconds (fadeTime);
				Application.LoadLevel (Application.loadedLevel);
		}
}
