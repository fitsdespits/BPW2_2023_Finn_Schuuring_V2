using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private SpriteRenderer spr;

    public bool collected;

    public List<int> collectedStateInTime = new List<int>();

    public void Start()
    {
        spr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && !collected)
        {
            MoneyDeactivate();
        }
    }

    public void MoneyDeactivate()
    {
        if (!collected)
        {
            Director.money += 1;
            spr.enabled = false;
            collected = true;
        }
    }

    public void MoneyActivate()
    {
        if (collected)
        {
            Director.money -= 1;
            spr.enabled = true;
            collected = false;
        }
    }
}
