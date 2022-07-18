//rete a faire
// - lerp gaze in - out
// - centraliser la log
// - mode sans VR
// - refaire le son des pas


//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    private float fire_start_time=0;

    private TextMeshProUGUI Txt;
    public SpriteRenderer GazeRing;
    public SpriteRenderer GazeRingTimer;


    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    private bool onFloor = true;

    private const float _maxDistance2 = 1000;
   // private TextMeshProUGUI Txt;
    private AudioSource audioSource;

    void Start()
    {
        // Txt = GameObject.Find("HUD_Text").GetComponent<TextMeshProUGUI>();
        //GazeRing = GameObject.Find("GazeRing").GetComponent<SpriteRenderer>();
        // GazeRing.sortingOrder = 150;


        
        GazeRingTimer.enabled = false;
        audioSource = GameObject.Find("sound_2").GetComponent<AudioSource>();
    }

    private bool isObjectController(GameObject go)
    {
        if (go != null)
        {
            Component[] components = go.GetComponents(typeof(Component));
            foreach (Component component in components)
            {
                if (component.GetType().ToString() == "ObjectController")
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
      

        if (Input.GetMouseButton(0))
        {

            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
/*
        //float angleY = currentRotation.y;
        float angle = transform.rotation.eulerAngles.x;
        // Txt.text = "---> "+ angle;
        //Debug.Log(Txt.text);
        if ((angle > 10) && (angle < 35))
        {
            Vector3 dir = (transform.forward / (2 * angle)) * Time.deltaTime * 100;
            dir.y = 0;
            transform.position += dir;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
        */
        RaycastHit hitfloor;
        int layerMaskFloor = 1 << 7;
        
        // Casts ray towards camera's down direction, to detect fllor and calculate height
        if (Physics.Raycast(transform.position, Vector3.down, out hitfloor, _maxDistance2, layerMaskFloor))
        {
            Vector3 pos = hitfloor.point; //get the position where the ray hit the ground
                                          //shoot a raycast up from that position towards the object
            Ray upRay = new Ray(pos, transform.position - pos);

            //get a point (vector3) in that ray 1.6 units from its origin
            Vector3 upDist = upRay.GetPoint(1.6f);
            //smoothly interpolate its position
            transform.position = Vector3.Lerp(transform.position, upDist, 0.5f);
            onFloor = true;
        }
        else
            onFloor = false;



        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at
        RaycastHit hit;
        int layerMaskObjects = 1 << 6;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, layerMaskObjects))
        {
            Debug.Log("---->hit");
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {

                // New GameObject.
                if (isObjectController(_gazedAtObject))
                {
                    GazeRing.size = new Vector2(1f, 1f);
                    GazeRingTimer.enabled = false;
                    GazeRing.enabled = true;
                    _gazedAtObject?.SendMessage("OnPointerExit");
                }
                _gazedAtObject = hit.transform.gameObject;
                if (isObjectController(_gazedAtObject))
                {
                    _gazedAtObject.SendMessage("OnPointerEnter");
                    fire_start_time = Time.time;
                    //Txt.text = "hit object";
                    GazeRing.size= new Vector2(3f, 3f);
                    GazeRingTimer.size = new Vector2(3f, 3f);
                    GazeRingTimer.enabled= true;
                    GazeRing.enabled = false;
                }
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
            GazeRing.size = new Vector2(1f, 1f);
            GazeRingTimer.enabled = false;
            GazeRing.enabled = true;
        }
        /*
        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
        */
        if (fire_start_time != 0)
            if (Time.time - fire_start_time > 1)
            {
                _gazedAtObject?.SendMessage("OnPointerClick");
                fire_start_time = 0;
                //Txt.text = "hit object: fire";
            }
        if (GazeRingTimer.enabled)
        {
            //Rotate gaze
            GazeRingTimer.transform.Rotate(Vector3.forward, Time.deltaTime*400);
            audioSource.Stop();
        }
        else
        {
            if (onFloor == true)
            {
                //move forward
                float angle = transform.rotation.eulerAngles.x;
                // Txt.text = "---> "+ angle;
                //Debug.Log(Txt.text);
                if ((angle > 10) && (angle < 35))
                {
                    Vector3 dir = (transform.forward / (2 * angle)) * Time.deltaTime * 100;
                    dir.y = 0;
                    transform.position += dir;
                    if (!audioSource.isPlaying)
                        audioSource.Play();
                }
                else
                    audioSource.Stop();
            }
            else
                audioSource.Stop();
        }
     

       
    }
}
