using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    [SerializeField] Transform buildingDemolishButton;

    private HealthSystem _health;
    private BuildingTypeScriptableObject _buildingType;

    private void Awake() {
        _buildingType = GetComponent<BuildingTypeRef>().buildingType;
        _health = GetComponent<HealthSystem>();
        _health.OnDeath += OnDeath;
        _health.SetMaxHealth(_buildingType.maxHealth);
        HideDemolishButton();
    }

    private void OnDeath(object sender, EventArgs e) {
        Debug.Log(_buildingType.nameString + " Died!");
        Destroy(gameObject);
    }

    private void OnMouseEnter() {
        ShowDemolishButton();
    }

    private void OnMouseExit() {
        HideDemolishButton();
    }

    private void ShowDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    private void HideDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
