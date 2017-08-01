using UnityEngine;
using System.Collections;

public class collideScript : MonoBehaviour {

    // Use this for initialization
    const int TEMPO_MAXIMO_AR = 1000;
    const int TEMPO_MAXIMO_COLISAO = 15;
    int tempoAposColisao;
    int tempoNoAr;
    bool jaColidiu;
	void Start () {
        tempoAposColisao = 0;
        tempoNoAr = 0;
        jaColidiu = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(jaColidiu)
        {
            tempoAposColisao++;
        }

        if(atirandoEssaBala())
        {
            tempoNoAr++;
            if(tempoNoAr >= TEMPO_MAXIMO_AR)
            {
                respawnarBala();               
            }

        }

        if (tempoAposColisao >= TEMPO_MAXIMO_COLISAO)
        {
            respawnarBala();
        }
        

        if (colidiu())
        {
            //this.gameObject.GetComponent<Renderer>().material.color = new Color(189, 0, 0);
            ativarPosColisao();
            
        }
        //else
        //{
        //    this.gameObject.GetComponent<Renderer>().material.color = new Color(255,255,255);
        //}
    }

    void respawnarBala()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = new Vector3(50, 160, 90);
        tempoAposColisao = 0;
        jaColidiu = false;
        tempoNoAr = 0;
    }

    bool atirandoEssaBala()
    {
        return this.transform.position != new Vector3(50, 160, 90);
    }

    bool colidiu()
    {
        return Physics.Raycast(transform.position, transform.forward, 1) && this.transform.position != new Vector3(50, 160, 90);
    }
    void ativarPosColisao()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        jaColidiu = true;
    }
}
