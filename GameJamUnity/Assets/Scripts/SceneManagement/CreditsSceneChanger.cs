using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneChanger : MonoBehaviour
{
string sceneToLoad; //Takes the name of the scene you want to load

    //Loads the indicated scene using the LoadScene function from the Unity SceneManager
    // TO SELECT THE NEXT SCENE, GO TO THE LAST EVENT IN THE ANIMATOR FOR THE CreditsObject AND INPUT THE NAME OF THE DESIRED SCENE
    public void loadScene(string selectedScene){
        SceneManager.LoadScene(selectedScene);
    }

}
