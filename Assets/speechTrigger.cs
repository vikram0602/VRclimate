using UnityEngine;
using System.Collections;

public class speechTrigger : MonoBehaviour {

	public AudioSource audio_source;
	public AudioClip audioclip;
	public Collider myself;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			//Setting the speech
			audio_source.clip = audioclip;
			audio_source.loop = true;
			audio_source.Play();
			myself.enabled = false;
		}
    }
}
