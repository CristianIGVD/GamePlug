using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

    public float speed = 10;

    public float radius = -60;

    public GameObject spider;

    public float webShootTravelTime = 2f;
    public float webShootRotation = 2f;
    bool canWebShoot = true;

    Vector3 controllerAxis = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canWebShoot)
        {
            if (Input.GetAxis("TargetHorizontal") != 0)
            {
                transform.Rotate(Vector3.forward * Input.GetAxis("TargetHorizontal") * Time.deltaTime * speed);
            }

            //spider.transform.localPosition = new Vector3(spider.transform.localPosition.x, radius, spider.transform.localPosition.z);
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine("WebShoot");
            }
        }
    }
    
    IEnumerator WebShoot()
    {
        Vector3 initialPosition = spider.transform.localPosition;
        radius *= -1;
        Vector3 targetLocalPosition = new Vector3(spider.transform.localPosition.x, radius, spider.transform.localPosition.z);

        Quaternion initialRotation = spider.transform.localRotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(180, 0, 0);


        canWebShoot = false;

        float currentTime = 0;
        while(currentTime < webShootTravelTime)
        {
            currentTime += Time.deltaTime;
            spider.transform.localPosition = Vector3.Lerp(initialPosition, targetLocalPosition, currentTime / webShootTravelTime);
            spider.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, currentTime / webShootRotation);
            yield return new WaitForEndOfFrame();
        }
        spider.transform.localPosition = targetLocalPosition;
        spider.transform.localRotation = targetRotation;

        canWebShoot = true;
        yield return null;
    }
}
