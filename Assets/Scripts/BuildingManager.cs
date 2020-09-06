using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private BuildingTypeScriptableObject _buildingType;
    private BuildingTypeListScriptableObject _buildingTypes;
    private Camera _cam;

    private void Awake() {
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        if (_buildingTypes == null) {
            Debug.Log("*** Building Types List not found");
            return;
        }
        _buildingType = _buildingTypes.types[0];
    }

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
        // TESTING - select building type
        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("Select building #0");
            _buildingType = _buildingTypes.types[0];
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            Debug.Log("Select building #1");
            _buildingType = _buildingTypes.types[1];
        }
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;

    }
}
