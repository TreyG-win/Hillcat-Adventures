using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter; //<= reference the plate counter script.
    [SerializeField] private Transform countertopPoint;
    [SerializeField] private Transform plateVisualPrefab; //<= we want the visual mesh of the plate, and not the plate object here.

    private List<GameObject> plateVisualGameObjectList; //<= keep a list of plates availible so we know where to place them.

    private void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start(){
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, countertopPoint); //<=Spawn a plate.

        float plateOffsetY = .1f; //<=amount to offset by
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY * plateVisualGameObjectList.Count,0); //<=update offset based on number of plates.
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);//<=Add plates to list.
    }
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e){
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count -1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
