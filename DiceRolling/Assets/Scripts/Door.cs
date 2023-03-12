using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isOpen;

    public GameObject button;
    public Button buttonScript;

    public int openThreshold;
    public GameObject dice;
    private Dice diceScript;

    public TMP_Text thresholdDisplay;
    public Animator animator;

    public AudioSource audioSource;
    public SoundEffectData opensound;
    public SoundEffectData closesound;

    public List<int> doorStateInTime = new List<int>();

    public void Awake()
    {
        if(dice != null)
        {
            diceScript = dice.GetComponent<Dice>();
            thresholdDisplay.text = openThreshold.ToString();
        }

        if(button != null)
        {
            buttonScript = button.GetComponent<Button>();
        }
    }
    public void DoorCheck()
    {
        if(button != null)
        {
            if(buttonScript.isActivated)
            {
                DoorOpening();
            } else
            {
                DoorClosing();
            }
            return;
        }

        if (diceScript.value == openThreshold)
        {
            DoorOpening();
        } else
        {
            DoorClosing();
        }
        return;
    }

    public void DoorOpening()
    {
        if (!isOpen)
        {
            LevelTools.LoadSoundEffect(audioSource, opensound);
        }
        isOpen = true;
        gameObject.layer = LayerMask.NameToLayer("DoorOpen");
        animator.SetBool("IsOpen", true);
    }
    public void DoorClosing()
    {
        if (isOpen)
        {
            LevelTools.LoadSoundEffect(audioSource, opensound);
        }
        isOpen = false;
        gameObject.layer = LayerMask.NameToLayer("DoorClosed");
        animator.SetBool("IsOpen", false);
    }
}
