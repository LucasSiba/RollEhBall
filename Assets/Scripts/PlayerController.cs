using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public  float      speed;
	public  Text       countText;
	public  Text       winText;
	public  Text       fpsText;
	public  AudioClip  popSoundOne;
	public  AudioClip  popSoundTwo;
	public  AudioClip  winSound;
	public  GameObject policeCar;

	private AudioSource audioSource;
	private int         count;
	private Rigidbody   rb;
	private float       deltaTime = 0.0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.} FPS", fps);
		fpsText.text = text;
	}

	void FixedUpdate ()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			Vector3 movement = new Vector3 (-touchDeltaPosition.x, 0.0f, -touchDeltaPosition.y);
			rb.AddForce (movement * speed);
		} else {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();

			if (Random.value > 0.5) {
				audioSource.PlayOneShot (popSoundOne, 1F);
			} else {
				audioSource.PlayOneShot (popSoundTwo, 1F);
			}
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 8)
		{
			winText.text = "You Win!";
			audioSource.PlayOneShot (winSound, 1F);
			policeCar.SetActive (true);
		}
	}
}