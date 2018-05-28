using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInverse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.forward,-1f * Time.deltaTime);
    }
}
