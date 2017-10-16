using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Tocou");
    }

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("TocouC");
    }
}
