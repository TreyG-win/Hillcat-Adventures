using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamerPCCounter : BaseCounter
{
    public int sceneIndexToLoad;
    public override void Interact(Player player){SceneManager.LoadScene(sceneIndexToLoad, LoadSceneMode.Single);}

}
