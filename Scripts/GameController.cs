using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject player;

    //Message display
    public Text hud;
    public Canvas gameOverUI;

    //Time
    private float time = 0;

    private bool isRunning = false;

    // Use this for initialization
    void Start () {
            InGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            time += Time.deltaTime;
            hud.text = "Time elapsed " + (int)time;
        }
        else
        {
            hud.text = "You Win! Your time was: " + time;
        }
    }

    public void InGame()
    {
        time = 0;
        isRunning = true;

        gameOverUI.enabled = false;
    }

    public void Win()
    {
        isRunning = false;
        gameOverUI.enabled = true;
    } 
}
