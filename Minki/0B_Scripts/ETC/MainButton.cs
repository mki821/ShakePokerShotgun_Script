using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Last_Lobby");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
