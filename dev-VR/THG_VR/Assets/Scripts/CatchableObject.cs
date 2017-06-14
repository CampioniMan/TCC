using UnityEngine;
using System.Collections;

public class CatchableObject : MonoBehaviour {

    public bool taComArma;
    public bool estaAtirando;
    private Vector3 moveDirection = Vector3.zero;
    public Transform bala;
    private bool caught;
    public Transform target;
    private object someScript;
//    Rigidbody bodyBala;
    
    
    // Use this for initialization
    void Start () {
        caught = false;
        //taComArma = false;
	}

    

    void atirar()
    {
        bala.GetComponent<Rigidbody>().isKinematic = true;
        bala.GetComponent<Rigidbody>().useGravity = false;

        bala.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        bala.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);
        estaAtirando = true;     
        //bodyBala = bala.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (estaAtirando)
        {
            Vector3 userDirection = Vector3.forward;
            bala.transform.Translate(userDirection * 4  * Time.deltaTime);
            bala.transform.parent = null;
        }
        if (taComArma)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float val1 = GameObject.FindGameObjectWithTag("VrMain").transform.rotation.eulerAngles.y;
                float val2 = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.y - val1;
                float val3 = val2 + val1;
                //Debug.Log("Val 1: " + val1);
                //Debug.Log("Val 2: " + val2);
                //Debug.Log("Val 3: " + val3);
                float valX = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.x;
                bala.transform.eulerAngles = new Vector3(valX,val3,bala.transform.rotation.z);

                
                //float VarX = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.x;
                //Debug.Log("X : " + VarX);
                //bala.transform.rotation = Quaternion.AngleAxis(VarX, Vector3.right);

                atirar();
               
            }

            if(!estaAtirando)
            {
                float val = GameObject.FindGameObjectWithTag("VrMain").transform.rotation.eulerAngles.y;
                bala.transform.rotation = Quaternion.AngleAxis(val, Vector3.up);
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(target.transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward, 1))
            {
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.GetComponent<Rigidbody>().useGravity = false;

                this.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
                this.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);
                taComArma = true;     
            }


        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            this.transform.parent = GameObject.Find("VRMain").transform;
            this.transform.parent = null;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
            taComArma = false;
        }

        CharacterController controller = GetComponent<CharacterController>();


        Rigidbody body;
        body = gameObject.GetComponent<Rigidbody>();

        if(!controller.isGrounded)
            body.AddForce(new Vector3(0,-100f, 0));

    }

    
}
