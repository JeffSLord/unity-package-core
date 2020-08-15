using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public delegate void SelectDelegate(GameObject selector, int option = 0);
    public delegate void DeselectDelegate(GameObject selector);
    public SelectDelegate select0Delegate;
    public SelectDelegate select1Delegate;
    public DeselectDelegate deselectDelegate;

    public void Select0(GameObject selector, int option = 0) {
        select0Delegate(selector, option);
    }
    public void Select1(GameObject selector, int option = 0) {
        select1Delegate(selector, option);
    }
    public void Deselect(GameObject selector) {
        deselectDelegate(selector);
    }
}