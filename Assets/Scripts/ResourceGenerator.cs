using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;

    private void Awake() {
        _resourceGeneratorData = GetComponent<BuildingTypeRef>().buildingType.resourceGeneratorData;
        _timer = 0f;
        _timerMax = _resourceGeneratorData.timerMax;
    }

    private void Start() {
        int nearbyResources = GetNearbyResources(_resourceGeneratorData, transform.position);
        if (nearbyResources == 0) {
            Debug.Log("No nearby resources");
            enabled = false;  // Disable resource generation
        } else {
            // Adjust production rate based on number of nearby resources (more == faster rate)
            _timerMax = (_resourceGeneratorData.timerMax / 2f) +
                _resourceGeneratorData.timerMax *
                (1 - ((float)nearbyResources / _resourceGeneratorData.maxResourceAmount));
        }
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0f) {
            _timer += _timerMax;
            ResourceManager.Instance.AddResources(_resourceGeneratorData.resourceType, 1);
        }
    }

    public static int GetNearbyResources(ResourceGeneratorData resourceGeneratorData, Vector3 position) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResources = 0;
        foreach (Collider2D collider in colliders) {
            ResourceNode node = collider.GetComponent<ResourceNode>();
            if (node != null && node.resourceType == resourceGeneratorData.resourceType) {
                nearbyResources++;
            }
        }
        nearbyResources = Mathf.Clamp(nearbyResources, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResources;
    }

    public ResourceGeneratorData GetResourceGeneratorData() {
        return _resourceGeneratorData;
    }

    public float GetTimerNormalized() {
        return _timer / _timerMax;
    }

    public float GetRate() {
        return 1 / _timerMax;
    }
}
