using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    public void LoadSinglePlayerGame() => SceneManager.LoadScene(1);

}


