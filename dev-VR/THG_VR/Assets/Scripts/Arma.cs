using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour
{
    private int tipo; //0 = shotgun  2 = pistola    3 = espada 
    private Transform armaTransform;
    private Bala[] municao = new Bala[40];
    Bala blAt;
    int delay = 0;
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
        if (contBala > 39)
            contBala = 0;

        blAt = municao[contBala++];
        blAt.prepararParaAtirar();
        
    }

    public void atirar()
    {        
        if(!semBalas() && !recarregando)
        {
            prepararBalaAtual();
            blAt.BalaTransform.GetComponent<Rigidbody>().isKinematic = true;
            blAt.BalaTransform.GetComponent<Rigidbody>().useGravity = false;
            blAt.BalaTransform.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
            blAt.BalaTransform.transform.localPosition = new Vector3(1.00f, -0.4f, 1.5f);
            if(qtBalas>0)
                qtBalas--;
            delay = 0;
        }                    
    }



    public bool semBalas()
    {
        return qtBalas == 0;
    }

    public void verificarRecarga()
    {
        if (delay < 10)
            delay++;
                
        if(recarregando)
        {
            tempoRecarga++;
            if(tempoRecarga >= TEMPO_RECARGA)
            {
                recarregarOK();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && qtBalas != 40)
        {
            recarregando = true;
        }
    }
    
    public void setDelay(int nDelay)
    {
        delay = nDelay;
    }
        
    public bool podeAtirar()
    {
        return delay >= 10;       
    }
    public void recarregarOK()
    {
        recarregando = false;
        tempoRecarga = 0;
        qtBalas = 40;
    }
}
