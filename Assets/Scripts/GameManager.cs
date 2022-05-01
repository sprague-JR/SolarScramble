using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Start is called before the first frame update
	public Car car;
	public PowerManager power;
	public MeteorSpawner aster;
	public OrbitManager orb;

	public Animator menuAnim;

    void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
		// Once time has run out, end the game.
		if(orb.timeRemaining <= 0)
		{
			orb.days.text = "0 Days";
			orb.hours.text = "0 Hours";
			car.enabled = false;
			power.enabled = false;
			aster.enabled = false;
			orb.enabled = false;

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			menuAnim.SetBool("IsComplete", true);
		}
        
    }

	// Load main menu.
	public void Quit()
	{
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

	// Reload the same scene.
	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
	}
}
