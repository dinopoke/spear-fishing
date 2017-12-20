using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour {

    public GameObject[] fishPrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.childCount < 2 && fishPrefabs.Length > 0) {

            int randomSpawnNumber = Random.Range(0, fishPrefabs.Length);

            float randomx = Random.Range(0, 7) * (Random.Range(0,2) * 2 - 1);
            float randomy = Random.Range(0, 7) * (Random.Range(0,2) * 2 - 1);
            Vector3 spawnPoint = new Vector3(randomx, randomy, -0.05f);
            Vector3 screenPos = Camera.main.WorldToViewportPoint(spawnPoint);

            if (screenPos.x < 0 || screenPos.x > 1) {
                if (screenPos.y < 0 || screenPos.y > 1) {
                    GameObject spawn = Instantiate(fishPrefabs[randomSpawnNumber], spawnPoint , Quaternion.identity) as GameObject;
                    spawn.transform.parent = transform;
                }
            }
        }
	}


            //float randomx = Random.Range(0, 40f) * (Random.Range(0,2) * 2 - 1);
            //if (randomx >= 0) {
            //    randomx = randomx + 1;
            //}
            //float randomy = Random.Range(0, 40f) * (Random.Range(0,2) * 2 - 1);
            //if (randomy >= 0) {
            //    randomy = randomy + 1;
            //}

            //Vector3 spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomx, randomy, -0.1f));
}
