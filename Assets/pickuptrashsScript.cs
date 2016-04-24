using UnityEngine;
using System.Collections;

public class pickuptrashsScript : MonoBehaviour {

	private float timer;
	private bool timerstart;
	private bool pending_decision_to_pick_up;
	
	public AudioSource audio_voice;
	public AudioClip audioclip_should_I_pick_this_up;
	public GameObject scientist;
	public Collider myself;
	public GameObject garbage;
	
	public bool decision;
	
	// Use this for initialization
	void Start () {
		timer =0.0f;
		timerstart = false;
		pending_decision_to_pick_up = false;
		decision = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		//Fire2 is ps4's X
		//Fire3 is ps4's O
		if (pending_decision_to_pick_up) {
			if(Input.GetButtonDown("Fire2")) {
				decision = true;
			}
			if(Input.GetButtonDown("Fire3")) {
				decision = false;
			}
			
			if (decision==true) {
				garbage.SetActive(false);
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		timer = 0.0f;
		timerstart = true;
		
		//Making the characters look at the player
		/*Vector3 targetPosition1 = new Vector3(player.transform.position.x, gossiper1.transform.position.y, player.transform.position.z);
		Vector3 targetPosition2 = new Vector3(player.transform.position.x, gossiper2.transform.position.y, player.transform.position.z);
		gossiper1.transform.LookAt(targetPosition1);
		gossiper2.transform.LookAt(targetPosition2);*/
				
		//Setting the speech when the characters interacts with the player
		audio_voice.clip = audioclip_should_I_pick_this_up;
		audio_voice.loop = false;
        audio_voice.Play();
		myself.enabled = false;
		
		pending_decision_to_pick_up = true;
		
        
    }
	
	/*void OnClick() {
		//Fire2 is ps4's X
		//Fire3 is ps4's O
		if (pending_decision_to_pick_up) {
			if(Input.GetButtonDown("Fire2")) {
				decision = true;
			}
			if(Input.GetButtonDown("Fire3")) {
				decision = false;
			}
		}
	}*/
}
