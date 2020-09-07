using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour {
    [SerializeField] Transform resourceUITemplate;
    [SerializeField] float offset = 160f;

    private ResourceTypeListScriptableObject _resourceTypes;
    private Dictionary<ResourceTypeScriptableObject, Transform> _resourceTypeTransforms;

    private void Awake() {
        float pos = -(offset + 20f);
        _resourceTypes = Resources.Load<ResourceTypeListScriptableObject>(typeof(ResourceTypeListScriptableObject).Name);
        if (_resourceTypes == null) {
            Debug.Log("*** Resource Types List not found");
            return;
        }
        // Hide template
        resourceUITemplate.gameObject.SetActive(false);
        resourceUITemplate.Find("selected").gameObject.SetActive(false);
        _resourceTypeTransforms = new Dictionary<ResourceTypeScriptableObject, Transform>();
        foreach (ResourceTypeScriptableObject resource in _resourceTypes.types) {
            Transform resourceTransform = Instantiate(resourceUITemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            _resourceTypeTransforms[resource] = resourceTransform;
            RectTransform xform = resourceTransform.GetComponent<RectTransform>();
            xform.anchoredPosition = new Vector2(pos, 0f);
            pos -= offset;
            resourceTransform.Find("image").GetComponent<Image>().sprite = resource.sprite;
        }
    }

    private void Start() {
        ResourceManager.Instance.OnResourceAmountsChanged += ResourcesChanged;
        UpdateResourceAmounts();
    }

    private void ResourcesChanged(object sender, EventArgs e) {
        UpdateResourceAmounts();
    }

    private void UpdateResourceAmounts() {
        foreach (ResourceTypeScriptableObject resource in _resourceTypes.types) {
            int amount = ResourceManager.Instance.GetResourceAmount(resource);
            _resourceTypeTransforms[resource].Find("text").GetComponent<TextMeshProUGUI>().SetText(amount.ToString());
        }
    }
}
