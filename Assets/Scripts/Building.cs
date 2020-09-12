using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    private HealthSystem _health;
    private BuildingTypeScriptableObject _buildingType;

    private void Awake() {
        _buildingType = GetComponent<BuildingTypeRef>().buildingType;
        _health = GetComponent<HealthSystem>();
        _health.OnDeath += OnDeath;
        _health.SetMaxHealth(_buildingType.maxHealth);
    }

    private void OnDeath(object sender, EventArgs e) {
        Debug.Log("Died");
        Destroy(gameObject);
    }
}
