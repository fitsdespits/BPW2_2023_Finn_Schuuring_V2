using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public static bool isGrabbing;
    private Vector3 lastDirection = Vector3.up;
    public static GameObject currentDice;
    
    public LayerMask mask;

    public List<int> grabStateInTime = new List<int>();

    public AudioSource audioSource;
    public SoundEffectData grabsound;

    public void GrabObject()
    {
        if (!Revert.startingWorld && !Revert.isReverting)
        {
            if (!isGrabbing)
            {
                GrabActivate();
            }
            else
            {
                GrabDeactivate();
            }
        }
    }

    public bool CheckForGrab()
    {
        //Checking around the player for dice.

        //Previous Direction
        Debug.DrawRay(transform.position, lastDirection, Color.magenta, 2f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, 1f, mask);
        if (hit)
        {
            currentDice = hit.collider.gameObject;
            return true;
        } else
        {
            //Up
            Debug.DrawRay(transform.position, Vector3.up, Color.green, 1f);
            hit = Physics2D.Raycast(transform.position, Vector3.up, 1f, mask);
            if (hit)
            {
                currentDice = hit.collider.gameObject;
                return true;
            }
            else
            {
                //Right
                Debug.DrawRay(transform.position, Vector3.right, Color.green, 1f);
                hit = Physics2D.Raycast(transform.position, Vector3.right, 1f, mask);
                if (hit)
                {
                    currentDice = hit.collider.gameObject;
                    return true;
                }
                else
                {
                    //Down
                    Debug.DrawRay(transform.position, Vector3.down, Color.green, 1f);
                    hit = Physics2D.Raycast(transform.position, Vector3.down, 1f, mask);
                    if (hit)
                    {
                        currentDice = hit.collider.gameObject;
                        return true;
                    }
                    else
                    {
                        //Left
                        Debug.DrawRay(transform.position, Vector3.left, Color.green, 1f);
                        hit = Physics2D.Raycast(transform.position, Vector3.left, 1f, mask);
                        if (hit)
                        {
                            currentDice = hit.collider.gameObject;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }

    public void UpdateLastDirection(Vector3 direction)
    {
        lastDirection = direction;
    }

    public void GrabActivate()
    {
        if (CheckForGrab())
        {
            currentDice.GetComponent<Dice>().PreviewsOn();
            currentDice.layer = LayerMask.NameToLayer("GrabbedDice");
            isGrabbing = true;
            LevelTools.LoadSoundEffect(audioSource, grabsound);
        }
    }

    public void GrabDeactivate()
    {
        if(currentDice != null)
        {
            currentDice.GetComponent<Dice>().PreviewsOff();
            currentDice.layer = LayerMask.NameToLayer("UngrabbedDice");
            currentDice = null;
            LevelTools.LoadSoundEffect(audioSource, grabsound);
        }
        isGrabbing = false;
    }
}
