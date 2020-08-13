using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public delegate void SelectDelegate(GameObject selector, int option = 0);
    public SelectDelegate selectDelegate;

    void Awake() {
        selectDelegate = DefaultDeselct;
    }
    private void DefaultSelect(GameObject selector, int option = 0) {

    }
    private void DefaultDeselct(GameObject selector, int option = 0) {

    }
    public void Select1(GameObject selector, int option = 0) {
        selectDelegate(selector, option);
    }
    public void Select2(GameObject selector, int option = 0) {

    }
    public void Deselect() {

    }

    public static void PlayerControlerToResource() {

    }
}