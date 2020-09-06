using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    private Dictionary<ResourceTypeScriptableObject, int> _resourceAmounts;
    private ResourceTypeListScriptableObject _resourceTypes;

    private void Awake() {
        _resourceTypes = Resources.Load<ResourceTypeListScriptableObject>(typeof(ResourceTypeListScriptableObject).Name);
        if (_resourceTypes == null) {
            Debug.Log("*** Resource Types List not found");
            return;
        }
        _resourceAmounts = new Dictionary<ResourceTypeScriptableObject, int>();
        foreach (ResourceTypeScriptableObject resource in _resourceTypes.types) {
            _resourceAmounts[resource] = 0;
            Debug.Log(resource.nameString);
        }
    }

    // DEBUG
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            AddResources(_resourceTypes.types[1], 2);
            ShowResourceAmounts();
        }
    }

    // DEBUG
    private void ShowResourceAmounts() {
        foreach (ResourceTypeScriptableObject resource in _resourceTypes.types) {
            Debug.Log(resource.nameString + ": " + _resourceAmounts[resource]);
        }
    }

    public void AddResources(ResourceTypeScriptableObject resource, int amount) {
        _resourceAmounts[resource] += amount;
    }
}
