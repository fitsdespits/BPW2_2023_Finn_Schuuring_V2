using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DoorColour : MonoBehaviour
{
    public int diceLink;
    public void Update()
    {
        if(gameObject.GetComponent<Door>().dice != null)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = gameObject.GetComponent<Door>().dice.GetComponentInChildren<SpriteRenderer>().color;
            diceLink = gameObject.GetComponent<Door>().dice.GetComponentInChildren<Dice>().diceID;
        }

        if (gameObject.GetComponent<Door>().button != null)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = gameObject.GetComponent<Door>().button.GetComponentInChildren<SpriteRenderer>().color;
        }
    }
}
