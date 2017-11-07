using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barraElixir : MonoBehaviour {

    public float taxaCrescimento = 5; // quanto cresce por tempo passado
    public float segundosParaCrescer = 1;
    public float maximoElixir = 100;
    public float multiplicador = 1;
    public Text output;

    private float quantoElixirTem = 0;
    private float tempoPassado = 0;
    private float taxaParaCrescerUmDeElixir;
    private bool ehPraDarOutput = false;

    // Use this for initialization
    void Start () {
        taxaParaCrescerUmDeElixir = (segundosParaCrescer / taxaCrescimento) / multiplicador;
        if (output != null)
            ehPraDarOutput = true;
    }
	
	// Update is called once per frame
	void Update () {
        tempoPassado += Time.deltaTime;
        if (tempoPassado >= taxaParaCrescerUmDeElixir)
        {
            tempoPassado -= taxaParaCrescerUmDeElixir;

            if (quantoElixirTem >= maximoElixir) // se atingiu o máximo de elixir
                quantoElixirTem = maximoElixir;
            else
            {
                quantoElixirTem++;
                if (ehPraDarOutput)
                    output.text = quantoElixirTem.ToString();
            }
        }
	}

    public void alteraMultiplicador(float novoMultiplicador)
    {
        multiplicador = novoMultiplicador;
        taxaParaCrescerUmDeElixir = (segundosParaCrescer / taxaCrescimento) / multiplicador;
    }

    // retorna se gastou ou não o elixir pedido
    public bool gastarElixir(int quanto)
    {
        if (quanto > quantoElixirTem)
            return false;

        quantoElixirTem -= quanto;
        if (ehPraDarOutput)
            output.text = quantoElixirTem.ToString();
        return true;
    }
}
