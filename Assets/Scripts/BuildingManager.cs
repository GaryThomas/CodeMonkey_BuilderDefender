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
        string errMsg;
        if (_activeBuildingType && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (CanSpawn(_activeBuildingType, Utils.GetMouseWorldPosition(), out errMsg)) {
                if (ResourceManager.Instance.CanAfford(_activeBuildingType.productionCosts)) {
                    ResourceManager.Instance.ApplyCosts(_activeBuildingType.productionCosts);
                    Instantiate(_activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
                } else {
                    TooltipUI.Instance.ShowMsg("Can't afford to build", 2f);
                }
            } else {
                TooltipUI.Instance.ShowMsg(errMsg, 2f);
            }
        }
    }

    public void SelectBuildingType(BuildingTypeScriptableObject buildingType) {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActiveBuildingTypeChangedEventArgs { buildingType = buildingType }
        );
    }

    private bool CanSpawn(BuildingTypeScriptableObject buildingType, Vector3 pos, out string errMsg) {
        BoxCollider2D box = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)pos + box.offset, box.size, 0);
        if (colliders.Length != 0) {
            errMsg = "No resources available here";
            return false;
        }
        // Look to see if there are any other factories of this type too close
        colliders = Physics2D.OverlapCircleAll(pos, buildingType.minSeparation);
        foreach (Collider2D collider in colliders) {
            BuildingTypeRef buildingTypeRef = collider.GetComponent<BuildingTypeRef>();
            if (buildingTypeRef != null) {
                if (buildingTypeRef.buildingType == buildingType) {
                    errMsg = "Sorry, there's another factory too closeby";
                    return false;
                }
            }
        }
        // Make sure there is at least one other building nearby (no isolationists!)
        colliders = Physics2D.OverlapCircleAll(pos, maxSeparation);
        foreach (Collider2D collider in colliders) {
            BuildingTypeRef buildingTypeRef = collider.GetComponent<BuildingTypeRef>();
            if (buildingTypeRef != null) {
                errMsg = "";
                return true;
            }
        }
        errMsg = "Sorry, that's way too far from another building";
        return false;
    }
}
