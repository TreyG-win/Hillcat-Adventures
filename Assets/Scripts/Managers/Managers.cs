using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Properly runs each of the managers (when more are available) by allowing
 * different scripts to call the necessary manager script to run functions.
 * 
 * This script is meant to be attached to an empty game object to
 * act as a manager object.
 */
[RequireComponent(typeof(PlayerManager))]
//[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    //The list of managers will go below the InventoryManager, being
    //initialized in the same manner.
    //public static InventoryManager Inventory { get; private set; }

    public static PlayerManager Player { get; private set; }

    private List<IGameManagers> startSequence;
    /* Upon calling the required manager, the list of managers will be added to
     * IGameManagers
    */
    private void Awake()
    {
            Player = GetComponent<PlayerManager>();
            //Inventory = GetComponent<InventoryManager>();

            startSequence = new List<IGameManagers>();
            //startSequence.Add(Inventory);
            startSequence.Add(Player);

            StartCoroutine(StartupManagers());
    }
    //Runs the startup method for each manager one at a time.
    private IEnumerator StartupManagers()
    {
        foreach (IGameManagers manager in startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManagers manager in startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            //Demonstrates the progress of the managers
            if (numReady > lastReady)
            {
                Debug.Log($"Progress: {numReady}/{numModules}");
                yield return null;
            }
            Debug.Log("All managers started up");
        }
    }
}
