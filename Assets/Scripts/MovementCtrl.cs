using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MovementCtrl : MonoBehaviour
{
    /*
    public float sensitivity = 10f;
   public float maxYAngle = 80f;
   private Vector2 currentRotation;
    
    private const float _maxDistance = 1000;
    private TextMeshProUGUI Txt;
    private AudioSource audioSource;
    */
    // Start is called before the first frame update
    void Start()
    {
       // Txt = GameObject.Find("HUD_Text").GetComponent<TextMeshProUGUI>();
      //  audioSource = GameObject.Find("sound_2").GetComponent<AudioSource>();
    }


  

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
        */
        /*
        RaycastHit hitfloor;
        int layerMaskFloor = 1 << 7;
        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 15, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hitfloor, _maxDistance, layerMaskFloor))
        {
            //Debug.Log("hit the floor:" + hitfloor.distance);
            if (hitfloor.distance < 10)
            {
                Vector3 dir = (transform.forward / (10 * hitfloor.distance * 2)) * Time.deltaTime*100;
                dir.y = 0;
                transform.position += dir;

               
                Txt.text = "hit the floor:" + hitfloor.distance;
            }
            else
                Txt.text = "hit the floor: No" ;
        }
        else
            Txt.text = "--->";
        */

        /*
        //float angleY = currentRotation.y;
        float angle = transform.rotation.eulerAngles.x;
       // Txt.text = "---> "+ angle;
        //Debug.Log(Txt.text);
        if ((angle > 10)&& (angle < 35))
        {
            Vector3 dir = (transform.forward / (2 * angle)) * Time.deltaTime * 100;
            dir.y = 0;
            transform.position += dir;
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
        */
        /*
        RaycastHit hitfloor;
        int layerMaskFloor = 1 << 7;
        //Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.down) * 15, Color.red);
        
        if (Physics.Raycast(transform.position, Vector3.down, out hitfloor, _maxDistance, layerMaskFloor))
        {
             Vector3 pos = hitfloor.point; //get the position where the ray hit the ground
                                     //shoot a raycast up from that position towards the object
            Ray upRay = new Ray(pos, transform.position - pos);

            //get a point (vector3) in that ray 1.6 units from its origin
            Vector3 upDist = upRay.GetPoint(1.6f);
            //smoothly interpolate its position
            transform.position = Vector3.Lerp(transform.position, upDist, 0.5f);
        }
       // else
        //    Txt.text = "--->";
        */
    }


}
        
   