using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class SocketMandaObjetos : MonoBehaviour {

    public bool socketReady = false;
    private TcpClient mySocket;
    private NetworkStream theStream;
    private StreamWriter theWriter;
    private StreamReader theReader;
    private const String separador = "(-)";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void mandarObjeto(GameObject obj)
    {
        writeString(obj.transform.ToString()+separador);
    }

    public void writeString(string theLine)
    {
        if (!socketReady) return;
        String foo = theLine + "\r\n";
        theWriter.Write(foo);
        theWriter.Flush();
    }
}
