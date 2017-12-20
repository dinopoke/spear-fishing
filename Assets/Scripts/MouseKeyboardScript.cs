using UnityEngine;
using System.Collections;

public class MouseKeyboardScript : MonoBehaviour {


  	public GameObject spear;
	public GameObject mainCamera;
    public GameObject rippleController;

	SpearFish spearScript;
	CameraMovement cameraScript;
    CreatePlayerRipple rippleScript;

    Vector2 moveDirection; 

	// Use this for initialization
	void Start () {
		// grab scripts
		spearScript = spear.GetComponent<SpearFish>();
		cameraScript = mainCamera.GetComponent<CameraMovement> ();
        rippleScript = rippleController.GetComponent<CreatePlayerRipple>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeyboardInput();
        CheckMouseInput();
	}	

    void CheckKeyboardInput() {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        rippleScript.CheckRipple(spearScript.GetPointOnScreen(Input.mousePosition), moveDirection);

        cameraScript.MoveCamera(moveDirection * 10);

    }

    void CheckMouseInput() {
        if (Input.GetMouseButtonDown(0)) {
            spearScript.StartSpearFish(spearScript.GetPointOnScreen(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0)) {
            spearScript.RadiusChange(spearScript.GetPointOnScreen(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0)) {
              spearScript.EndSpearFish(spearScript.GetPointOnScreen(Input.mousePosition));
        }

    }
}
