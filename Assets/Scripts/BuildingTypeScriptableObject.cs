using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeScriptableObject : ScriptableObject {
    public string nameString;
    public Transform prefab;
    public Sprite sprite;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public float minSeparation;
    public ResourceAmount[] productionCosts;
    public int maxHealth;

    public string GetProductionCostsString() {
        string str = "";
        bool first = true;
        foreach (ResourceAmount cost in productionCosts) {
            str += (first ? "" : ", ") + cost.resource.nameString + ": " + cost.amount;
            first = false;
        }
        return str;
    }
}
