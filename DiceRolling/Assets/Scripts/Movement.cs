using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isMoving;
    private Vector3 originPos, targetPos;
    public float timeToMove;

    public bool isDice = false;
    public CameraShake camShake;

    public LayerMask mask;
    public LayerMask doorMask;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public AudioSource audioSource;
    public SoundEffectData walkSound;

    private Revert revertScript;
    public List<Vector3> positionInTime = new List<Vector3>();


    public void Awake()
    {
        if (!isDice)
        {
            revertScript = gameObject.GetComponent<Revert>();
        }

        animator = GetComponentInChildren<Animator>();

        camShake = FindObjectOfType<CameraShake>();
    }
    public void Move(float x, float y)
    {
        if (!isMoving && !Revert.startingWorld && !Revert.isReverting)
        {
            if (!isDice)
            {
                animator.SetBool("IsMoving", true);
            }

            LevelTools.LoadSoundEffect(audioSource, walkSound);

            if (x == 0 && y == 1)
            {
                StartCoroutine(MoveObject(Vector3.up));
            }
            else
            {
                if (x == 0 && y == -1)
                {
                    StartCoroutine(MoveObject(Vector3.down));
                }
                else
                {
                    if (x == -1 && y == 0)
                    {
                        StartCoroutine(MoveObject(Vector3.left));
                         spriteRenderer.flipX = true;
                    }
                    else
                    {
                        if (x == 1 && y == 0)
                        {
                            StartCoroutine(MoveObject(Vector3.right));
                            spriteRenderer.flipX = false;
                        }
                    }
                }
            }
        }
    }

    public IEnumerator MoveObject(Vector3 direction)
    {
        isMoving = true;

        if(CheckForCollision(direction) == false)
        {
            if (isDice)
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("X", direction.x);
                animator.SetFloat("Y", direction.y);
            }

            //Moving
            if (Grab.isGrabbing)
            {
                if (!isDice)
                {
                    Grab.currentDice.GetComponent<Movement>().Move(direction.x, direction.y);
                }

                if (isDice)
                {
                    gameObject.GetComponent<Dice>().RollDice(direction);
                }
            }
            float elapsedTime = 0;

            originPos = transform.position;
            targetPos = originPos + direction;

            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
            
            animator.SetBool("IsMoving", false);

            if (isDice)
            {
                camShake.StartCoroutine(camShake.Shake(.08f,.09f));
            }

            //Player movement saves the worldstate.
            if (!isDice)
            {
                revertScript.SaveWorld();
            }
        } else
        {
            //Collision
            float elapsedTime = 0;

            originPos = transform.position;
            targetPos = originPos + direction / 5;

            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;

            Vector3 originPos2 = transform.position;
            targetPos = originPos;
            
            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(originPos2, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            if (!isDice)
            {
                animator.SetBool("IsMoving", false);
            }
        }

        if (!isDice)
        {
            gameObject.GetComponent<Grab>().UpdateLastDirection(direction);
        }

        yield return new WaitForSecondsRealtime(0.03f);

        isMoving = false;
    }

    public bool CheckForCollision(Vector3 dir)
    {
        //Player Check
        if (!isDice)
        {
            //Checking collision from players hitray.
            Debug.DrawRay(transform.position, dir, Color.blue, 2f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, mask);

            if (hit)
            {
                return true;
            }
            else
            {
                if (Grab.isGrabbing)
                {
                    if (!IsPlayerSafe(dir))
                    {
                        return true;
                    }
                    
                    hit = Physics2D.Raycast(Grab.currentDice.transform.position, dir, 1f, Grab.currentDice.GetComponent<Movement>().mask);
                    if (hit)
                    {
                        return true;
                    }
                    else
                    {
                        if (IsDoorLinked(dir))
                        {
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        } else
        {
            //Dice Check
            return false;
        }
    }

    public bool IsDoorLinked(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(Grab.currentDice.transform.position, dir, 1f, doorMask);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<DoorColour>().diceLink == Grab.currentDice.GetComponent<Dice>().diceID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsPlayerSafe(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir * 0.8f, dir, 0.5f, doorMask);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<DoorColour>().diceLink == Grab.currentDice.GetComponent<Dice>().diceID)
            {
                return false;
            }
            else
            {
                return true;
            }
        } else
        {
            return true;
        }
    }
}
