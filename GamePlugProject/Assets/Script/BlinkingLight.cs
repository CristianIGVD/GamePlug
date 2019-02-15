using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    Light blinkLight;
    public float minWaitTime;
    public float MaxWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        blinkLight = GetComponent<Light>();
        StartCoroutine(flashing());
    }

    IEnumerator flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime,MaxWaitTime));
            blinkLight.enabled = !blinkLight.enabled;
        }
    }
}
