using UnityEngine;
using System.Collections;

public class CatchableObject : MonoBehaviour
{
    private bool taComArma;
    private const int GRAVITY = 4;
    private const int TEMPO_AR_MAX = 250;
    private Bala balaAtual;
    public Transform mira;
    public Arma arm;
    public Transform[] vetBalas = new Transform[40];
    public Bala[] balas = new Bala[40];
    public Transform target;
    public Transform target2;
    public int vidaObj = 100;

    void atualizarBalas()
    {        
        for(int i = 0;i< 40;i++)
        {
            Bala bala = arm.Municao[i];
            bala.moverBala();
            if (bala.JaColidiu)
            {
                bala.TempoAposColisao++;
                if (bala.TempoAposColisao >= 1)
                {
                    bala.receberEstadoAposColisao();
                }
            }

            if (bala.atirando())
            {
                bala.incTempoNoAr();
                if (bala.TempoNoAr >= TEMPO_AR_MAX)
                {
                    bala.doRespawn();
                }
            }
        }
    }

    bool apertouBotaoDeTiro()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool podePegarArma()
    {
        bool b = false;
        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward, out hit, 1f))
        {
            if (hit.transform.position == GameObject.Find("Sphere").transform.position)
                b = true;
        }
        return b;
    }

    bool apertouTab()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    void verificarMira()
    {

        if(taComArma && !mira.transform.parent == GameObject.FindGameObjectWithTag("MainCamera").transform)
        {
            mira.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
            mira.transform.localPosition = new Vector3(0.1f, -0.2f, 4f);
        }
        else
        {
            if (mira.transform.position != new Vector3(100, 100, 100))
                mira.transform.position = new Vector3(100, 100, 100);
        }
    }

    bool acertouAlvo()
    {
        if (!taComArma)
        {
            return false;
        }

        bool acertou = false;

        for (int i = 0; i < 40; i++)
        {
            if (arm.Municao[i].acertouAlvo(target2))
                acertou = true;
        }
        return acertou;
    }

    void atirar()
    {
        balaAtual.BalaTransform.GetComponent<Rigidbody>().isKinematic = true;
        balaAtual.BalaTransform.GetComponent<Rigidbody>().useGravity = false;
        balaAtual.BalaTransform.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        balaAtual.BalaTransform.transform.localPosition = new Vector3(1.00f, -0.4f, 1.2f);        
    }

    void pegarArma()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        this.transform.localPosition = new Vector3(1.26f, -1.1f, 1.2f);
        GameObject.Find("Armas").transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        this.transform.parent = GameObject.Find("Armas").transform;
        taComArma = true;
    }

    void soltarArma()
    {        
        this.transform.parent = null;
        GameObject.Find("Armas").transform.parent = null;
        this.transform.parent = GameObject.Find("Armas").transform;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        taComArma = false;
    }

    void imporGravidade()
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
        arm = gameObject.AddComponent(typeof(Arma)) as Arma;
        arm.createArma(this.transform,vetBalas,0);
    }

    void Update()
    {
        atualizarBalas();
        verificarMira();
        imporGravidade();
        arm.verificarRecarga();        
        if (taComArma && apertouBotaoDeTiro())
        {
            arm.atirar();
        }
        else
        if (apertouBotaoDeTiro())
        {
            if (podePegarArma())
            {
                pegarArma();
            }
        }

        if (acertouAlvo())
        {
            vidaObj -= 5;
            Renderer rend = target2.gameObject.GetComponent<Renderer>();

            if (vidaObj <= 0)
            {
                target2.transform.position = new Vector3(500, 160, 90);
            }
            else
            if (vidaObj <= 20)
            {
                if (rend.material.color != new Color(189, 0, 0))
                    rend.material.color = new Color(189, 0, 0);
            }
        }

        if (apertouTab() && taComArma)
        {
            soltarArma();
        }
    }
}
