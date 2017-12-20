using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour {

    public float turnSpeed = 5;
    public float moveSpeed = 6;

    public float waitAfterTurn = 2;
    public float waitAfterMove = 3;

    private Rigidbody2D thisRigidbody;

    public GameObject ripple;
    public float rippleSpeed = 10;
    public float rippleMaxSize = 100;


	// Use this for initialization
	void Start () {
	    thisRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(RandomMovement());

        
	}
	
	// Update is called once per frame

	void Update () {
        
        
	}


    IEnumerator RandomMovement() {
        while (true) {

            thisRigidbody.AddTorque(Random.Range(-turnSpeed, turnSpeed));
            yield return new WaitForSeconds(Random.Range(0, waitAfterTurn));
            MoveForward(moveSpeed);
            StartCoroutine(CreateRipple());
            yield return new WaitForSeconds(Random.Range(0, waitAfterMove));
            
        }
    }

    void AddTorque(float torque) {

        thisRigidbody.AddTorque(torque);

    }

    public void MoveForward(float speed) {
        thisRigidbody.AddForce(transform.up * speed * 100  * Time.fixedDeltaTime);
    }

    IEnumerator CreateRipple() {
        GameObject currentRipple =  Instantiate(ripple, transform.position, Quaternion.identity) as GameObject;
        currentRipple.transform.parent = gameObject.transform;
        float newSize = 1;
        while (newSize < rippleMaxSize) {
            currentRipple.transform.localScale = new Vector3(newSize, newSize, 0);
            newSize += Time.deltaTime * rippleSpeed;
            yield return null;
        }
        Destroy(currentRipple);
    }

    public void FishHit() {
        Destroy(gameObject);
    }
}
