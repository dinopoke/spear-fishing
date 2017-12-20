using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float movespeed = 1f;
    public float tiltAmount = 15f;
	public float resetSpeed = 2f;
    Vector2 rotationDirection;
    Vector3 resetPosition;
	Quaternion resetRotation;

    public float minX = 1;
    public float maxX = 1;
    public float minY = 1;
    public float maxY = 1;


    public float cameraDistance = 2f;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
        resetPosition.z = -cameraDistance;
        transform.position = resetPosition;
	}
	
	// Update is called once per frame
	void Update () {

        //RESETTING THE TILT
		//resetRotation = new Quaternion( 0,0, transform.rotation.z,0);

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * resetSpeed);
        resetPosition = transform.position;
        resetPosition.z = -cameraDistance;
		transform.position = Vector3.Lerp(transform.position, resetPosition, Time.deltaTime * resetSpeed);
	}

	public void MoveCamera(Vector2 direction) {
		direction = direction * Time.deltaTime * movespeed / 10;
		transform.Translate(-direction.x, -direction.y, 0);

        // CLAMP
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);



        // TILTING
        rotationDirection.x = direction.y;
        rotationDirection.y = -direction.x;
        transform.Rotate(rotationDirection * tiltAmount);

	}

	public void RotateCamera(float angle){
		transform.Rotate(new Vector3 (0, 0, angle));
	}

}
