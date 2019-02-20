 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class QM_CharController : MonoBehaviour {
 
     private float moveSpeed = 6; // move speed
     private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
     private float gravity = 10; // gravity acceleration
     private bool isGrounded;
     private float deltaGround = 0.2f; // character is grounded up to this distance
     private float jumpSpeed = 10; // vertical jump initial speed
     private float jumpRange = 10; // range to detect target wall
     private Vector3 surfaceNormal; // current surface normal
     private Vector3 myNormal; // character normal
     private float distGround; // distance from character position to ground
     private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
     private float vertSpeed = 0; // vertical jump current speed
 
     private Transform myTransform;
     //public BoxCollider boxCollider; // drag BoxCollider ref in editor
     public Rigidbody rigidbody;
     public CapsuleCollider capsule;




    public GameObject cameraBoom;
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public float maxVeritcalAim;
    public float minVerticalAim;
    
    GameObject character;
    private void Start(){
         myNormal = transform.up; // normal starts as character up direction
         myTransform = transform;
         GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation

        // distance from transform.position to ground
        distGround = capsule.height - capsule.center.y;
        //distGround = boxCollider.size.y - boxCollider.center.y;

        character = this.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
 
     private void FixedUpdate(){
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity* (rigidbody.mass*myNormal));
 }
 
     private void Update(){
     // jump code - jump to wall or simple jump
     if (jumping) return; // abort Update while jumping to a wall
 
         Ray ray;
         RaycastHit hit;
 
     if (Input.GetButtonDown("Jump")){ // jump pressed:
         ray = new Ray(myTransform.position, myTransform.forward);
            Debug.DrawRay(myTransform.position,myTransform.forward,Color.red);
         if (Physics.Raycast(ray, out hit, jumpRange)){ // wall ahead?
             JumpToWall(hit.point, hit.normal); // yes: jump to the wall
         }
         else if (isGrounded){ // no: if grounded, jump up
                rigidbody.velocity += jumpSpeed * myNormal;
         }
     }
     
     // movement code - turn left/right with Horizontal axis:
     //myTransform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);
     // update surface normal and isGrounded:
     ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
     if (Physics.Raycast(ray, out hit)){ // use it to update myNormal and isGrounded
         isGrounded = hit.distance <= distGround + deltaGround;
         surfaceNormal = hit.normal;
     }
     else {
         isGrounded = false;
         // assume usual ground normal to avoid "falling forever"
         surfaceNormal = Vector3.up;
     }

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;




        if (-mouseLook.y < maxVeritcalAim && -mouseLook.y > minVerticalAim)
        {
            cameraBoom.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        }




        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
     // find forward direction with new myNormal:
     Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
     // align character to the new myNormal while keeping the forward direction:
     Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        Vector3 targetRotEuler = targetRot.eulerAngles;

        #region HORIZONTAL_ROTATION
        if(Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * 10f * Time.deltaTime, 0);
        }
        #endregion


        if (myNormal == new Vector3(0, 1, 0) || myNormal == new Vector3(0, 0, -1))
        {
            //targetRotEuler.y = Quaternion.AngleAxis(mouseLook.x, character.transform.up).eulerAngles.y;
        }
        targetRot.eulerAngles = targetRotEuler;

        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed*Time.deltaTime);
        
        // move the character forth/back with Vertical axis:
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        myTransform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0,0 );
    }
 
     private void JumpToWall(Vector3 point, Vector3 normal){
     // jump to wall
     jumping = true; // signal it's jumping to wall
     rigidbody.isKinematic = true; // disable physics while jumping
     Vector3 orgPos = myTransform.position;
     Quaternion orgRot = myTransform.rotation;
     Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
     Vector3 myForward = Vector3.Cross(myTransform.right, normal);
     Quaternion dstRot = Quaternion.LookRotation(myForward, normal);
     
         StartCoroutine (jumpTime (orgPos, orgRot, dstPos, dstRot, normal));
         //jumptime
 }
 
     private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal) {
         for (float t = 0.0f; t < 1.0f; ){
             t += Time.deltaTime;
             myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
             myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
             yield return null; // return here next frame
         }
         myNormal = normal; // update myNormal
         rigidbody.isKinematic = false; // enable physics
         jumping = false; // jumping to wall finished
 
         }
 
 }