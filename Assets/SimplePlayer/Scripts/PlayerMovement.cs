using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //set from in the PlayerInput Class
    [HideInInspector]
    public float HorizontalInput = 0f;
    [HideInInspector]
    public float VerticalInput = 0f;
    [HideInInspector]
    public bool JumpInput;
    [HideInInspector]
    public float MouseX = 0f;
    [HideInInspector]
    public float MouseY = 0f;

    //the pitch of the camera for look up and down
    float CameraPitch = 0f;

    CharacterController cc;
    float Gravity = -20.0f;
    float YVel = 0.0f;
    public float Speed = 10f;
    [Range(1f,50f)]
    public float MouseSensitivity = 10f;

    //find the camera for the look up/down. Will be a child of the Player
    Camera camera;



    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        camera = transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //move due to gravity
        YVel += Gravity * Time.deltaTime;
        //jump
        if (cc.isGrounded)
        {
            YVel = -0.1f;
            if (JumpInput)
            {
                YVel = 10.0f;
            }
        }
        Vector3 xmove = transform.right * HorizontalInput * Speed * Time.deltaTime;
        Vector3 zmove = transform.forward * VerticalInput * Speed * Time.deltaTime;
        Vector3 ymove = new Vector3(0, YVel * Time.deltaTime, 0);
        Vector3 move = xmove + ymove + zmove;
 
        cc.Move(move);

        //look control
        CameraPitch -= MouseY * MouseSensitivity;
        CameraPitch = Mathf.Clamp(CameraPitch, -60f, 85f);
        camera.transform.localEulerAngles = new Vector3(CameraPitch, 0, 0);
        //now rotate the whole charactercontroller withh the mouse x axis
        transform.Rotate(0, MouseX * MouseSensitivity, 0);


    }
}
