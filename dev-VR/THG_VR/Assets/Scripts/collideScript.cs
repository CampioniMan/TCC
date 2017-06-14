﻿using UnityEngine;
using System.Collections;

public class collideScript : MonoBehaviour {

    // Use this for initialization
    int i;
    bool b;
	void Start () {
        i = 0;
        b = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(b)
        {
            Debug.Log(i);
            i++;
        }

        if(i==15)
        {
            this.transform.position = new Vector3(1000, 1000,1000);
            i = 0;
            b = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, 1))
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(189, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
            b = true;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(255,255,255);
        }
    }

}
