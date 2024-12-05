using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Works alongside the Loader class to notify it when the current level has updated the screen
 * */
public class LoaderCallback : MonoBehaviour {

    private bool firstUpdate = true;

    private void Update() {
        if (firstUpdate) {
            firstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
