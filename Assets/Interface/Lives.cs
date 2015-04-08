using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {
	public int LivesCount;
	public GameObject[] HealthContainers;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (LivesCount){
			case 0:
			for (int i = LivesCount; i < 5; i++){
			HealthContainers[i].SetActive(false);
			}
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;
			case 1:
			for (int i = LivesCount; i < 5; i++){
			HealthContainers[i].SetActive(false);
			}
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;
			case 2:
			for (int i = LivesCount; i < 5; i++){
			HealthContainers[i].SetActive(false);
			}
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;
			case 3:
			for (int i = LivesCount; i < 5; i++){
			HealthContainers[i].SetActive(false);
			}
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;
			case 4:
			for (int i = LivesCount; i < 5; i++){
			HealthContainers[i].SetActive(false);
			}
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;
			case 5:
			for (int i = 0; i < LivesCount; i++){
				HealthContainers[i].SetActive(true);
			}
			break;

		}
}
}
