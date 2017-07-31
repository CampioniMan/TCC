using UnityEngine;
using System.Collections;

public class controller2 : MonoBehaviour
{
    public float speed = 5.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float jumpSpeed = 12.0F;
    public float gravity = 20.0F;
    private bool parar = false;
    private Vector3 spawnPoint;
    private bool walking = true;
    public GameObject ground;
    void Start()
    {
        spawnPoint = transform.position;
    }
    void Update(){
        CharacterController controller = GetComponent<CharacterController>();
  
        if(Input.GetKeyDown(KeyCode.P))
        {
            walking = !walking;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && controller.isGrounded)
        {
            speed = 10.0F;
            jumpSpeed = 13.0F;
        }
        else
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            parar = true;  
        }
        if(controller.isGrounded && parar == true)
        {
            speed = 5.0F;
            parar = false;
            jumpSpeed = 12.0F;
        }

        if (walking)
        {
            for (int i = 0; i < speed; i++)
            {
                transform.position += Camera.main.transform.forward * .5f * 0.05F;
            }

        }
        
        if (Physics.Raycast(transform.position, transform.up, -100))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                moveDirection.y = jumpSpeed;
            }
                
        }

        if (transform.position.y < -10f)
        {
            transform.position = spawnPoint;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);   

    }

}
