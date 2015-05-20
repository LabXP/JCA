using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour
{

	public static UserController instance = null;

	public string login;
	public string userName;
	public Texture2D userPicture;
	public Sprite userPictureSprite;
	public int levelPlayer;
	public int points;
	public string stars;
	public string playersConf;
	public int life = 3;
	public int bestpoints;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}


}