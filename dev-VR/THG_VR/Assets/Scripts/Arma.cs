using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour
{
    private int tipo; //0 = shotgun  2 = pistola    3 = espada 
    private Transform armaTransform;
    private Bala[] municao = new Bala[40];
    Bala blAt;
    private int qtBalas;
    private bool recarregando;
    private int tempoRecarga = 0;
    private const int TEMPO_RECARGA = 50;
    int contBala = 0;

    public Bala[] Municao
    {
        get
        {
            return municao;
        }

        set
        {
            municao = value;
        }
    }

    public void createArma(Transform transf, Transform[] balas, int qualArma)
    {
        tipo = qualArma;
        armaTransform = transf;
        for (int i = 0; i < 40; i++)
        {
            municao[i] = gameObject.AddComponent(typeof(Bala)) as Bala;
            municao[i].createBala(balas[i]);
            municao[i].TempoNoAr = 0;
            municao[i].VetorGravidade = Vector3.zero;
            municao[i].TempoAposColisao = 0;
            municao[i].Velocidade = 40;
            municao[i].JaColidiu = false;
        }
        qtBalas = 40;
        recarregando = false;
    }
    public Arma()
    {
        tipo = 0;
        qtBalas = 40;
        recarregando = false;
    }

    void prepararBalaAtual()
    {
        if (contBala < 40)
        {
            blAt = municao[contBala++];
            blAt.prepararParaAtirar();
        }

    }

    public void atirar()
    {
        if (!semBalas())
        {
            prepararBalaAtual();
            blAt.BalaTransform.GetComponent<Rigidbody>().isKinematic = true;
            blAt.BalaTransform.GetComponent<Rigidbody>().useGravity = false;
            blAt.BalaTransform.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
            blAt.BalaTransform.transform.localPosition = new Vector3(1.00f, -0.4f, 1.2f);
            if (qtBalas > 0)
                qtBalas--;
        }
    }



    public bool semBalas()
    {
        return qtBalas == 0;
    }

    public void verificarRecarga()
    {
        if (recarregando)
        {
            tempoRecarga++;
            if (tempoRecarga >= TEMPO_RECARGA)
            {
                recarregar();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            contBala = 0;
            recarregando = true;
        }
    }

    public void recarregar()
    {
        recarregando = false;
        tempoRecarga = 0;
        qtBalas = 40;
    }
}
