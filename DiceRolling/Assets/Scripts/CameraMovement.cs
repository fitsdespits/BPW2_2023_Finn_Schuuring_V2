using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform cam;
    public bool twisting;

    public void Awake()
    {
        cam = gameObject.transform;
    }

    public void StartTwist()
    {
        if (!twisting)
        {
            StartCoroutine(RevertTwist());
        }
    }

    public IEnumerator RevertTwist()
    {
        twisting = true;
        Quaternion orig = cam.transform.rotation;
        Quaternion twist = Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y, cam.transform.rotation.z - 2);

        float twistTime = .05f;
        float elapsedTime = 0;
        while (elapsedTime < twistTime)
        {
            transform.rotation = Quaternion.Lerp(orig, twist, (elapsedTime / twistTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.transform.rotation = twist;

        elapsedTime = 0;
        while (elapsedTime < twistTime)
        {
            transform.rotation = Quaternion.Lerp(twist, orig, (elapsedTime / twistTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.transform.rotation = orig;

        yield return new WaitForSecondsRealtime(0.01f);

        twisting = false;
    }
}
