using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	public static bool playing = true;
	public int turns = 0;
	public bool win, lose;
	public int totalPoints = 0;
	public int bubblesLeft = 0;
	public GameObject victoryScreen, loseScreen;

	private Shoot shoot;
	private Helper helper;
	private NewGame game;
	private int basePoints = 100;
	private int pointsModifier = 1;

	void Start ()
	{
		shoot = GetComponent<Shoot> ();
		helper = GetComponent<Helper> ();
		game = GetComponent<NewGame> ();
		bubblesLeft = game.totalUsableBubbles;
		playing = true;
	}

	//Controla a jogada depois de lançar a bola
	public void AfterATurn ()
	{
		if (playing) {
			if (!helper.hitBubbles) {
				turns++;
				Invoke ("CheckIfNewRow", 0.2f);
				pointsModifier = 1;
				bubblesLeft--;
			} else {
				Points (helper.shotBubbles, helper.extraBubbles);
				CheckIfWin ();
			}

			helper.shotBubbles = 0;
			helper.extraBubbles = 0;
			helper.hitBubbles = false;
			shoot.canShoot = true;
		}
	}

	//Verifica se é o turno certo para acrescentar uma nova linha
	void CheckIfNewRow ()
	{
		//Se jogada = jogada limite
		if (turns % game.bubblesLimitToNewRow == 0) {
			helper.NewRow ();
		}

		CheckIfLose ();
	}

	//Verifica se o jogador perdeu (linha chegou no limite embaixo e/ou gastou todas as bolhas)
	void CheckIfLose ()
	{
		lose = false;

		if (turns >= game.totalUsableBubbles) {
			lose = true;
			Debug.Log ("lose");
		}

		for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(1); i++) {
			try {
				if (game.matrix.bubbleMatrix [10, i] != null) {
					lose = true;
					Debug.Log ("lose");
				}
			} catch (System.IndexOutOfRangeException) {

			}
		}

		if (lose) {
			playing = false;
			loseScreen.SetActive (true);
		}
	}

	//Verifica se o jogador ganhou (destruiu todas as bolhas da tela)
	void CheckIfWin ()
	{
		win = true;

		for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(0); i++) {
			for (int j = 0; j < game.matrix.bubbleMatrix.GetLength(1); j++) {
				if (game.matrix.bubbleMatrix [i, j] != null) {
					win = false;
					break;
				}
			}
		}
	
		if (win) {
			Debug.Log ("win");
			playing = false;
			victoryScreen.SetActive (true);
		}
	}

	//Controla a pontuação
	void Points (int shotBubbles, int extraBubbles)
	{
		//Somente pontua se fez trio
		if (shotBubbles >= 3) {
			int turnPoints = 0; //Pontos do turno
			bool combo = false; //Se fez combo

			//Se acertou mais de 6, combo
			if (shotBubbles >= 6) {
				combo = true;
			}

			//A cada trio ganha 100pts
			while (shotBubbles - 3 >= 0) {
				turnPoints += basePoints;
				shotBubbles -= 3;
			}

			int shotBubblesLeft = shotBubbles; //Bolhas restantes dos trios

			//Acrescenta 25 pts por bolha restante
			turnPoints += shotBubblesLeft * 25;
			//Acrescenta 50 pts por bolha solta
			turnPoints += extraBubbles * 50;
			//Multiplica pelo combo
			turnPoints *= pointsModifier;
			//Adiciona ao total de pontos
			totalPoints += turnPoints;

			//Aumenta o modifier do combo
			if (combo) {
				pointsModifier *= 2;
			}
			combo = false;
		}
	}
}
