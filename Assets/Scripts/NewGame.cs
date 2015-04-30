using UnityEngine;
using System.Collections;

/*Inicia um novo jogo*/
public class NewGame : MonoBehaviour
{

	public Sprite[] backgorundSprite; //array com tres fundos
	public AudioClip[] bgMusicClips; //array com "tres" musicas
	public SpriteRenderer background; //renderer do cenario
	public Matrix matrix;	//matriz inicial
	public int totalUsableBubbles, bubblesLimitToNewRow; //bolhas totais, limite para descer a linha

	private AudioSource bgMusicSource; //audio source da musica de fundo
	private int rows = 1, columns = 20, distance = 1, variety = 1;	//linhas, colunas, distancia entre uma bolha e outra, variedade de bolhas
	private Vector2 position = new Vector2 (-9.5f, 4.5f);	//Posição inicial (esquerda/topo)
	private static int staticVariety; //hue

	void Awake ()
	{
		Application.targetFrameRate = 60;
		bgMusicSource = GetComponent<AudioSource> ();
		//Inicia a matriz de acordo com o level
		if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5") {
			variety = 3;
			rows = 5;
			totalUsableBubbles = 25;
			bubblesLimitToNewRow = 20;
		} else if (Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10" || Application.loadedLevelName == "Level11") {
			variety = 4;
			rows = 6;
			totalUsableBubbles = 25;
			bubblesLimitToNewRow = 15;
		} else if (Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
			variety = 5;
			rows = 7;
			totalUsableBubbles = 35;
			bubblesLimitToNewRow = 10;
		} else {
			variety = 5;
			rows = 8;
			totalUsableBubbles = 50;
			bubblesLimitToNewRow = 5;
		}
		//seta a variedade de bolhas do nivel
		setVariety ();

		//poe o fundo e a musica de acordo com o nivel
		if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5" || Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10") {
			bgMusicSource.clip = bgMusicClips [0];
			bgMusicSource.Play ();
			background.sprite = backgorundSprite [0];
		} else if (Application.loadedLevelName == "Level11" || Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
			bgMusicSource.clip = bgMusicClips [1];
			bgMusicSource.Play ();
			background.sprite = backgorundSprite [1];
		} else {
			bgMusicSource.clip = bgMusicClips [2];
			bgMusicSource.Play ();
			background.sprite = backgorundSprite [2];
		}

		//Inicia a matriz
		matrix = new Matrix (rows, columns, position, distance);

	}

	//seta variedade de bolhas
	public void setVariety ()
	{
		NewGame.staticVariety = variety;
	}

	//get estatico da variedade
	public static int getVariety ()
	{
		return NewGame.staticVariety;
	}
}
