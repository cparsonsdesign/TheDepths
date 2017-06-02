using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;


	
	void Update () {

        // xDegreesPerFrame = Time.deltaTime, 60 (seconds in a minute), 360 (degrees in a rotation), xRotationsPerMinute
        // Degrees frame^-1 = Seconds frame^-1 / Seconds minute^-1, Degrees Rotation^-1, Rotations Minute^-1

        // Degrees frame^-1 = frame^-1  minute, Degrees Rotation^-1, Rotations Minute^-1  (dividing the frist two causes the seconds to cancel out)
        // Degrees frame^-1 = frame^-1  minute * Degrees Rotation^-1 * Rotations Minute^-1

        // Degrees frame^-1 = frame ^-1 * Degrees   (By multiplying, all of the other variables will cancel each other out)

        float xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute; 
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute; ; 
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute; ;
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
