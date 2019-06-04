using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Cmds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    Debug.Log(SceneManager.sceneCount + ": scene count");
  }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleRoomba()
    {
      GameObject.Find("Roomba").GetComponent<RoombaMovement>().isON =
        !GameObject.Find("Roomba").GetComponent<RoombaMovement>().isON;
    }

    public void GoToMainMenu()
    {
      SceneManager.LoadScene("TitleScreen");
    }

    public void ExitScreen(GameObject screen)
    {
      screen.SetActive(false);
    }

    public void OpenScreen(GameObject screen)
    {
      screen.SetActive(true);
    }

    public void Play()
    {
      SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
      Application.Quit();
    }

}
