using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerController controls;
    public GameObject player;

    Movement moveScript;
    Grab grabScript;
    Revert revertScript;
    Tutorial tutorialScript;

    Vector2 move;

    //Input Buffering
    public List<Vector2> moveBuffer = new List<Vector2>();
    public int grabBuffer = 0;

    void Awake()
    {
        //Getting player scripts
        moveScript = player.GetComponent<Movement>();
        grabScript = player.GetComponent<Grab>();
        revertScript = player.GetComponent<Revert>();
        tutorialScript= player.GetComponent<Tutorial>();

        //Controller initialisation
        controls = new PlayerController();

        //Movement
        controls.Controls.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Controls.Move.performed += ctx => Move();
        controls.Controls.Move.canceled += ctx => move = Vector2.zero;

        //Grab
        controls.Controls.Grab.performed += ctx => Grab();

        //Revert
        controls.Controls.Revert.performed += ctx => Revert();
    }

    private void Move()
    {
        if(moveBuffer.Count < 2)
        {
            moveBuffer.Add(move);
        }
    }

    private void Grab()
    {
        grabBuffer += 1;
    }

    private void Revert()
    {
        if (tutorialScript.tutorialPhase == 3)
        {
            tutorialScript.eventTriggered = true;
        }
        revertScript.RevertWorld();
    }

    public void Update()
    {
        //Grab Buffer
        if(grabBuffer > 3)
        {
            grabBuffer = 3;
        }
        if(grabBuffer > 0 && !moveScript.isMoving)
        {
            if (tutorialScript.tutorialPhase == 1)
            {
                tutorialScript.eventTriggered = true;
            }
            grabScript.GrabObject();
            grabBuffer -= 1;
        }

        //Movement Buffer
        if (moveBuffer.Count > 0 && !moveScript.isMoving)
        {
            if(tutorialScript.tutorialPhase == 0)
            {
                tutorialScript.eventTriggered = true;
            }
            moveScript.Move(Mathf.Round(moveBuffer[0].x), Mathf.Round(moveBuffer[0].y));
            moveBuffer.RemoveAt(0);
        }
    }

    private void OnEnable()
    {
        controls.Controls.Enable();
    }

    private void OnDisable()
    {
        controls.Controls.Disable();
    }
}