using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager> {

    private BuildingTypeScriptableObject _activeBuildingType;
    private BuildingTypeListScriptableObject _buildingTypes;
    private Camera _cam;

    public override void Awake() {
        base.Awake();
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        if (_buildingTypes == null) {
            Debug.Log("*** Building Types List not found");
            return;
        }
        _activeBuildingType = null;
    }

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType) {
            Instantiate(_activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    public void SelectBuildingType(BuildingTypeScriptableObject buildingType) {
        _activeBuildingType = buildingType;
    }
}
