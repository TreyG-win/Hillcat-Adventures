using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{

    private bool isLoaded;

    [Header("Finley's PlayerInput")]
    [Tooltip("Drag the player (I.E. Finley) into this selection, this will connect the player input here.")]
    public PlayerInput playerInput;
    void Start()
    {
        if(SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
        
    }

    public void LoadSelectedScene(int scene)
    {
        if (!isLoaded)
        {
            playerInput.enabled = false;
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            isLoaded = true;
        }
        
    }

    public void UnloadSelectedScene(int scene)
    {
        if (isLoaded)
        {
            playerInput.enabled = true;
            SceneManager.UnloadSceneAsync(scene);
            isLoaded = false;
        }
       
    }

    public void restartScene(int scene)
    {
        UnloadSelectedScene(scene);
        LoadSelectedScene(scene);
    }

}
