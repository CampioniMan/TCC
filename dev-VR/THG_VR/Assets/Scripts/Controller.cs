using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class controller : MonoBehaviour
{
    public float speed = 3.0F;
    private const float MAX_SPEED = 15.0F;
    private const float MIN_SPEED = 3.0F;
    public float rotateSpeed = 3.0F;
    private Vector3 moveDirection = Vector3.zero;
    public const float JUMP_SPEED = 15.0F;
    public const float GRAVITY= 20.0F;
    private Vector3 spawnPoint;

    bool apertouBotaoCorrer()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
    
    bool soltouBotaoCorrer()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }

    bool podePular()
    {
        return Physics.Raycast(transform.position, transform.up, -1);
    }

    void pular()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = JUMP_SPEED;
        }
    }

    void adicionarGravidade()
    {
        moveDirection.y -= GRAVITY * Time.deltaTime;
    }

    void moverPersonagem()
    {
        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(Camera.main.transform.forward * curSpeed);
    }

    void respawn()
    {
        transform.position = new Vector3(spawnPoint.x + 50, spawnPoint.y, spawnPoint.z + 50);
    }


    void Start()
    {
        spawnPoint = transform.position;
    }

    void Update()
    {
        if (apertouBotaoCorrer())
        {
            speed = MAX_SPEED;
        }
        else
        if (soltouBotaoCorrer())
        {
            speed = MIN_SPEED;
        }

        if (podePular())
        {
            pular();
        }

        adicionarGravidade();

        moverPersonagem();    

        if (transform.position.y < -50f)
        {
            respawn();
        }
    }

}

