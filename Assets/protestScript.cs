using UnityEngine;
using System.Collections;

public class protestScript : MonoBehaviour {

	//Protestor stuff
	public GameObject protestor;
	public Animator protestor_animator;
	public RuntimeAnimatorController protestor_animatorcontroller_standup;
	public RuntimeAnimatorController protestor_animatorcontroller_arguing;
	public RuntimeAnimatorController protestor_animatorcontroller_idle;
	public RuntimeAnimatorController protestor_animatorcontroller_talking;
	public AudioSource protestor_audio_source;
	public AudioClip protestor_audioclip_1;
	public AudioClip protestor_audioclip_2;
	public AudioClip protestor_audioclip_3;
	public AudioClip protestor_audioclip_4good;
	public AudioClip protestor_audioclip_4bad;
	
	//Company Guy Stuff
	public GameObject companyguy;
	public Animator companyguy_animator;
	public RuntimeAnimatorController companyguy_animatorcontroller_noding;
	public RuntimeAnimatorController companyguy_animatorcontroller_idle;
	public RuntimeAnimatorController companyguy_animatorcontroller_talking;
	public RuntimeAnimatorController companyguy_animatorcontroller_walking;
	public AudioSource companyguy_audio_source;
	public AudioClip companyguy_audioclip_1;
	public AudioClip companyguy_audioclip_2;	
	
	//Player stuff
	public GameObject player;
	public AudioSource player_audiosource;
	public AudioClip player_good;
	public AudioClip player_bad;
	
	//Collider stuff
	public Collider myself;
	
	//private stuff
	private bool timerstart;
	private float timer;
	private int sequence_number;
	private bool decision;
	private bool done_talking;
	
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		timerstart = false;
		sequence_number = -1;
		decision = false;
		done_talking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerstart) timer = timer+Time.deltaTime;
		
		if (sequence_number==0 ) {
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_standup;
			sequence_number = 1;
			timer = 0.0f;
		}
		//The leader stands up and talks
		//The companyguy walks towards the leader
		else if (sequence_number ==1 && timer>2.5f) {
			Vector3 targetPosition1 = new Vector3(player.transform.position.x, protestor.transform.position.y, player.transform.position.z);
			protestor.transform.LookAt(targetPosition1);
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_talking;
			protestor_audio_source.clip = protestor_audioclip_1;
			protestor_audio_source.loop = false;
			protestor_audio_source.Play();
			
			Vector3 targetPosition2 = new Vector3(protestor.transform.position.x, companyguy.transform.position.y, protestor.transform.position.z);
			companyguy.transform.LookAt(targetPosition2);
			companyguy_animator.runtimeAnimatorController = companyguy_animatorcontroller_walking;
			
			timer = 0.0f;
			sequence_number = 2;
		}
		
		//The companyguy interrupts the leader
		else if (sequence_number==2 & timer>protestor_audioclip_1.length) {
			companyguy_animator.runtimeAnimatorController = companyguy_animatorcontroller_talking;
			companyguy_audio_source.clip = companyguy_audioclip_1;
			companyguy_audio_source.loop = false;
			companyguy_audio_source.Play();
			
			Vector3 targetPosition1 = new Vector3(companyguy.transform.position.x, protestor.transform.position.y, companyguy.transform.position.z);
			protestor.transform.LookAt(targetPosition1);
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_idle;
			timer = 0.0f;
			sequence_number =3;
		}
		
		//The leader says the sick people line
		else if (sequence_number==3 & timer>companyguy_audioclip_1.length) {
			
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_arguing;
			protestor_audio_source.clip = protestor_audioclip_2;
			protestor_audio_source.loop = false;
			protestor_audio_source.Play();
			
			companyguy_animator.runtimeAnimatorController = companyguy_animatorcontroller_noding;
			timer = 0.0f;	
			sequence_number =4;
		}
		
		//The companyguy replies
		else if (sequence_number==4 & timer>protestor_audioclip_2.length) {
			companyguy_animator.runtimeAnimatorController = companyguy_animatorcontroller_talking;
			companyguy_audio_source.clip = companyguy_audioclip_2;
			companyguy_audio_source.loop = false;
			companyguy_audio_source.Play();
			
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_idle;
			timer = 0.0f;
			sequence_number=5;
		}
		
		//Leader talks to user again
		else if (sequence_number==5 & timer>companyguy_audioclip_2.length) {
			Vector3 targetPosition1 = new Vector3(player.transform.position.x, protestor.transform.position.y, player.transform.position.z);
			protestor.transform.LookAt(targetPosition1);
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_talking;
			protestor_audio_source.clip = protestor_audioclip_3;
			protestor_audio_source.loop = false;
			protestor_audio_source.Play();
			
			Vector3 targetPosition2 = new Vector3(player.transform.position.x, companyguy.transform.position.y, player.transform.position.z);
			companyguy.transform.LookAt(targetPosition2);
			companyguy_animator.runtimeAnimatorController = companyguy_animatorcontroller_idle;
			
			timer = 0.0f;
			sequence_number=6;	
		}
		
		//Waiting for player's reply
		else if (sequence_number==6 & timer>protestor_audioclip_3.length) {
			if (!done_talking) {
				protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_idle;
				done_talking = true;
			}
			
			if(Input.GetButtonDown("Fire2")) {
				player_audiosource.loop = false;
				player_audiosource.clip = player_good;
				player_audiosource.Play();
				decision = true;
				timer = 0.0f;
				sequence_number=7;
			}
			else if(Input.GetButtonDown("Fire3")) {
				player_audiosource.loop = false;
				player_audiosource.clip = player_bad;
				player_audiosource.Play();
				decision = false;
				timer = 0.0f;
				sequence_number=7;
			}			
		}
		
		//Player replied already
		else if (sequence_number==7 && ((decision && timer>=player_good.length) || (!decision && timer>=player_bad.length))) {
			if (decision) {
				protestor_audio_source.clip = protestor_audioclip_4good;
				protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_talking;
			}	
			else {
				protestor_audio_source.clip = protestor_audioclip_4bad;
				protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_arguing;
			}	
			protestor_audio_source.loop = false;
			protestor_audio_source.Play();
			
			timer = 0.0f;
			sequence_number=8;
		}	
		
		else if (sequence_number==8 && ((decision && timer>=protestor_audioclip_4good.length) || (!decision && timer>=protestor_audioclip_4bad.length))) {
			protestor_animator.runtimeAnimatorController = protestor_animatorcontroller_idle;
		}
	
	}
	
	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "Player") {
			timer = 0.0f;
			timerstart = true;
			myself.enabled = false;
			sequence_number = 0;
			//Making the characters look at the player
			/*Vector3 targetPosition1 = new Vector3(player.transform.position.x, gossiper1.transform.position.y, player.transform.position.z);
			Vector3 targetPosition2 = new Vector3(player.transform.position.x, gossiper2.transform.position.y, player.transform.position.z);
			gossiper1.transform.LookAt(targetPosition1);
			gossiper2.transform.LookAt(targetPosition2);*/
			
		}
    }
}
