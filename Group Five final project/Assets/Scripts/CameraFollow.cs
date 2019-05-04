using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    public int FollowDistance = 15;
    public int FollowHeight = 4;
    public int UpOffset = 2;
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = ((transform.position - player.transform.position).normalized) * 17;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player != null)
        {
            transform.position = player.transform.position + offset;
            //transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles - new Vector3(0, 35, 0));
            //transform.LookAt(player.transform);
            //transform.rotation = Quaternion.Euler(new Vector3(45, 45, 0));
            Vector3 newCamPos = player.transform.position + (-player.transform.forward * FollowDistance);
            newCamPos.y += FollowHeight; 
            transform.position = newCamPos;
            transform.LookAt(player.transform.position + player.transform.up.normalized * UpOffset);
            RaycastHit rayHit;
            if (Physics.Raycast(player.transform.position, transform.TransformDirection(-Vector3.forward), out rayHit, 15))
            {
                //Debug.DrawRay(player.transform.position, transform.TransformDirection(-Vector3.forward) * rayHit.distance, Color.yellow);
                transform.position = new Vector3(rayHit.point.x, transform.position.y * 0.70f, rayHit.point.z);
            }
        }
    }
}
