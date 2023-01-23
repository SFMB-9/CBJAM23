using UnityEngine.SceneManagement;
using UnityEngine;

public class RetryLevel : MonoBehaviour
{
    public void onClick()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
