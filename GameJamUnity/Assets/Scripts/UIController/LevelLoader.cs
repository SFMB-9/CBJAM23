using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    public static LevelLoader instance;
    
    private bool isTransitioning = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void LoadNextLevel()
    {
        if (isTransitioning) return;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void LoadLevelByIndex(int levelIndex)
    {
        if (isTransitioning) return;
        StartCoroutine(LoadLevel(levelIndex));
    }
    
    
    IEnumerator LoadLevel(int levelIndex)
    {
        if (isTransitioning) yield break;
            isTransitioning = true;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        isTransitioning = false;
        SceneManager.LoadScene(levelIndex);
    }


}
