using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpaceship : MonoBehaviour {

    private int _index = 0;

    Vector3[] finalPositions = { new Vector3(11.9f, -6.05f, 0), new Vector3(9.24f, -7.02f, 0), new Vector3(11.92f, -5.3f, 0) };
    Vector3[] initialPositions = { new Vector3(-10.99f, 6.0f, 0), new Vector3(-11.87f, 2.15f, 0), new Vector3(1.01f, 6.54f, 0) };

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Update () {
        
        transform.Translate(finalPositions[_index] * 0.15f * Time.deltaTime);

        if (transform.position.x > 11.0f) {

            _index = Random.Range(0, 3);
    
            transform.position = initialPositions[_index];
        }
	}

}
