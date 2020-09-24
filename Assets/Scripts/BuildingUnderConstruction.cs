using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUnderConstruction : MonoBehaviour {

    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private Transform _buildingConstructionParticles;

    private float _constructionTime;
    private float _constructionTimer = 5f;
    private BuildingTypeScriptableObject _buildingType;
    private BoxCollider2D _box;
    private Material _constructionMaterial;

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
        _constructionMaterial = _image.material;
    }

    private void Setup(BuildingTypeScriptableObject buildingType) {
        _constructionTime = buildingType.constructionTime;
        _constructionTimer = _constructionTime;
        _buildingType = buildingType;
        _image.sprite = buildingType.sprite;
        BoxCollider2D buildingBox = buildingType.prefab.GetComponent<BoxCollider2D>();
        _box.size = buildingBox.size;
        _box.offset = buildingBox.offset;
        Instantiate(_buildingConstructionParticles, transform.position, Quaternion.identity);
    }

    private void Update() {
        _constructionTimer -= Time.deltaTime;
        _constructionMaterial.SetFloat("_Progress", 1 - GetConstructionTimerNormalized());
        if (_constructionTimer <= 0) {
            Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
            Instantiate(_buildingConstructionParticles, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayClip(SoundManager.Instance.clips.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    public float GetConstructionTimerNormalized() {
        return _constructionTimer / _constructionTime;
    }
}
