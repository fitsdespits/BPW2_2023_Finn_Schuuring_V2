using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logo : MonoBehaviour
{
    public float freq;
    public float amp;

    public void Update()
    {
        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);
    }
}
