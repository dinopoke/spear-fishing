using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

    float timer = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer + Time.deltaTime;
        if (timer > 1) {
            Destroy(gameObject);
        }
	}
}
