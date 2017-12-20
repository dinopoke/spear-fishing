using UnityEngine;
using System.Collections;

public class LotusRipple : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Lotus")) {
			col.gameObject.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position) * 0.05f);
		}
	}
}
