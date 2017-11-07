using UnityEngine;
using System.Collections;

public class AdicionaNoClique : MonoBehaviour {
    
    public GameObject[] prefabs;
    public int selecionado;
    public const string TAG_DEFAULT_SEND = "Enviar";

    private string nomeDefault = "Inimigo";
    private int qtosClick = 0;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (this.GetComponent<barraElixir>().gastarElixir(10))
                {
                    GameObject cube = Instantiate(prefabs[selecionado]); // criando uma instância do objeto
                    cube.name = nomeDefault + qtosClick++;               // colocando um nome mais apropriado
                    cube.tag = TAG_DEFAULT_SEND;                         // colocando uma tag apropriada
                    cube.transform.parent = GameObject.Find("CoisasParaMandar").transform; // setando o pai dele
                    cube.transform.position = hit.point;                 // setando a posição espacial dele
                }
            }
        }
    }
}
