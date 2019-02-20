using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{
    GameObject grabbedObject;
    float grabbedObjectSize;

    GameObject GetMouseHoverObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward * range;
        if (Physics.Linecast(position, target, out raycastHit))
            return raycastHit.collider.gameObject;
        return null;
    }
   
    void TryGrabObject (GameObject grabObject)
    {
        if (grabbedObject == null)
            return;

        grabbedObject = grabObject;
        grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;
    }

   /* void DropObject()
    {
        if (grabbedObject == null)
            return;
        grabbedObject == null

    }*/

   /* void Update()
    {
        if (Input.GetMouseButtonDown)
    }*/
}
