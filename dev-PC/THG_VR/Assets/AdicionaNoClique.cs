using UnityEngine;
using System.Collections;

public class AdicionaNoClique : MonoBehaviour {
    
    public GameObject[] prefabs;
    public int selecionado;

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
                GameObject cube = Instantiate(prefabs[selecionado]);
                cube.transform.parent = GameObject.Find("CoisasParaMandar").transform;
                cube.transform.position = hit.point;
            }
        }
    }
}
