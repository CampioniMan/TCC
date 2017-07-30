using UnityEngine;
using System.Collections;

public class CatchableObject : MonoBehaviour {

    public bool taComArma;
    private Vector3 moveDirection = Vector3.zero;
    private Transform[] balas;
    public Transform bala1;
    public Transform bala2;
    public Transform bala3;
    public Transform bala4;
    public Transform bala5;
    public Transform bala6;
    public Transform bala7;
    public Transform bala8;
    public Transform bala9;
    public Transform bala10;
    private Transform balaGeral;
    private int[] tempo;
    private bool caught;
    public Transform target;
    private object someScript;
//    Rigidbody bodyBala;
    
    
    // Use this for initialization
    void Start () {
        caught = false;
        tempo = new int[10];
        balas = new Transform[10];
        balas[0] = bala1;
        balas[1] = bala2;
        balas[2] = bala3;
        balas[3] = bala4;
        balas[4] = bala5;
        balas[5] = bala6;
        balas[6] = bala7;
        balas[7] = bala8;
        balas[8] = bala9;
        balas[9] = bala10;
        balaGeral = balas[0];
        //taComArma = false;
	}

    
    void moverBalas()
    {
        for(int i = 0;i<10;i++)
        {
            if(balas[i].transform.position != new Vector3(50,160,90))
            {
                Vector3 userDirection = Vector3.forward;
                balas[i].transform.Translate(userDirection * 4 * Time.deltaTime);
                balas[i].transform.parent = null;
            }
        }
    }

    void prepararBala()
    {
        for (int i = 0; i < 10; i++)
        {
            if (balas[i].transform.position == new Vector3(50, 160, 90))
            {
                balaGeral = balas[i];
            }
        }
    }
    void atirar()
    {
        balaGeral.GetComponent<Rigidbody>().isKinematic = true;
        balaGeral.GetComponent<Rigidbody>().useGravity = false;

        balaGeral.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        balaGeral.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);   
    }

    // Update is called once per frame

    void Update()
    {
        moverBalas();
            
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
                prepararBala();
                balaGeral.transform.eulerAngles = new Vector3(valX,val3,balaGeral.transform.rotation.z);

                
                //float VarX = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.x;
                //Debug.Log("X : " + VarX);
                //balaGeral.transform.rotation = Quaternion.AngleAxis(VarX, Vector3.right);
                atirar();
               
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
