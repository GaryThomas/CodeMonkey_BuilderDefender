using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {
    [SerializeField] GameObject ghostSprite;

    private void Awake() {
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += ActiveBuildingTypeChanged;
    }

    private void ActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        // Debug.Log("Change building type to " + e.buildingType.nameString);
        if (e.buildingType == null) {
            Hide();
        } else {
            Show(e.buildingType.sprite);
        }
    }

    private void Update() {
        transform.position = Utils.GetMouseWorldPosition();
    }

    public void Show(Sprite sprite) {
        ghostSprite.SetActive(true);
        ghostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Hide() {
        ghostSprite.SetActive(false);
    }
}
