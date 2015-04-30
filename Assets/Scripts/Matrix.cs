using UnityEngine;
using System.Collections;

/*Constroi a matriz inicial do level*/
public class Matrix
{

    public static bool isFirstRowFull = true;
    public Bubbles[,] bubbleMatrix;	//array com as bolhas do jogo

    //construtor
    public Matrix(int rows, int columns, Vector2 initialPosition, int distance)
    {
        bool fullRow = true;	//se a linha tem o maximo de colunas
        Vector2 newPosition = initialPosition;	//nova posição de acordo com a bolha anterior
        int newColumns = columns;	//quantidade de colunas

        bubbleMatrix = new Bubbles[11, columns];	//inicializador da array

        //constroi a matriz inicial
        for (int i = 0; i < rows; i++)
        {
            if (fullRow)
            {
                newPosition.x = initialPosition.x;
                newColumns = columns;
            }
            else
            {
                newPosition.x = initialPosition.x + 0.5f;
                newColumns = columns - 1;
            }
            for (int j = 0; j < newColumns; j++)
            {
                bubbleMatrix[i, j] = new Bubbles(newPosition.x, newPosition.y);
                newPosition.x += distance;
            }
            newPosition.y -= distance;
            fullRow = !fullRow;
        }
        //debug
        /*for (int i = 0; i < rows; i ++) {
                for (int j = 0; j < columns; j++) {
                        try {
                                Debug.Log (bubbleMatrix [i, j].getColor ());
                        } catch (System.NullReferenceException) {
                        }
                }
        }*/
    }
}
