using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    private BuildingTypeScriptableObject _buildingType;
    private float _timer;
    private float _timerMax;

    private void Awake() {
        _buildingType = GetComponent<BuildingTypeRef>().buildingType;
        _timer = 0f;
        _timerMax = _buildingType.resourceGeneratorData.timerMax;
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0f) {
            _timer += _timerMax;
            Debug.Log("Ding! " + _buildingType.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResources(_buildingType.resourceGeneratorData.resourceType, 1);
        }
    }
}
