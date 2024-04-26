using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter; //<=there needs to be a reference to the stove counter object.
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start(){
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;//<=Begin listening for the event from StoveCounter. Pass to the On State Changed method when an event is heard.
    }

    /*On State Changed method that performs actions when the stove counter event is heard. 
    This takes in the stove object, and the generic arguments (args) passed through the 
    OnStateChangedEventArgs class. In this case, the current state of our stove object. 
    So the value of e is the Class Cbject*/
    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e){
        /*because we passed out stove object and the OnStateChangedEventArgs class, we have access to the 
        stove counter's enums and the current state of the stove that was sent by the OnStateChangedEventArgs
        class. We can now compare the current state with known states in the enum to perform whatever action 
        we wsnt on a state change.*/
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried; //<= boolean to compare the states we want.
        stoveOnGameObject.SetActive(showVisual); //<=Sets the defined stove visual.
        particlesGameObject.SetActive(showVisual); //<=Sets the defined particles visual.

    }

}
