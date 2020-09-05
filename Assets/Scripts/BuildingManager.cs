using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [SerializeField] private Transform pfWoodHarvester;

    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(pfWoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;

    }
}
