using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;

    void Start() {
        cam1.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Q) && (cam1.enabled == true || cam3.enabled == true)) {
        cam1.enabled = false;
        cam2.enabled = true;
        cam3.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && (cam2.enabled == true || cam1.enabled == true)) {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && (cam2.enabled == true || cam3.enabled == true)) {
        cam1.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
        }
    }
}