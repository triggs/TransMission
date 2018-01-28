using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Text newGameText;
    public Text exitGameText;
    public Button startText;
    public Button exitText;
    // Use this for initialization
    void Start () {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void StartGame()
    {
        // SceneManager.LoadScene("Difficulty");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
