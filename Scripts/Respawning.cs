using UnityEngine;
using System.Collections;

public class Respawning : MonoBehaviour {
    public GameObject RespawnPoint;
	
    void OnTriggerEnter(Collider other) {
        other.gameObject.transform.position = RespawnPoint.transform.position;
    }
}
