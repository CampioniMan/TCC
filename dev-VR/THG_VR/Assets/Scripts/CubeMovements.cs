using UnityEngine;
using System.Collections;

public class CubeMovements : MonoBehaviour {

    int i;
    Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
        i = 0;
	}

    void moverCubo()
    {
        Vector3 userDirection = Vector3.forward;
        this.transform.Translate(userDirection * Time.deltaTime);
        this.transform.parent = null;

    }


    // Update is called once per frame
    void Update () {
        if(Physics.Raycast(this.transform.position,this.transform.up,-1))
            this.GetComponent<Rigidbody>().MovePosition(this.transform.position + this.transform.forward * 10.0f*Time.deltaTime);
        i++;
        if(i == 40)
        {
            transform.Rotate(0,90, 0);
            i = 0;
        }
	}
}
