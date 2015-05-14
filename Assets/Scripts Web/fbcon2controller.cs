using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using System;

public class fbcon2controller : MonoBehaviour {

	public string get_data;
	public string fbname;


	public UserController userController;
	public HSController controller;

	public static string resultSeverBug;
	private bool callServerEnded;

	//minatto
	public GameObject psError;


	void Update(){
		if (resultSeverBug != "" && callServerEnded == true){
			takeToUserController(resultSeverBug);
			Application.LoadLevel("Lobby1");

		} 

	}

	// Use this for initialization
	void Start(){
		CallFBInit();
		callServerEnded = false;
	}

	void OnMouseDown(){
		CallFBLogin();
	}

	private void CallFBInit()
		
	{
		FB.Init(OnInitComplete, OnHideUnity);
		
	}
	
	private void OnInitComplete()
		
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
		
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	private void CallFBLogin()
		
	{
		FB.Login("email", LoginCallback);
	}


	private void LoginCallback(FBResult result)
	{
		if (result.Error != null){
			print("Logged In");
		}else if (!FB.IsLoggedIn) {
			print("Failed");
		}else {
			print("baka");
			FB.API("/me?fields=first_name", Facebook.HttpMethod.GET, LoginCallback2);

		}

	}

	// logar
	void LoginCallback2(FBResult result)
	{
		if (result.Error != null)
			print("Error Response:\n" + result.Error);
		else if (!FB.IsLoggedIn)
			print("Login cancelled by Player");
		else{
			IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.Text) as IDictionary;
			fbname = dict["first_name"].ToString();

			print("your name is: " + fbname);


			//pega a foto 50x50 no usuario 
			StartCoroutine(GetTexture());
			controller.LoginFacebook(FB.UserId, fbname);

		}
	}


	public void takeToUserController(string resultSeverBug){
		if(resultSeverBug != ""){

			string[] resultado = resultSeverBug.Split(new char[] {';'});
			//passa os dados do usuario para o script da cena
			userController.userName = resultado[0];
			if(resultado[1] != ""){
				userController.levelPlayer = int.Parse(resultado[1]);
			}
			if(resultado[2] != ""){
				userController.points = int.Parse(resultado[2]);
		
			}
			userController.stars = resultado[3];
			userController.playersConf = resultado[4];
			if(resultado[5] != ""){
				userController.life = int.Parse (resultado[5]);
			}
			if(resultado[6] != ""){
				print(resultado[6]);
				userController.bestpoints = int.Parse(resultado[6]);
			}

			Entrar.callServerEnded = true;
		}else {

			StartCoroutine ("PSError");
		}
	}
	//funcao minatto errro de senha
	IEnumerator PSError(){
		psError.SetActive(true);
		yield return new WaitForSeconds (3f);
		psError.SetActive(false);
	}




	//pegar a foto do individuo
	IEnumerator GetTexture () 
	{
		// Start a download of the given URL
		// Notice that we are including the facebook username into the URL
		WWW url = new WWW ("https://graph.facebook.com/" + FB.UserId + "/picture");
		userController.login = FB.UserId;
		
		// Print a message letting us know that the process of retrieving the 
		// profile picture has started
		print("Retrieving Facebook Texture");
		
		// Wait for download to complete
		yield return url;
		
		// Print a message letting us know that the profile picture has been retrieved
		print("Facebook Texture Retrieved");

		userController.userPictureSprite = Sprite.Create(url.texture,new Rect(0, 0, 50, 50),new Vector2(50,50));

		// assign the texture to the GameObject so we can see it
		 userController.userPicture = url.texture; 


		//go to Lobby1
		callServerEnded = true;
		//Application.LoadLevel("Lobby1");
	}

}// fim Fbcon2Controller

