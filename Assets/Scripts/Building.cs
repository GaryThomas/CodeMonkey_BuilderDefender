using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    [SerializeField] Transform buildingDemolishButton;
    [SerializeField] Transform buildingRepairButton;

    private HealthSystem _health;
    private BuildingTypeScriptableObject _buildingType;

    private void Awake() {
        _buildingType = GetComponent<BuildingTypeRef>().buildingType;
        _health = GetComponent<HealthSystem>();
        _health.OnDeath += OnDeath;
        _health.SetMaxHealth(_buildingType.maxHealth);
        _health.OnDamaged += OnDamage;
        _health.OnHealed += OnHealed;
        HideDemolishButton();
        HideRepairButton();
    }

    private void OnHealed(object sender, EventArgs e) {
        if (_health.IsFullHealth()) {
            HideRepairButton();
        }
    }

    private void OnDamage(object sender, EventArgs e) {
        SoundManager.Instance.PlayClip(SoundManager.Instance.clips.BuildingDamaged);
        ShowRepairButton();
    }

    private void OnDeath(object sender, EventArgs e) {
        Debug.Log(_buildingType.nameString + " Died!");
        SoundManager.Instance.PlayClip(SoundManager.Instance.clips.BuildingDestroyed);
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

    private void ShowRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }

    private void HideRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
