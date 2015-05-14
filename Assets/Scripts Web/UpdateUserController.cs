using UnityEngine;
using System.Collections;

public class UpdateUserController : MonoBehaviour {
	

	public UserController controller;

	void Awake (){
		controller = FindObjectOfType<UserController>();
	}



	public void UpdateDatabase() {

		if(controller.points>controller.bestpoints)
			controller.bestpoints = controller.points;

		if(controller.life<0){
			controller.points = 0;
			controller.life = 3;
		}

		print("localhost/jca/update.php?login="+controller.login+"&senhaservidor=batata&nome="+controller.name+
		      "&niveldojogador="+controller.levelPlayer+"&pontuacao="+controller.points+"&estrelas="+controller.stars+
		      "&confdojogador="+controller.playersConf+"&vida="+controller.life+"&melhorpontuacao="+controller.bestpoints);



		string url = "localhost/jca/update.php?login="+controller.login+"&senhaservidor=batata&nome="+controller.name+
						"&niveldojogador="+controller.levelPlayer+"&pontuacao="+controller.points+"&estrelas="+controller.stars+
							"&confdojogador="+controller.playersConf+"&vida="+controller.life+"&melhorpontuacao="+controller.bestpoints;
		//upando
		WWW www = new WWW(url);

		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www){
		
		
		yield return www;
		// check for errors
		if (www.error == null && www.text != null){
				print(www.text + " resposta update" );
		}
	}
}
