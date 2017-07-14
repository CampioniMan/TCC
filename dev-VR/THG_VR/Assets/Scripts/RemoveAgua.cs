using UnityEngine;
using System.Collections;
using System;

public class RemoveAgua : MonoBehaviour {
    private int[] indices = {72*3, 73*3};
    private int[] original;

    // Use this for initialization
    void Start () {
        original = transform.GetComponent<MeshFilter>().mesh.triangles;
    }
	
	// Update is called once per frame
	void Update () {
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        transform.GetComponent<MeshFilter>().mesh.triangles = original;
        int[] comBuraco = new int[original.Length - (3 * indices.Length)];
        for (int i = 0, j = 0; j < mesh.triangles.Length;)
        {
            if (Array.IndexOf(indices, j) == -1)
            {
                comBuraco[i++] = mesh.triangles[j++];
                comBuraco[i++] = mesh.triangles[j++];
                comBuraco[i++] = mesh.triangles[j++];
            }
            else
            {
                j += 3;
            }
        }
        transform.GetComponent<MeshFilter>().mesh.triangles = comBuraco;
    }
}
