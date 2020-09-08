using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager> {

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
        public BuildingTypeScriptableObject buildingType;
    }

    private BuildingTypeScriptableObject _activeBuildingType;
    private BuildingTypeListScriptableObject _buildingTypes;

    public override void Awake() {
        base.Awake();
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        if (_buildingTypes == null) {
            Debug.Log("*** Building Types List not found");
            return;
        }
        _activeBuildingType = null;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType) {
            Instantiate(_activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    public void SelectBuildingType(BuildingTypeScriptableObject buildingType) {
        _activeBuildingType = buildingType;
        Debug.Log("BM Select building " + buildingType.nameString + ", handler: " + OnActiveBuildingTypeChanged);
        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActiveBuildingTypeChangedEventArgs { buildingType = buildingType }
        );
    }
}
