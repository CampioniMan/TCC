using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SegueObjeto : MonoBehaviour {

    public GameObject alvo;
    public float distanciaPerto = 10; // mais perto
    public float distanciaLonge = 35; // mais longe
    protected float distanciaAtual;

    // Use this for initialization
    void Start () {
        distanciaAtual = distanciaMedia();
        this.transform.position = new Vector3(alvo.transform.position.x, alvo.transform.position.y + (int)distanciaAtual, alvo.transform.position.z);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && distanciaAtual > distanciaPerto)
        {
            distanciaAtual--;
            Debug.Log(distanciaAtual);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && distanciaAtual < distanciaLonge)
        {
            distanciaAtual++;
            Debug.Log(distanciaAtual);
        }
        this.transform.position = new Vector3(alvo.transform.position.x, alvo.transform.position.y+ distanciaAtual, alvo.transform.position.z);
        Camera.main.orthographicSize = distanciaAtual;

    }

    float distanciaMedia()
    {
        return (distanciaPerto + distanciaLonge) / 2;
    }
}
