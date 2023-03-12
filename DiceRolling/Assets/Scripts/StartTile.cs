using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : MonoBehaviour
{
    private bool init;
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && !init)
        {
            init = true;
            Director.money = 0;
        }
    }
}
