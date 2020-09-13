using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour {
    [SerializeField] ResourceGenerator resourceGenerator;

    private Transform _bar;

    private void Start() {
        if (resourceGenerator) {
            ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
            _bar = transform.Find("bar");
            transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetRate().ToString("F1"));
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        _bar.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
