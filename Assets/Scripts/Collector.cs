using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
	//Collects meteorites for points.

	public int pointsPerMeteorite;
	public int TotalPoints = 0;
	private AudioSource pickupNoise;

	public TMPro.TextMeshProUGUI score;

	public void Awake()
	{
		pickupNoise = GetComponent<AudioSource>();
	}


	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "meteorite")
		{
			// Remove the collected meteorite from the game and update score.
			MeteoriteManager.active.Remove(collision.gameObject);
			collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
			collision.collider.enabled = false;
			// Only destroy the meteorite after a delay so that any audio it is playing isn't cut off.
			GameObject.Destroy(collision.gameObject,5f);

			TotalPoints += pointsPerMeteorite;

			if(!pickupNoise.isPlaying)
			{
				pickupNoise.Play();
			}

			// Update UI.
			score.text = TotalPoints.ToString();
		}
	}

}
