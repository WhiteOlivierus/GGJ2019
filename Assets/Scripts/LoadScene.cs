using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadAScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
