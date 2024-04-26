using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The interface that the other data managers will implement.
//This script does not need to be attached to any objects.
public interface IGameManagers
{
    //Gets the status of the manager
    ManagerStatus status { get; }

    //Initializes the manager.
    void Startup();
}
