using UnityEngine;
using System.Collections;

public class CatchableObject : MonoBehaviour {

    private bool taComArma;
    private Transform balaAtual;
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
    public Transform target;


    
    void moverBalasAtiradas()
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

    void conectarBalasNoVetor()
    {
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
    }

    void prepararBalaAtual()
    {
        for (int i = 0; i < 10; i++)
        {
            if (balas[i].transform.position == new Vector3(50, 160, 90))
            {
                balaAtual = balas[i];
            }
        }

        float val1 = GameObject.FindGameObjectWithTag("VrMain").transform.rotation.eulerAngles.y;
        float val2 = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.y - val1;
        float val3 = val2 + val1;

        float valX = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.x;

        balaAtual.transform.eulerAngles = new Vector3(valX, val3, balaAtual.transform.rotation.z);
    }

    bool apertouBotaoDeTiro()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool podePegarArma()
    {
        return Physics.Raycast(target.transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward, 1);
    }

    bool apertouTab()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
    void atirar()
    {
        balaAtual.GetComponent<Rigidbody>().isKinematic = true;
        balaAtual.GetComponent<Rigidbody>().useGravity = false;
        balaAtual.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        balaAtual.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);   
    }

    void pegarArma()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;

        this.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        this.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);
        taComArma = true;
    }

    void soltarArma()
    {
        this.transform.parent = GameObject.Find("VRMain").transform;
        this.transform.parent = null;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        taComArma = false;
    }

    void aumentarGravidade()
    {
        CharacterController controller = GetComponent<CharacterController>();


        Rigidbody body;
        body = gameObject.GetComponent<Rigidbody>();

        if (!controller.isGrounded)
            body.AddForce(new Vector3(0, -100f, 0));
    }

    void Start()
    {
        taComArma = false;
        balas = new Transform[10];
        conectarBalasNoVetor();
        balaAtual = balas[0];
    }

    void Update()
    {
        moverBalasAtiradas();
                    
        if (taComArma && apertouBotaoDeTiro())
        {
                prepararBalaAtual();
                atirar();            
        }
        else
        if (apertouBotaoDeTiro())
        {
            if (podePegarArma())
            {
                pegarArma();                
            }
        }

        if (apertouTab() && taComArma)
        {
            soltarArma();
        }

        aumentarGravidade();
    }

    
}
