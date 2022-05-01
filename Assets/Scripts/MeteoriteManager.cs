using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{

	// Determines speed and location at which the meteor lands.
	public float LandRadius;
	public float gravity;
	public float terminal_velocity;

	// Current velocity of meteore
	float velocity = 0f;

	// How long does the meteorite stay on the planet's surface.
	public float duration_min, duration_max;
	float duration;
	float timer;
	bool landed;

	// Allows the spawner to limit max number of asteroids at any one time.
	public static List<GameObject> active;

	public AudioSource impactNoise;

    void Awake()
    {
		duration = duration_min + (Random.value * duration_max);
		landed = false;

		if(active == null)
		{
			active = new List<GameObject>();
		}
		active.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

		if(transform.position.magnitude <= LandRadius)
		{
			// Meteor has landed.
			
			timer += Time.deltaTime;

			// Gradually shrink the meteor before destroying it completely.
			float msize = Mathf.Pow((timer / duration), 9f);
			msize = 1f - msize;
			transform.localScale = new Vector3(msize * 0.5f,msize * 0.5f,msize * 0.5f);

			if (!landed)
			{
				// When the meteorite first lands correct it's position and play a sound effect.
				transform.position = transform.position.normalized * LandRadius;
				impactNoise.Play();
				landed = true;
			}

			if (timer >= duration)
			{
				// Once timer has exceeded its limit, remove this game object from the active list, and destroy it.
				active.Remove(gameObject);
				GameObject.Destroy(gameObject);
			}
		}
		else
		{
			// Accelerate towards the centre of the planet.
			velocity = Mathf.Min(velocity + (gravity * Time.deltaTime), terminal_velocity);
			transform.Translate((transform.position * -1f).normalized * velocity);
		}
    }
}
