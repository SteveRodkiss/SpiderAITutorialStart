using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{

    PlayerMovement playerMovement;

    // Use this for initialization
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.VerticalInput = Input.GetAxis("Vertical");
        playerMovement.HorizontalInput = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.JumpInput = true;
        }
        else
        {
            playerMovement.JumpInput = false;
        }

        playerMovement.MouseX = Input.GetAxis("Mouse X");
        playerMovement.MouseY = Input.GetAxis("Mouse Y");
    }


    void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
