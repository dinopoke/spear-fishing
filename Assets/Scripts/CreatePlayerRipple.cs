using UnityEngine;
using System.Collections;

public class CreatePlayerRipple : MonoBehaviour {

    public GameObject playerRipple;

    float timeToRipple = 0;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CheckRipple(Vector3 location, Vector2 deltaVector) {
        float speed = deltaVector.magnitude;

        if (timeToRipple > 30) {
            StartCoroutine(StartRipple(location, speed));
            timeToRipple = 0;
        }
        else{
        timeToRipple = timeToRipple + speed;
        }

    }

    public IEnumerator StartRipple(Vector3 location, float speed) {

        GameObject currentRipple = Instantiate(playerRipple, location, Quaternion.identity) as GameObject;
        float newSize = 1;
        while (newSize < 3 * speed) {
            currentRipple.transform.localScale = new Vector3(newSize, newSize, 0);
            newSize += Time.deltaTime * speed;
            yield return null;
        }
        Destroy(currentRipple);
    }
}
