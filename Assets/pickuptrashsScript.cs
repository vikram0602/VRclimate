using UnityEngine;
using System.Collections;

public class pickuptrashsScript : MonoBehaviour {

	private float timer;
	private float timer2;
	private bool timerstart;
	private bool pending_decision_to_pick_up;
	private bool flyer_decision;
	private bool flyer_accepted;
	private int initiate_sequence;
	private float audio_length;
	
	
	public Collider myself;
	public MeshRenderer garbage;
	
	//Player stuff
	public GameObject player;
	public AudioSource audio_voice;
	public AudioClip audioclip_should_I_pick_this_up;
	public AudioClip audioclip_accepted_flyer;
	public AudioClip audioclip_denied_flyer;
	
	//Scientist stuff
	public GameObject scientist;
	public AudioSource audio_scientist_voice;
	public AudioClip audioclip_scientist_1good;
	public AudioClip audioclip_scientist_2good;
	public AudioClip audioclip_scientist_1bad;
	public AudioClip audioclip_scientist_2bad;
	public Animator scientist_animator;
	public RuntimeAnimatorController scientist_animatorcontroller_leftturn;
	public RuntimeAnimatorController scientist_animatorcontroller_walking;
	public RuntimeAnimatorController scientist_animatorcontroller_talking;
	public RuntimeAnimatorController scientist_animatorcontroller_flyer;
	public RuntimeAnimatorController scientist_animatorcontroller_pointing;
	public RuntimeAnimatorController scientist_animatorcontroller_idle;
	
	public bool decision;
	
	
	// Use this for initialization
	void Start () {
		timer =0.0f;
		timer2 =0.0f;
		timerstart = false;
		pending_decision_to_pick_up = false;
		decision = false;
		initiate_sequence = -1;
		flyer_decision = false;
		flyer_accepted = false;
		if (audioclip_scientist_1good.length>audioclip_scientist_1bad.length) audio_length = audioclip_scientist_1good.length;
		else audio_length = audioclip_scientist_1bad.length;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timerstart) {
			timer = timer+Time.deltaTime;
		}
		
		if (flyer_accepted) {
			timer2 =timer2+Time.deltaTime;
		}
		//Fire2 is ps4's X
		//Fire3 is ps4's O
		if (pending_decision_to_pick_up && initiate_sequence==-1) {
			if(Input.GetButtonDown("Fire2")) {
				decision = true;
				initiate_sequence = 0;
				scientist.SetActive(true);
				timer = 0.0f;
				timerstart = true;
				audio_scientist_voice.clip = audioclip_scientist_1good;
				audio_scientist_voice.loop = false;
				garbage.enabled = false;
				scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_leftturn;
			}
			else if(Input.GetButtonDown("Fire3")) {
				decision = false;
				initiate_sequence = 0;
				scientist.SetActive(true);
				timer = 0.0f;
				timerstart = true;
				audio_scientist_voice.clip = audioclip_scientist_1bad;
				audio_scientist_voice.loop = false;
				scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_leftturn;
			}
		}
		
		if (initiate_sequence==0 && timer>=3.0f && timer<4.2f) {
			Vector3 targetPosition1 = new Vector3(player.transform.position.x, scientist.transform.position.y, player.transform.position.z);
			scientist.transform.LookAt(targetPosition1);
			scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_walking;	
			initiate_sequence = 1;
		}
		//else if (initiate_sequence==1 && timer>=4.0f && ((decision && timer<audioclip_scientist_1good.length+4.0f)||(!decision && timer<audioclip_scientist_1bad.length+4.0f))) {
		//else if (initiate_sequence==1 && timer>=4.0f && (timer< audio_length +4.0f)) {
		else if (initiate_sequence==1 && timer>=4.2f/*((decision && timer<audioclip_scientist_1good.length+4.0f) || (!decision && timer<audioclip_scientist_1bad.length+4.0f))*/) {
			
			Vector3 targetPosition1 = new Vector3(player.transform.position.x, scientist.transform.position.y, player.transform.position.z);
			scientist.transform.LookAt(targetPosition1);
			scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_talking;
			audio_scientist_voice.Play();
			initiate_sequence = 2;
		}
		//else if (initiate_sequence==2 && (decision && timer>=audioclip_scientist_1good.length+4.0f)||(!decision && timer>=audioclip_scientist_1bad.length+4.0f)) {
		//else if (initiate_sequence==2 && (timer>= audio_length +4.0f)) {
		else if (initiate_sequence==2 && ((decision && timer>=audioclip_scientist_1good.length+4.2f) || (!decision && timer>=audioclip_scientist_1bad.length+4.2f))) {
			Vector3 targetPosition1 = new Vector3(player.transform.position.x, scientist.transform.position.y, player.transform.position.z);
			scientist.transform.LookAt(targetPosition1);
			scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_flyer;
			initiate_sequence = 3;
			timerstart = false;			
		}
		
		else if (initiate_sequence == 3 && !flyer_accepted) {
			if(Input.GetButtonDown("Fire2")) {
				scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_idle;
				flyer_accepted = true;
				flyer_decision = true;
				audio_voice.clip = audioclip_accepted_flyer;
				audio_voice.loop = false;
				audio_voice.Play();
				
				timer2=0.0f;
				initiate_sequence = 4;
			}
			else if(Input.GetButtonDown("Fire3")) {
				scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_idle;
				flyer_accepted = true;
				flyer_decision = false;
				audio_voice.clip = audioclip_denied_flyer;
				audio_voice.loop = false;
				audio_voice.Play();
				timer2=0.0f;
				initiate_sequence = 4;
			}
		}
		
		else if (initiate_sequence==4 && ((flyer_decision && timer2>=audioclip_accepted_flyer.length) || (!flyer_decision && timer2>=audioclip_denied_flyer.length))) {
			
			if(flyer_decision) audio_scientist_voice.clip = audioclip_scientist_2good;
			else audio_scientist_voice.clip = audioclip_scientist_2bad;
			audio_scientist_voice.loop = false;
			audio_scientist_voice.Play();
			scientist_animator.runtimeAnimatorController = scientist_animatorcontroller_pointing;
			initiate_sequence = 5;
		}
		
	}
	
	void OnTriggerEnter(Collider other){
	
		if (other.gameObject.tag == "Player") {
			timer = 0.0f;
			timerstart = false;
			
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
        
    }
}
