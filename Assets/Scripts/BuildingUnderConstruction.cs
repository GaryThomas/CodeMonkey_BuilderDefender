﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUnderConstruction : MonoBehaviour {

    [SerializeField] private SpriteRenderer _image;
    private float _constructionTime;
    private float _constructionTimer = 5f;
    private BuildingTypeScriptableObject _buildingType;
    private BoxCollider2D _box;
    private BuildingTypeRef _ref;
    private Vector3 _position;

    public static BuildingUnderConstruction Create(Vector3 position, BuildingTypeScriptableObject buildingType) {
        // Can't think of a way to cache this (yet)
        Transform prefab = Resources.Load<Transform>("BuildingUnderConstruction");
        Transform xform = Instantiate(prefab, position, Quaternion.identity);
        BuildingUnderConstruction buildingUnderConstruction = xform.GetComponent<BuildingUnderConstruction>();
        buildingUnderConstruction.Setup(position, buildingType);
        return buildingUnderConstruction;
    }

    private void Awake() {
        _box = GetComponent<BoxCollider2D>();
        _ref = GetComponent<BuildingTypeRef>();
    }

    private void Setup(Vector3 position, BuildingTypeScriptableObject buildingType) {
        _constructionTime = buildingType.constructionTime;
        _constructionTimer = _constructionTime;
        _buildingType = buildingType;
        _position = position;
        _image.sprite = buildingType.sprite;
        BoxCollider2D buildingBox = buildingType.prefab.GetComponent<BoxCollider2D>();
        _box.size = buildingBox.size;
        _box.offset = buildingBox.offset;
        _ref.buildingType = buildingType;
    }

    private void Update() {
        _constructionTimer -= Time.deltaTime;
        if (_constructionTimer <= 0) {
            Instantiate(_buildingType.prefab, _position, Quaternion.identity);
            Debug.Log("Poof!" + transform.position + ", " + _position);
            Destroy(gameObject);
        }
    }

    public float GetConstructionTimerNormalized() {
        return _constructionTimer / _constructionTime;
    }
}