using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] Building building;

    private void Awake() {
        button.onClick.AddListener(() => {
            BuildingTypeScriptableObject buildingType;
            buildingType = building.GetComponent<BuildingTypeRef>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.productionCosts) {
                ResourceManager.Instance.AddResources(resourceAmount.resource, Mathf.FloorToInt(resourceAmount.amount * 0.60f));
            }
            Destroy(building.gameObject);
        });
    }
}
