using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Transform cam;
    private RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;
    public float momentum;
    public float speed;
    public float step;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
                attached = true;
                rb.isKinematic = true;
            }
            /* else
                 attached = false;
                 rb.isKinematic = false;*/
        }

        if (Input.GetButtonUp("Fire1"))
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = cam.forward * momentum;
        }
        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        if (!attached && momentum >= 10)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;

            if (!attached)
            {
                momentum -= Time.deltaTime * 5;

            }
        }



    }
}
