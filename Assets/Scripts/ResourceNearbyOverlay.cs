using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour {
    private ResourceGeneratorData _resourceGeneratorData;
    private TextMeshPro _text;

    private void Awake() {
        Hide();
    }

    private void Update() {
        int nearbyResources = ResourceGenerator.GetNearbyResources(_resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResources / _resourceGeneratorData.maxResourceAmount * 100f);
        _text.SetText(percent.ToString() + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData) {
        _resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = _resourceGeneratorData.resourceType.sprite;
        _text = transform.Find("text").GetComponent<TextMeshPro>();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
