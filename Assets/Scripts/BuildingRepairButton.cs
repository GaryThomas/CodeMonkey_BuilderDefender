using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private Building building;
    [SerializeField] private ResourceTypeScriptableObject goldResource;

    private void Awake() {
        ResourceAmount[] costs = new ResourceAmount[] {
            new ResourceAmount { resource = goldResource, amount = 0 }
            };
        button.onClick.AddListener(() => {
            HealthSystem health = building.GetComponent<HealthSystem>();
            int missingHealth = health.GetMaxHealth() - health.GetHealth();
            costs[0].amount = missingHealth / 2;
            if (ResourceManager.Instance.CanAfford(costs)) {
                health.HealFully();
                ResourceManager.Instance.ApplyCosts(costs);
            } else {
                TooltipUI.Instance.ShowMsg("Cannot afford repairs", 2f);
            }
        });
    }
}
