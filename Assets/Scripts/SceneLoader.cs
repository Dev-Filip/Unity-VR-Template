using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Escape");
    }
}