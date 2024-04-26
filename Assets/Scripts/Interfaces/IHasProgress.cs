using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*creates an event handler that multiple objects can use to update the progress bar*/
public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs{
        public float progressNormalized;
    }
}
