using UnityEngine;
using System.Collections;
using System;
public class HSController : MonoBehaviour{

    private string login; //fazer com getset //
    private string pass;
	private string nome; //  usar apenas com facebooklogin
    public string cont;
    
    public void Login(string log, string pas) {
    	
    	login = log;
    	pass = pas;

    	string url = "localhost/jca/logsenha.php?logando="+login+"&pass="+pass;
    	WWW www = new WWW(url);
    	StartCoroutine(WaitForRequest(www, 'n'));


    }
    
	public void LoginFacebook(string idfb, string nom){

		login = idfb;
		nome = nom;

		string url = "localhost/jca/logface.php?logando="+login+"&nome="+nome;
		WWW www = new WWW(url);

		StartCoroutine(WaitForRequest(www, 'f'));

	}


    IEnumerator WaitForRequest(WWW www, char call){


		yield return www;
     // check for errors
    	if (www.error == null && www.text != null){
            fbcon2controller.resultSeverBug = www.text;
        }
    }

    
}