using UnityEngine;
using System.Collections;

public class PointsParticles : MonoBehaviour {
	private GameController gc;
	public GameObject[] pointsParticles;

	void Awake(){
		gc = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		SwitchParticles();
	}

	void SwitchParticles(){
		if (gc.totalPoints >= 1000 && gc.totalPoints < 5000){
			for (int i = 0; i < 12; i++){
				if (i != 0){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[0].activeSelf == false){
				pointsParticles[0].SetActive(true);
				}
			} else if (gc.totalPoints >= 5000 && gc.totalPoints < 10000){
				for (int i = 0; i < 12; i++){
				if (i != 1){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[1].activeSelf == false){
				pointsParticles[1].SetActive(true);
			}
			} else if (gc.totalPoints >= 10000 && gc.totalPoints < 15000){
				for (int i = 0; i < 12; i++){
				if (i != 2){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[2].activeSelf == false){
				pointsParticles[2].SetActive(true);
			}
			} else if (gc.totalPoints >= 15000 && gc.totalPoints < 20000){
				for (int i = 0; i < 12; i++){
				if (i != 3){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[3].activeSelf == false){
				pointsParticles[3].SetActive(true);
			}
			} else if (gc.totalPoints >= 20000 && gc.totalPoints < 30000){
				for (int i = 0; i < 12; i++){
				if (i != 4){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[4].activeSelf == false){
				pointsParticles[4].SetActive(true);
			}
			} else if (gc.totalPoints >= 30000 && gc.totalPoints < 50000){
				for (int i = 0; i < 12; i++){
				if (i != 5){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[5].activeSelf == false){
				pointsParticles[5].SetActive(true);
			}
			} else if (gc.totalPoints >= 50000 && gc.totalPoints < 75000){
				for (int i = 0; i < 12; i++){
				if (i != 6){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[6].activeSelf == false){
				pointsParticles[6].SetActive(true);
			}
			} else if (gc.totalPoints >= 75000 && gc.totalPoints < 100000){
				for (int i = 0; i < 12; i++){
				if (i != 7){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[7].activeSelf == false){
				pointsParticles[7].SetActive(true);
			}
			} else if (gc.totalPoints >= 100000 && gc.totalPoints < 200000){
				for (int i = 0; i < 12; i++){
				if (i != 8){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[8].activeSelf == false){
				pointsParticles[8].SetActive(true);
			}
			} else if (gc.totalPoints >= 200000 && gc.totalPoints < 600000){
				for (int i = 0; i < 12; i++){
				if (i != 9){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[9].activeSelf == false){
				pointsParticles[9].SetActive(true);
			}
			} else if (gc.totalPoints >= 600000 && gc.totalPoints < 1000000){
				for (int i = 0; i < 12; i++){
				if (i != 10){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[10].activeSelf == false){
				pointsParticles[10].SetActive(true);
			}
			} else if (gc.totalPoints >= 1000000){
				for (int i = 0; i < 12; i++){
				if (i != 11){
					pointsParticles[i].SetActive(false);
				}
			}
			if (pointsParticles[11].activeSelf == false){
				pointsParticles[11].SetActive(true);
			}
			}
	}
}
