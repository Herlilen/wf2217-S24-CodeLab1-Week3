using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //mouse speed ratio
    public float mouseSensitivity = 100f;
    //get transform 
    public Transform playerBody;

    private float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        //using input system to read the mouse axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //change yaw with mouse Y
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //lock the yaw angle
        
        //rotate the player game object with mouse x axis
        playerBody.Rotate(Vector3.up * mouseX);
        //rotate yaw of camera
        gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
