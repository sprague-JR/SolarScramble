using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
	public Transform planet;
	public GameObject prefab;
	public float radius;

	public float delay_min, delay_max;
	float delay;
	public int max_meteorites;
	float timer = 0;

	public float planetRadius = 5;
    // Start is called before the first frame update
    void Start()
    {
		delay = delay_min + (Random.value * delay_max);
		timer = 0;
		SpawnMeteor();
	}

    // Update is called once per frame
    void Update()
    {
		if(timer > delay && MeteoriteManager.active.Count < max_meteorites)
		{
			// Spawn meteors at random intervals.
			delay = delay_min + (Random.value * delay_max);
			timer = 0;
			SpawnMeteor();
		}
		timer += Time.deltaTime;
    }



	void SpawnMeteor()
	{
		// Generate a random vector on the unit sphere then multiply by the spawning radius to get the start position.
		Vector3 fromCentre = new Vector3((Random.value * 2f) -1f, (Random.value * 2f) - 1f, (Random.value * 2f) - 1f).normalized * radius;
		Vector3 location = planet.transform.position + fromCentre;

		GameObject meteor = GameObject.Instantiate(prefab, location, Quaternion.identity);
		meteor.GetComponent<MeteoriteManager>().LandRadius = planetRadius;
	}
}
