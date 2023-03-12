using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public int tutorialPhase;
    public float textDelayTimer;
    public float textDelaySeconds;

    public TMP_Text text;

    public bool eventTriggered = false;
    public bool pause;

    public void Awake()
    {
        tutorialPhase = 0;
        text.text = "Use WASD to move your character.";
        text.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (pause)
        {
            if(textDelaySeconds < textDelayTimer)
            {
                textDelaySeconds += Time.deltaTime;
            } else
            {
                pause = false;
            }
            return;
        }
        
        switch (tutorialPhase)
        {
            case 0:
                //WASD
                if(eventTriggered)
                {
                    textDelaySeconds = 0;
                    eventTriggered = false;
                    pause = true;
                    tutorialPhase += 1;
                    text.text = "Use SPACE to grab dice.";
                }
                break;

            case 1:
                if (eventTriggered)
                {
                    textDelaySeconds = 0;
                    eventTriggered = false;
                    pause = true;
                    tutorialPhase += 1;
                    text.text = "Open doors by matching dice and door values.";
                }
                break;

            case 2:
                textDelaySeconds = 0;
                pause = true;
                tutorialPhase += 1;
                text.text = "Use Z to revert actions when stuck.";
                break;

            case 3:
                if (eventTriggered)
                {
                    textDelaySeconds = 0;
                    eventTriggered = false;
                    pause = true;
                    tutorialPhase += 1;
                    text.text = "Jump down the hole to proceed.";
                }
                break;

            case 4:
                if (eventTriggered)
                {
                    textDelaySeconds = 0;
                    eventTriggered = false;
                    pause = true;
                    tutorialPhase += 1;
                    text.gameObject.SetActive(false);
                }
                break;
        }
    }
}
