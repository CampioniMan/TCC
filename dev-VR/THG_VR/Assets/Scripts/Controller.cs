using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private bool rotacionando = false;
    protected Caracteristicas car;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20.0F;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3.0F;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            rotacionando = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            rotacionando = false;
        }

        if (rotacionando)
        {
            float y = this.transform.localPosition.y + transform.Find("Head").localPosition.y;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, y, this.transform.localPosition.z);
        }

        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
    }
}