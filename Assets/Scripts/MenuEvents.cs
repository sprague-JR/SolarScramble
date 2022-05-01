using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuEvents : MonoBehaviour
{
	// Handles the implementation of main menu buttons.

	public void PlaySmall()
	{
		SceneManager.LoadScene("SmallPlanet", LoadSceneMode.Single);
	}

	public void PlayMedium()
	{
		SceneManager.LoadScene("LargePlanet", LoadSceneMode.Single);
	}

	public void PlayLarge()
	{
		SceneManager.LoadScene("MediumPlanet", LoadSceneMode.Single);
	}


	public void Quit()
	{
		Application.Quit();
	}


}
