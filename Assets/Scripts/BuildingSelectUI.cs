using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour {
    [SerializeField] private Transform buildingSelect;
    [SerializeField] private float offset = 180f;
    [SerializeField] private float pos = 70f;

    private BuildingTypeListScriptableObject _buildingTypes;
    private Dictionary<BuildingTypeScriptableObject, Transform> _buildingButtons;

    private void Awake() {
        _buildingButtons = new Dictionary<BuildingTypeScriptableObject, Transform>();
        buildingSelect.gameObject.SetActive(false);
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        foreach (BuildingTypeScriptableObject buildingType in _buildingTypes.types) {
            Transform xform = Instantiate(buildingSelect, transform);
            xform.gameObject.SetActive(true);
            xform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            xform.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("Clicked on " + buildingType.nameString);
                BuildingManager.Instance.SelectBuildingType(buildingType);
                UpdateSelectedBuildingType(buildingType);
            });
            // xform.Find("selected").gameObject.SetActive(false);
            _buildingButtons[buildingType] = xform;
            RectTransform rect = xform.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(pos, 0, 0);
            pos += offset;
        }
        UpdateSelectedBuildingType(null);
    }

    private void UpdateSelectedBuildingType(BuildingTypeScriptableObject current) {
        foreach (BuildingTypeScriptableObject buildingType in _buildingButtons.Keys) {
            Transform xform = _buildingButtons[buildingType];
            xform.Find("selected").gameObject.SetActive(buildingType == current);
        }
    }
}
