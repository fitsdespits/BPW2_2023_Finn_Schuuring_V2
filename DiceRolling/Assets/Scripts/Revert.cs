using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revert : MonoBehaviour
{
    public static bool isReverting;
    public static bool startingWorld;
    
    public GameObject cam;
    public CameraMovement camMove;

    private Movement[] moveList;
    private Grab[] grabList;
    private Dice[] diceList;
    private Door[] doorList;
    private Money[] moneyList;

    public static int turn = 0;

    public void Awake()
    {
        moveList = FindObjectsOfType<Movement>();
        grabList = FindObjectsOfType<Grab>();
        diceList = FindObjectsOfType<Dice>();
        doorList = FindObjectsOfType<Door>();
        moneyList = FindObjectsOfType<Money>();

        camMove = FindObjectOfType<CameraMovement>();

        StartCoroutine(StartWorld());
    }
    public void RevertWorld()
    {
        if (!isReverting)
        {
            isReverting = true;

            if (turn >= 2)
            {
                turn -= 1;

                camMove.StartTwist();

                //Movement Reverting
                foreach (Movement moveScript in moveList)
                {
                    Vector3 p = moveScript.positionInTime[moveScript.positionInTime.Count - 2];
                    moveScript.gameObject.transform.position = p;
                    moveScript.positionInTime.RemoveAt(moveScript.positionInTime.Count - 1);
                }

                //Grab Reverting
                foreach (Grab grabScript in grabList)
                {
                    if (grabScript.grabStateInTime[grabScript.grabStateInTime.Count - 2] == 1)
                    {
                        if (grabScript.grabStateInTime.Count - 1 != grabScript.grabStateInTime.Count - 2)
                        {
                            grabScript.GrabActivate();
                        }
                    }
                    else
                    {
                        if (grabScript.grabStateInTime.Count - 1 != grabScript.grabStateInTime.Count - 2)
                        {
                            grabScript.GrabDeactivate();
                        }
                    }

                    grabScript.grabStateInTime.RemoveAt(grabScript.grabStateInTime.Count - 1);
                }

                //Dice Roll Reverting
                foreach (Dice diceScript in diceList)
                {
                    Data d = diceScript.diceData[diceScript.diceData.Count - 2];
                    diceScript.value = d.dataValue;
                    diceScript.up = d.dataUp;
                    diceScript.down = d.dataDown;
                    diceScript.left = d.dataLeft;
                    diceScript.right = d.dataRight;

                    diceScript.TextUpdate();

                    diceScript.diceData.RemoveAt(diceScript.diceData.Count - 1);


                }

                //Door Reverting
                foreach (Door doorScript in doorList)
                {
                    if (doorScript.doorStateInTime[doorScript.doorStateInTime.Count - 2] == 1)
                    {
                        if (doorScript.doorStateInTime.Count - 1 != doorScript.doorStateInTime.Count - 2)
                        {
                            doorScript.DoorOpening();
                        }
                    }
                    else
                    {
                        if (doorScript.doorStateInTime.Count - 1 != doorScript.doorStateInTime.Count - 2)
                        {
                            doorScript.DoorClosing();
                        }
                    }

                    doorScript.doorStateInTime.RemoveAt(doorScript.doorStateInTime.Count - 1);
                }

                //Money Reverting
                foreach (Money moneyScript in moneyList)
                {
                    if (moneyScript.collectedStateInTime[moneyScript.collectedStateInTime.Count - 2] == 1)
                    {
                        if (moneyScript.collectedStateInTime.Count - 1 != moneyScript.collectedStateInTime.Count - 2)
                        {
                            moneyScript.MoneyActivate();
                        }
                    }
                    else
                    {
                        if (moneyScript.collectedStateInTime.Count - 1 != moneyScript.collectedStateInTime.Count - 2)
                        {
                            moneyScript.MoneyDeactivate();
                        }
                    }

                    moneyScript.collectedStateInTime.RemoveAt(moneyScript.collectedStateInTime.Count - 1);
                }
            }
            isReverting = false;
        }
    }

    public void SaveWorld()
    {
        turn += 1;

        //Movement Saving
        foreach (Movement moveScript in moveList)
        {
            moveScript.positionInTime.Add(moveScript.gameObject.transform.position);
        }

        //Grab Saving
        foreach (Grab grabScript in grabList)
        {
            if (Grab.isGrabbing)
            {
                grabScript.grabStateInTime.Add(1);
            } else
            {
                grabScript.grabStateInTime.Add(0);
            }
        }

        //Dice Roll Saving
        foreach (Dice diceScript in diceList)
        {
            diceScript.diceData.Add(new Data(diceScript.value,diceScript.up,diceScript.down,diceScript.left,diceScript.right));
        }

        //Door Saving
        foreach (Door doorScript in doorList)
        {
            doorScript.DoorCheck();
            if (doorScript.isOpen)
            {
                doorScript.doorStateInTime.Add(1);
            }
            else
            {
                doorScript.doorStateInTime.Add(0);
            }
        }

        //Money Saving
        foreach (Money moneyScript in moneyList)
        {
            if (moneyScript.collected)
            {
                moneyScript.collectedStateInTime.Add(0);
            } else
            {
                moneyScript.collectedStateInTime.Add(1);
            }
        }
    }

    public IEnumerator StartWorld()
    {
        startingWorld = true;

        yield return new WaitForSecondsRealtime(0.1f);

        SaveWorld();

        startingWorld = false;
    }

    public void WorldReset()
    {
        turn = 0;

        //Movement
        foreach (Movement moveScript in moveList)
        {
            moveScript.positionInTime.Clear();
        }

        //Grab
        foreach (Grab grabScript in grabList)
        {
            grabScript.grabStateInTime.Clear();
        }

        //DiceRoll
        foreach (Dice diceScript in diceList)
        {
            diceScript.diceData.Clear();
        }

        //Door
        foreach (Door doorScript in doorList)
        {
            doorScript.doorStateInTime.Clear();
        }

        //Money
        foreach (Money moneyScript in moneyList)
        {
            moneyScript.collectedStateInTime.Clear();
        }
        
        SaveWorld();
    }
}
