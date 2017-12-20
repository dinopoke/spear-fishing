using UnityEngine;
using System.Collections;

public class RippleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Fish")) {
            //col.transform.rotation = Quaternion.LookRotation(Vector3.forward, col.transform.position - transform.position);
            col.gameObject.GetComponent<FishMovement>().MoveForward(7f);
        }
        else if (col.gameObject.CompareTag("Lotus")) {
			col.gameObject.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position) * 0.07f);
        }
    }
}
