using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerManager))]
public class Car : MonoBehaviour
{
	// Orbit based character controller
	// Designed to control a vehicle acting on a sphere

	// The Centre of the sphere currently being rotated around.
	public Transform centre;
	public float radius;
	public float speed;
	public float RotSpeed;
	public float acceleration;
	float curSpeed;

	private PowerManager power; 

	void Awake()
	{
		power = GetComponent<PowerManager>();
	}

    void Update()
    {
		float charge = power.curCharge;

		charge = charge <= 0 ? 0 : 1;
		//converts float of how much charge is left to bool of whether any charge is left
		
		// Retrieve user input.
		float LeftAxis = Input.GetAxis("left");
		float RightAxis = Input.GetAxis("right");

		
		float RotDelta = RightAxis - LeftAxis;
		RotDelta *= charge;
		//between -1 and 1, representing left and right turns respectively.

		transform.Rotate(Vector3.up * RotDelta * RotSpeed * Time.deltaTime, Space.Self);

		//Movement
		float desired_speed = (LeftAxis + RightAxis) * speed * 0.5f;
		desired_speed *= charge;

		curSpeed = Mathf.Lerp(curSpeed, desired_speed, acceleration * Time.deltaTime);
		

		Vector3 nextPos = transform.position + (transform.forward * curSpeed * Time.deltaTime);
		nextPos = centre.position + ((nextPos - centre.position).normalized * radius);
		transform.position = nextPos;

		transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, getNormal(transform.position)), getNormal(transform.position));
    }

	Vector3 getNormal(Vector3 point)
	{
		return (point - centre.position).normalized;
	}

}
