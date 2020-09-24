using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    [SerializeField] private Transform buildingDemolishButton;
    [SerializeField] private Transform buildingRepairButton;
    [SerializeField] private Transform buildingDestroyedParticles;
    [SerializeField] private float shakeOnDamageAmount = 5f;
    [SerializeField] private float shakeOnDamageDuration = 0.1f;
    [SerializeField] private float shakeOnDeathAmount = 15f;
    [SerializeField] private float shakeOnDeathDuration = 0.4f;
    [SerializeField] private float aberationOnDamageLevel = 1f;
    [SerializeField] private float aberationOnDeathLevel = 1f;

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
        CameraShake.Instance.Shake(shakeOnDamageAmount, shakeOnDamageDuration);
        ChromaticAberationEffect.Instance.SetWeight(aberationOnDamageLevel);
        ShowRepairButton();
    }

    private void OnDeath(object sender, EventArgs e) {
        Debug.Log(_buildingType.nameString + " Died!");
        SoundManager.Instance.PlayClip(SoundManager.Instance.clips.BuildingDestroyed);
        Instantiate(buildingDestroyedParticles, transform.position, Quaternion.identity);
        CameraShake.Instance.Shake(shakeOnDeathAmount, shakeOnDeathDuration);
        ChromaticAberationEffect.Instance.SetWeight(aberationOnDeathLevel);
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
