using UnityEngine;
using System.Collections;

public class WaterMovement : MonoBehaviour {

	Vector3 originalposition;

	// Use this for initialization
	void Start ()
    {
		originalposition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		transform.position = new Vector3(originalposition.x + Mathf.PingPong(Time.time / 100, 0.03f), transform.position.y, transform.position.z);
    }
}
