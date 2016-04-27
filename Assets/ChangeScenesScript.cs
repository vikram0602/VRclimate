using UnityEngine;
using System.Collections;

public class ChangeScenesScript : MonoBehaviour {

	//Flags set by PickUpTrash and Protestors Scenes
	public bool pickuptrashdone;
	public bool protestorsdone;
	public bool pickuptrash_decision;
	public bool protestors_decision;
	
	//Audio SOurce and clip from player
	public AudioSource playervoice;
	public AudioClip playerclip;
	public GameObject player;
	
	//Objects that need to appear or disappear;
	public GameObject scene_neutral_stuff;
	public GameObject scene_trash_stuff;
	public GameObject scene_company_stuff;
	public GameObject scene_goodfuture_stuff;
	public GameObject scene_badfuture_stuff;
	public GameObject trash_sea;
	public GameObject company_sea;
	
	//Camera fading effect stuff
	public GameObject camera1;
	public GameObject camera2;
	
	//Some private stuff
	private float timer;
	private bool futurethoughtplayed;
	private bool futurethoughtplayed2;
	private int sequence_number;
	private float time_to_fade;
	
	// Use this for initialization
	void Start () {
		pickuptrashdone = false;
		protestorsdone = false;
		timer = 0.0f;
		futurethoughtplayed = false;
		futurethoughtplayed2 = false;
		sequence_number = 0;
		time_to_fade = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (protestorsdone && pickuptrashdone) {
		
			timer = timer + Time.deltaTime;
			
			if (sequence_number ==0 && timer > 5.0f) {
				playervoice.clip = playerclip;
				playervoice.loop = false;
				playervoice.Play();
				sequence_number = 1;
				timer = 0.0f;
			}
			
			else if (sequence_number ==1 && timer > playerclip.length +1.0f) {
				timer = 0.0f;
				sequence_number = 2;
			}
			
			else if (sequence_number ==2) {
				
				//camarita_linda_de_mis_amores.fieldOfView = 60.0f + 100*(timer)/(time_to_fade);
				
				if (timer>time_to_fade) {
					timer = 0.0f;
					sequence_number = 3;
				}	
			}
			
			else if (sequence_number==3) {
				sequence_number = 4;
				timer = 0.0f;
				player.transform.position = new Vector3(-313.0f, 21.51f, 116.59f);
				camera1.SetActive(false);
				camera2.SetActive(true);
				/*camarita_linda_de_mis_amores..clearFlags = CameraClearFlags.SolidColor;
				Color colorblack = Color.black;
				camarita_linda_de_mis_amores.backgroundColor = colorblack;
				camarita_linda_de_mis_amores.cullingMask = 0;*/
				
				if (pickuptrash_decision && protestors_decision) {
					//Activate Good future
					scene_neutral_stuff.SetActive(false);
					scene_trash_stuff.SetActive(false);
					scene_company_stuff.SetActive(false);
					scene_goodfuture_stuff.SetActive(true);
					scene_badfuture_stuff.SetActive(false);
					trash_sea.SetActive(false);
					company_sea.SetActive(false);
				}
				else if (!pickuptrash_decision && protestors_decision) {
					//Activate trash only bad future
					scene_neutral_stuff.SetActive(false);
					scene_trash_stuff.SetActive(true);
					scene_company_stuff.SetActive(false);
					scene_goodfuture_stuff.SetActive(false);
					scene_badfuture_stuff.SetActive(true);
					trash_sea.SetActive(true);
					company_sea.SetActive(false);
				}
				else if (pickuptrash_decision && !protestors_decision) {
					//Activate Company only bad future
					scene_neutral_stuff.SetActive(false);
					scene_trash_stuff.SetActive(false);
					scene_company_stuff.SetActive(true);
					scene_goodfuture_stuff.SetActive(false);
					scene_badfuture_stuff.SetActive(true);
					trash_sea.SetActive(false);
					company_sea.SetActive(true);
				}
				else if (!pickuptrash_decision && !protestors_decision) {
					//Activate Fully bad future
					scene_neutral_stuff.SetActive(false);
					scene_trash_stuff.SetActive(true);
					scene_company_stuff.SetActive(true);
					scene_goodfuture_stuff.SetActive(false);
					scene_badfuture_stuff.SetActive(true);
					trash_sea.SetActive(false);
					company_sea.SetActive(true);
				}
			}
			
			else if (sequence_number==4) {
				
				if (timer>2.0f) {
					camera1.SetActive(true);
					camera2.SetActive(false);
					sequence_number = 5;
				}
			}
		}
	}
}
