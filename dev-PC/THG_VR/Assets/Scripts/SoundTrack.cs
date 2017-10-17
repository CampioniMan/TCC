using UnityEngine;
using System.Collections;

public class SoundTrack : MonoBehaviour {

    // Um segundo em Hz
    private const int SEGUNDO = 44100;

    public AudioClip soundTrack;
    private AudioSource audioLoader;

	// Use this for initialization
	void Start () {
        audioLoader = this.GetComponent<AudioSource>();
        audioLoader.clip = soundTrack;
        audioLoader.PlayDelayed(SEGUNDO * soundTrack.length);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Parar()
    {
        audioLoader.Stop();
    }
}
