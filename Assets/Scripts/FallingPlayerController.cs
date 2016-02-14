using UnityEngine;
using System.Collections;

public class FallingPlayerController : MonoBehaviour {

	private Rigidbody rb;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
		
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
		    Vector3 push_away = new Vector3(Random.Range(-50.0F, 50.0F), 0, Random.Range(-50.0F, 50.0F));
			rb.AddForce (push_away);
		}
	}
}