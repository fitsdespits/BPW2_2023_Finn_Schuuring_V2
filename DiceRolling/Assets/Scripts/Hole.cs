using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    public Tutorial tutorialScript;
    public Transform transportPoint;
    public bool endGoal;
    public GameObject demotext;

    private CameraFollow camScript;

    public GameObject blackScreen;
    public GameObject circleMask;
    public AnimationCurve fadeCurve;
    public float fadeSeconds;
    public bool fadeIn;
    public bool fadeOut;

    public AudioSource audioSource;
    public SoundEffectData victorySound;

    private void Start()
    {
        tutorialScript = FindObjectOfType<Tutorial>();
        camScript = FindObjectOfType<CameraFollow>();

        blackScreen.SetActive(true);
        circleMask.SetActive(true);
        circleMask.transform.localScale = new Vector3(0, 0, 1);
        fadeIn = true;
    }

    public void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        if (fadeOut)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        circleMask.transform.position = tutorialScript.transform.position;
        if (fadeSeconds < fadeCurve[fadeCurve.length - 1].time)
        {
            fadeSeconds += Time.deltaTime;
            circleMask.transform.localScale = new Vector3(fadeCurve.Evaluate(fadeSeconds), fadeCurve.Evaluate(fadeSeconds), 1);
        }
        else
        {
            fadeIn = false;
            blackScreen.SetActive(false);
            circleMask.SetActive(false);
        }
    }

    public void FadeOut()
    {
        circleMask.transform.position = tutorialScript.transform.position;
        if (fadeSeconds > 0)
        {
            fadeSeconds -= Time.deltaTime;
            circleMask.transform.localScale = new Vector3(fadeCurve.Evaluate(fadeSeconds), fadeCurve.Evaluate(fadeSeconds), 1);
        }
        else
        {
            fadeOut = false;
            circleMask.transform.localScale = new Vector3(0, 0, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            if(tutorialScript.tutorialPhase == 4) 
            {
                tutorialScript.eventTriggered= true;
            }   
            StartCoroutine(TransportPlayer(collision.gameObject));
        }
    }

    public IEnumerator TransportPlayer(GameObject player)
    {
        blackScreen.SetActive(true);
        circleMask.SetActive(true);
        fadeOut = true;
        yield return new WaitForSecondsRealtime(.5f);

        LevelTools.LoadSoundEffect(audioSource, victorySound);

        yield return new WaitForSecondsRealtime(1.7f);

        if (!endGoal)
        {
            player.transform.position = transportPoint.position;
            camScript.gameObject.transform.position = player.transform.position;
            Revert r = FindObjectOfType<Revert>();
            r.WorldReset();

            fadeIn = true;
        }
        else
        {
            Debug.Log("Level Complete");
            yield return new WaitForSecondsRealtime(1f);
            demotext.SetActive(true);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
