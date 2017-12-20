using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
	
	const float minTurnAngle = 10f;
	const float minPinchDistance = 10;

	static public float turnAngleDelta;
	static public float turnAngle;

	static public float pinchDistanceDelta;
	static public float pinchDistance;

	bool singleTouch = false;

	public GameObject spear;
	public GameObject mainCamera;
    public GameObject rippleController;

	SpearFish spearScript;
	CameraMovement cameraScript; 
    CreatePlayerRipple rippleScript;

	public int bufferCap = 10;
	int frameTouchBuffer = 0;

	// Use this for initialization
	void Start () {
		// grab scripts
		spearScript = spear.GetComponent<SpearFish>();
		cameraScript = mainCamera.GetComponent<CameraMovement> ();
        rippleScript = rippleController.GetComponent<CreatePlayerRipple>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckTouchInput ();
	}

	void CheckTouchInput() {


        // Two finger swipe to pan camera
		if (Input.touchCount == 2 && singleTouch == false) {

			Touch touch1 = Input.touches[0];
			Touch touch2 = Input.touches[1];

			if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved) {
				if (!CheckTouchPinch (touch1, touch2) && !CheckTouchTurn (touch1, touch2)) {       
					cameraScript.MoveCamera(touch1.deltaPosition);
					Vector2 midpoint = (spearScript.GetPointOnScreen (touch1.position) + spearScript.GetPointOnScreen (touch2.position)) * 0.5f;
					rippleScript.CheckRipple(midpoint ,touch1.deltaPosition * 0.07f );
				}
			}
				

		}
        // One finger touch to spear fish
		else if (Input.touchCount == 1 || singleTouch == true) {
			Touch touch1 = Input.touches[0];

			if (touch1.phase == TouchPhase.Began) {
				// Break function here in case it was going to be a two finger swipe
				frameTouchBuffer++;
                
			} else if (touch1.phase == TouchPhase.Ended && singleTouch == true) {
				// We can tell the spear script that we let go
				singleTouch = false;
				spearScript.EndSpearFish (spearScript.GetPointOnScreen (touch1.position));
			}
			else if (frameTouchBuffer > bufferCap) {
				// Create the circle
				spearScript.StartSpearFish (spearScript.GetPointOnScreen(touch1.position));
				singleTouch = true;
				frameTouchBuffer = 0;
			}

			else if (frameTouchBuffer > 0 ) {
				frameTouchBuffer++;
			}

		 	else if (singleTouch == true) {
                // Run the radius change bit
				spearScript.RadiusChange(spearScript.GetPointOnScreen(touch1.position));
				
			}
				
		}
	}



	bool CheckTouchPinch (Touch touch1, Touch touch2) {

		// ... check the delta distance between them ...
		pinchDistance = Vector2.Distance(touch1.position, touch2.position);
		float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
		pinchDistanceDelta = pinchDistance - prevDistance;

		// ... if it's greater than a minimum threshold, it's a pinch!
		if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance) {
			return true;
		}
		else{
			// ... no pinch detected!
			return false;
		}
	}

	bool CheckTouchTurn(Touch touch1, Touch touch2) {

		// ... check the delta angle between them ...
		turnAngle = Angle(touch1.position, touch2.position);
		float prevTurn = Angle(touch1.position - touch1.deltaPosition,
			touch2.position - touch2.deltaPosition);
		turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

		// ... if it's greater than a minimum threshold, it's a turn!
		if (Mathf.Abs(turnAngleDelta) > minTurnAngle) {

			// ROTATION DOESN'T WORK YET

			//Debug.Log (turnAngleDelta);
			//cameraScript.RotateCamera(-turnAngleDelta);
			return true;
		} else {
            // ... no turn detected!
			return false;
		}

	}

	static private float Angle (Vector2 pos1, Vector2 pos2) {
		Vector2 from = pos2 - pos1;
		Vector2 to = new Vector2 (1, 0);

		float result = Vector2.Angle (from, to);
		Vector3 cross = Vector3.Cross (from, to);

		if (cross.z > 0) {
			result = 360f - result;
		}

		return result;
	}
}
