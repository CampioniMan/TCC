using UnityEngine;
using System.Collections;

public class ScriptMenu : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Sair()
    {
        // fechar o socket de conexão, se tiver(vai que)
        // parar o áudio

        // realmente sair
        Application.Quit();
    }
}
