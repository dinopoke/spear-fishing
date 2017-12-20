using UnityEngine;
using System.Collections;

public class SpearFish : MonoBehaviour {

    public GameObject circle;
    public GameObject splash;
    public GameObject hit;

    public float radiusMax = 6;
    public float speed = 1;

    public float radiusThreshold = 3;
    float lerpValue;
    float lerpRadius;

    public float circleHeight = 0.15f;
    float circleHeightFromCam;

    public GameObject rippleController;
    CreatePlayerRipple rippleScript;

	// Use this for initialization
	void Start () {
        circleHeightFromCam = Camera.main.GetComponent<CameraMovement>().cameraDistance - circleHeight;
        rippleScript = rippleController.GetComponent<CreatePlayerRipple>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public Vector3 GetPointOnScreen(Vector3 inputPosition) {
            Vector3 worldPos = inputPosition;
            worldPos.z = circleHeightFromCam;
        return Camera.main.ScreenToWorldPoint(worldPos);

    }

    public void StartSpearFish(Vector3 point) {
        circle.SetActive(true);
        circle.transform.position = point;
        circle.transform.localScale = new Vector3(radiusMax, radiusMax, 0);
        lerpValue = 0;
    }

    public void RadiusChange(Vector3 point) {
        circle.transform.position = point;
        lerpValue++;
        // TODO add more than just ping pong
        lerpRadius = Mathf.Lerp(radiusMax, 1, Mathf.PingPong(lerpValue * Time.fixedDeltaTime * speed, 1));
        circle.transform.localScale = new Vector3(lerpRadius, lerpRadius, 0);


    }

    public void EndSpearFish(Vector3 point) {
        circle.SetActive(false);
        float hitRadius = circle.GetComponent<Renderer>().bounds.extents.x;
        if (radiusMax - circle.transform.localScale.x > radiusThreshold) {
            Vector2 randomCirclePoint = Random.insideUnitCircle * hitRadius;
            point = (Vector3)randomCirclePoint + point;


            RaycastHit2D col = Physics2D.Raycast(point, Vector3.forward);
			if (col){ 
                // A fish has been hit
                if(col.collider.CompareTag("Fish")) {
                    point.z = -0.1f;
                    Instantiate(hit, point, Quaternion.identity);
                    col.collider.gameObject.GetComponent<FishMovement>().FishHit();
                }
            }
            else {
            Instantiate(splash, point, Quaternion.identity);
                }

            StartCoroutine(rippleScript.StartRipple(point, 1/ circle.transform.localScale.x));
        } 
    }

}
