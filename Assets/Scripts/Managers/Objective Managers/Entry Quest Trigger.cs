using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryQuestTrigger : MonoBehaviour
{
    private ObjectiveManager obj;
    private void Start()
    {
        obj = Object.FindAnyObjectByType<ObjectiveManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !ObjectiveManager.entryQuestComplete)
        {
            ObjectiveManager.entryQuestComplete = true;
            obj.completeEntryQuest();
            Destroy(gameObject);
        }
    }
}
