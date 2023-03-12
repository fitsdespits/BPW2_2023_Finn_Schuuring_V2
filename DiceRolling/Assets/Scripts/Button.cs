using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject dice;
    public bool isActivated = false;
    public Sprite[] images;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dice" && collision.gameObject == dice)
        {
            isActivated = true;
            spriteRenderer.sprite = images[0];
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dice" && collision.gameObject == dice)
        {
            isActivated = false;
            spriteRenderer.sprite = images[1];
        }
    }
}
