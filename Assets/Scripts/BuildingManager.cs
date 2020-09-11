using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager> {

    [SerializeField] private float maxSeparation = 25f;

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
        if (_activeBuildingType && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (CanSpawn(_activeBuildingType, Utils.GetMouseWorldPosition())) {
                if (ResourceManager.Instance.CanAfford(_activeBuildingType.productionCosts)) {
                    ResourceManager.Instance.ApplyCosts(_activeBuildingType.productionCosts);
                    Instantiate(_activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
                }
            }
        }
    }

    public void SelectBuildingType(BuildingTypeScriptableObject buildingType) {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActiveBuildingTypeChangedEventArgs { buildingType = buildingType }
        );
    }

    private bool CanSpawn(BuildingTypeScriptableObject buildingType, Vector3 pos) {
        BoxCollider2D box = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)pos + box.offset, box.size, 0);
        if (colliders.Length != 0) {
            return false;
        }
        // Look to see if there are any other factories of this type too close
        colliders = Physics2D.OverlapCircleAll(pos, buildingType.minSeparation);
        foreach (Collider2D collider in colliders) {
            BuildingTypeRef buildingTypeRef = collider.GetComponent<BuildingTypeRef>();
            if (buildingTypeRef != null) {
                if (buildingTypeRef.buildingType == buildingType) {
                    // Sorry, there's another factory too close
                    return false;
                }
            }
        }
        // Make sure there is at least one other building nearby (no isolationists!)
        colliders = Physics2D.OverlapCircleAll(pos, maxSeparation);
        foreach (Collider2D collider in colliders) {
            BuildingTypeRef buildingTypeRef = collider.GetComponent<BuildingTypeRef>();
            if (buildingTypeRef != null) {
                return true;
            }
        }
        return false;
    }
}
