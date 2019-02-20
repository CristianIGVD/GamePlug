using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {
    public float thicknessScale = 0.2f;
    public Vector3 targetPos;
    public Vector3 startingPos;
    public float targetLenghtScale;
    public float stretchingTime = 1.0f;
    float currentTime;
    bool hasHitSomething;


    public void Set(Vector3 pTargetPos, Vector3 pStartingPos, float pTargetLenghtScale)
    {
        targetPos = pTargetPos;
        startingPos = pStartingPos;
        targetLenghtScale = 30.0f;// pTargetLenghtScale;
        transform.position = startingPos;
        transform.localScale = Vector3.zero;
        hasHitSomething = false;
    }
    // Use this for initialization
    void Start()
    {
        currentTime = 0;
        Destroy(gameObject, 1.0f);

    }

    // Update is called once per frame
    void Update() {
        if (!hasHitSomething)
        {
            currentTime += Time.deltaTime;
            float ratio = currentTime / stretchingTime;

            transform.position = startingPos + ((targetPos - startingPos).normalized * 30.0f * 0.5f * ratio);

            if (ratio > 1)
            {
                ratio = 1;
            }
            else
            {
                transform.LookAt(targetPos);
            }

            Vector3 tempCopyOfWebLocalScale = transform.localScale;
            tempCopyOfWebLocalScale.x = 0.2f;
            tempCopyOfWebLocalScale.y = 0.2f;
            tempCopyOfWebLocalScale.z = targetLenghtScale * ratio;
            transform.localScale = tempCopyOfWebLocalScale;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            hasHitSomething = true;
        }
    }
}
