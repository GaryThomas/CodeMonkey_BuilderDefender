using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUnderConstruction : MonoBehaviour {

    [SerializeField] private SpriteRenderer _image;
    private float _constructionTime;
    private float _constructionTimer = 5f;
    private BuildingTypeScriptableObject _buildingType;
    private BoxCollider2D _box;

    public static BuildingUnderConstruction Create(Vector3 position, BuildingTypeScriptableObject buildingType) {
        // Can't think of a way to cache this (yet)
        Transform prefab = Resources.Load<Transform>("BuildingUnderConstruction");
        Transform xform = Instantiate(prefab, position, Quaternion.identity);
        BuildingUnderConstruction buildingUnderConstruction = xform.GetComponent<BuildingUnderConstruction>();
        buildingUnderConstruction.Setup(buildingType);
        return buildingUnderConstruction;
    }

    private void Awake() {
        _box = GetComponent<BoxCollider2D>();
    }

    private void Setup(BuildingTypeScriptableObject buildingType) {
        _constructionTime = buildingType.constructionTime;
        _constructionTimer = _constructionTime;
        _buildingType = buildingType;
        _image.sprite = buildingType.sprite;
        BoxCollider2D buildingBox = buildingType.prefab.GetComponent<BoxCollider2D>();
        _box.size = buildingBox.size;
        _box.offset = buildingBox.offset;
    }

    private void Update() {
        _constructionTimer -= Time.deltaTime;
        if (_constructionTimer <= 0) {
            Debug.Log("Poof!");
            Destroy(gameObject);
        }
    }

    public float GetConstructionTimerNormalized() {
        return _constructionTimer / _constructionTime;
    }
}
