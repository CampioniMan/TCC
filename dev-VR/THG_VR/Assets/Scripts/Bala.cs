using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour
{
    private Transform balaTransform;
    private bool jaColidiu;
    private int tempoNoAr;
    private Vector3 vetorGravidade;
    private int velocidade;
    private int tempoAposColisao;
    private const int TEMPO_COLISAO_LIMITE = 1;
    private const int TEMPO_AR_LIMITE = 1;
    private const int GRAVITY = 4;
    private const float RAIO = 0.25f;

    public Transform BalaTransform
    {
        get
        {
            return balaTransform;
        }

        set
        {
            balaTransform = value;
        }
    }

    public bool JaColidiu
    {
        get
        {
            return jaColidiu;
        }

        set
        {
            jaColidiu = value;
        }
    }

    public int TempoNoAr
    {
        get
        {
            return tempoNoAr;
        }

        set
        {
            tempoNoAr = value;
        }
    }


    public Vector3 VetorGravidade
    {
        get
        {
            return vetorGravidade;
        }

        set
        {
            vetorGravidade = value;
        }
    }


    public int Velocidade
    {
        get
        {
            return velocidade;
        }

        set
        {
            velocidade = value;
        }
    }

    public int TempoAposColisao
    {
        get
        {
            return tempoAposColisao;
        }

        set
        {
            tempoAposColisao = value;
        }
    }

    public static int GET_TEMPO_COLISAO_LIMITE
    {
        get
        {
            return TEMPO_COLISAO_LIMITE;
        }
    }

    public void calcGravidadeY(float valor)
    {
        vetorGravidade.y -= valor;
    }

    public void createBala(Transform tReference)
    {
        BalaTransform = tReference;
	BalaTransform.localScale = new Vector3(RAIO,RAIO,RAIO);
	BalaTransform.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        JaColidiu = false;
        TempoNoAr = 0;
        Velocidade = 40;
        VetorGravidade = Vector3.zero;
        TempoAposColisao = 0;
    }

    public void doRespawn()
    {
        balaTransform.GetComponent<Rigidbody>().isKinematic = true;
        balaTransform.GetComponent<Rigidbody>().useGravity = false;
        balaTransform.transform.position = new Vector3(50, 160, 90);
        jaColidiu = false;
        tempoAposColisao = 0;
        velocidade = 40;
        tempoNoAr = 0;
        vetorGravidade = Vector3.zero;
    }

    public void moverBala()
    {
        if (atirando()) {
            Vector3 userDirection = Vector3.forward;
            balaTransform.transform.Translate(userDirection * velocidade * Time.deltaTime);
            if (!jaColidiu)
            {
                calcGravidadeY(GRAVITY * Time.deltaTime);
                balaTransform.transform.Translate(vetorGravidade * Time.deltaTime);
            }

            if (balaTransform.transform.parent != GameObject.Find("Municao").transform)
                balaTransform.transform.parent = GameObject.Find("Municao").transform;
        }
    }

    public void incTempoNoAr()
    { 
        tempoNoAr++;
    }

    public bool passouTempoLimiteNoAr()
    {
        return tempoNoAr >= TEMPO_AR_LIMITE;
    }

    public void receberEstadoAposColisao()
    {
        this.velocidade = 0;
        this.balaTransform.GetComponent<Rigidbody>().isKinematic = false;
        this.balaTransform.GetComponent<Rigidbody>().useGravity = true;
    }


    public  bool atirando()
    {
        return this.balaTransform.position != new Vector3(50, 160, 90);
    }

    public void prepararParaAtirar()
    {
        if (!this.atirando())
        {
            float val1 = GameObject.FindGameObjectWithTag("VrMain").transform.rotation.eulerAngles.y;
            float val2 = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.y - val1;
            float val3 = val2 + val1;

            float valX = GameObject.FindGameObjectWithTag("HeadTag").transform.rotation.eulerAngles.x;

            this.balaTransform.transform.eulerAngles = new Vector3(valX, val3, this.balaTransform.transform.rotation.z);
            this.velocidade = 40;
        }
    }

    public bool acertouAlvo(Transform target)
    {
        RaycastHit hit;

        if  (
            Physics.Raycast(this.balaTransform.transform.position, this.balaTransform.transform.forward, out hit, 2) && !this.jaColidiu
            && hit.transform.localScale != this.balaTransform.transform.localScale
            ||
            Physics.Raycast(this.balaTransform.transform.position, this.balaTransform.transform.forward, out hit, 1) && !this.jaColidiu
            && hit.transform.localScale != this.balaTransform.transform.localScale
            )
        {
            this.jaColidiu = true;
            this.vetorGravidade = Vector3.zero;
            this.velocidade = 40;

            if (hit.transform.position == target.transform.position)
                return true;
        }
        return false;
    }
}
