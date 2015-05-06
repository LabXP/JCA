using UnityEngine;
using System.Collections;
using System;

/*Classe do objeto bolha*/
public class Bubbles
{
    //cores de bolhas
    public enum BubbleColor
    {
        Blue,
        Red,
        Yellow,
        Green,
        Purple
    }
		;
    public GameObject bubbleObject;
    public BubblesController bubbleObjectController;
    private BubbleColor bubbleColor;	//cor da bolha

    //escolhe uma cor aleatoria de acordo com a variedade ou com a cor disponivel
    public BubbleColor setColor(int variety, string color)
    {
        if (color == null)
        {
            Array A = Enum.GetValues(typeof(BubbleColor));
            BubbleColor V = (BubbleColor)A.GetValue(UnityEngine.Random.Range(0, variety));
            return bubbleColor = V;
        }
        else
        {
            return bubbleColor = (BubbleColor)Enum.Parse(typeof(BubbleColor), color);
        }
    }
    //retorna a cor da bolha
    public string getColor()
    {
        return bubbleColor.ToString();
    }

    //construtor
    public Bubbles(float positionX, float positionY, string color = null)
    {
        int variety = NewGame.getVariety();
        setColor(variety, color);

        bubbleObject = MonoBehaviour.Instantiate(Resources.Load(getColor())) as GameObject;
        Vector2 position = new Vector2(positionX, positionY);
        bubbleObject.transform.position = position;

        bubbleObjectController = bubbleObject.GetComponent<BubblesController>();
    }
}
