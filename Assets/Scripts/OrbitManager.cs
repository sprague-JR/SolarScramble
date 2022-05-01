using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
	// Gently rotates a directional light along the y axis simulating the orbit of the sun.

	public float speed;
	public float orbits;
	public TMPro.TextMeshProUGUI days, hours;
	public float timeRemaining = 100f;
	float timer = 0;

    void Update()
    {
		timer += Time.deltaTime;
		transform.Rotate(Vector3.up * speed * Time.deltaTime);

		// Total rotation since game began;
		float totalAngle = timer * speed;
		float total_orbits = totalAngle / 360f;

		timeRemaining = orbits - total_orbits;

		// Convert number of solar rotations to days and hours.
		int numDays = Mathf.FloorToInt(timeRemaining);
		int numHours = Mathf.FloorToInt((timeRemaining - numDays) * 24f);

		// Update UI.
		days.text = numDays.ToString() + " Days";
		hours.text = numHours.ToString() + " Hours";
		

	}
}
