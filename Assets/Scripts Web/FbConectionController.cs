using UnityEngine;
using System.Collections;
using Facebook;
public class FbConectionController : MonoBehaviour {

	public string lastResponse = "";
	// Use this for initialization
	void Start () {


	}


	// Update is called once per frame
	void Update () {
	
	}



	void OnMouseDown(){
	
		CallFBLogin();
	}



		#region FB.Login() example
	
	private void CallFBLogin()
	{
		FB.Login("public_profile,email,user_friends", LoginCallback);
	}
	
	private void CallFBLoginForPublish()
	{
		// It is generally good behavior to split asking for read and publish
		// permissions rather than ask for them all at once.
		//
		// In your own game, consider postponing this call until the moment
		// you actually need it.
		FB.Login("publish_actions", LoginCallback);
	}
	
	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
			lastResponse = "Error Response:\n" + result.Error;
		else if (!FB.IsLoggedIn)
		{
			lastResponse = "Login cancelled by Player";
		}
		else
		{
			lastResponse = "Login was successful!";
		}
	}
	
	private void CallFBLogout()
	{
		FB.Logout();
	}
	#endregion
}
