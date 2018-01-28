using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Button startText;
    public Button exitText;
    // Use this for initialization
    void Start () {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
    }
	
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
