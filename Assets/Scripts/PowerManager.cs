using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{

	public Transform sun;
	public Transform planet;

	public float rechargeRate;
	public float curCharge = 1;
	public float ConsumptionRate;
	// Determines recharge rates, and how long the player can survive in the dark.

	public UnityEngine.UI.Image slider;
	// UI representing power left

	public Light llight, rlight;
	// Car headlights

	public UnityEngine.UI.Image left, centre, right;
	// Indicating the amount of charge the car is currently recieving.

	public Sprite full, empty;
	// Two different states for each charge indicator.


	public TMPro.TextMeshProUGUI localTime;

	private const float degreesToHours = 12f / 180f;
    void Update()
    {
		// Uses the angle between the vehicle and the sun to determine the amount of solar power the vehicle receives.
		float sunlight = Vector3.Angle((transform.position - planet.position).normalized, sun.forward * -1.0f);

		// Discretise the amount of sunlight received by the player for easier understanding.
		if(sunlight > 95)
		{
			// The vehicle is receiving no sunlight so gradually drain battery.
			curCharge -= ConsumptionRate * Time.deltaTime;

			// Turn on headlights to indicate vehicle is in darkness.
			llight.enabled = true;
			rlight.enabled = true;

			// Update power UI.
			right.sprite = empty;
			centre.sprite = empty;
			left.sprite = empty;
			
		}else if(sunlight > 60)
		{
			// Slowly charge battery due to limited amount of sunlight.
			curCharge += rechargeRate * Time.deltaTime * 0.25f;

			// Update Power UI.
			right.sprite = empty;
			centre.sprite = empty;
			left.sprite = full;


		}else if(sunlight > 40)
		{
			// Slowly charge battery due to limited amount of sunlight.
			curCharge += rechargeRate * 0.5f * Time.deltaTime;

			// Update Power UI.
			right.sprite = empty;
			centre.sprite = full;
			left.sprite = full;
		}
		else
		{
			// Only turn lights off once in full daylight to prevent flickering effect during local sunrise and sunset.
			llight.enabled = false;
			rlight.enabled = false;

			// Quickly charge the battery due to full sunlight.
			curCharge += rechargeRate * Time.deltaTime;

			// Update Power UI.
			right.sprite = full;
			centre.sprite = full;
			left.sprite = full;
		}

		// Turn off lights if the vehicle has run out of battery.
		llight.enabled = llight.enabled && curCharge > 0;
		rlight.enabled = rlight.enabled && curCharge > 0;

		curCharge = Mathf.Clamp01(curCharge);

		// Update UI.
		slider.fillAmount = curCharge;

		// Calculate the current 24hr time for the vehicle as additional UI element.
		Vector3 timezone = transform.position - planet.position;
		// Longitude is ommited from timezone calculations
		timezone.y = 0;
		
		float time = Vector3.Angle(transform.position - planet.position, sun.forward * -1f);

		time *= degreesToHours;

		//calculate AM or PM
		Vector3 west = Vector3.Cross(planet.position - transform.position, Vector3.up);
		bool pm = false;

		if(west.magnitude == 0)
		{
			// At one of the poles, therefore always mid day.
			pm = true;
		}
		else
		{
			if(Vector3.Angle(west,sun.forward * -1f) <= 90f)
			{
				pm = true;
			}
		}

		// Convert 12hr time to 24hr time.
		if (pm)
			time = time + 12;
		else
		{
			time = 12 - time;
		}

		// Update UI.
		int hours = Mathf.FloorToInt(time);
		int minutes = Mathf.FloorToInt((time - hours) * 60f);
		localTime.text = hours.ToString("00") + ":" + minutes.ToString("00");
    }
}
