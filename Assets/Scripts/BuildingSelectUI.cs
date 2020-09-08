using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour {
    [SerializeField] private Transform buildingSelect;
    [SerializeField] private float offset = 180f;
    [SerializeField] private float startingPos = 70f;
    [SerializeField] private Sprite arrowSprite;

    private BuildingTypeListScriptableObject _buildingTypes;
    private Dictionary<BuildingTypeScriptableObject, Transform> _buildingButtons;
    private Transform _arrowButton;

    private void Awake() {
        float pos = startingPos;
        _buildingButtons = new Dictionary<BuildingTypeScriptableObject, Transform>();
        buildingSelect.gameObject.SetActive(false);
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        // Set up special "nothing selected" button
        _arrowButton = GenButton(pos, null, true);
        foreach (BuildingTypeScriptableObject buildingType in _buildingTypes.types) {
            pos += offset;
            GenButton(pos, buildingType);
        }
        UpdateSelectedBuildingType(null);
    }

    private Transform GenButton(float pos, BuildingTypeScriptableObject buildingType, bool isArrow = false) {
        Transform xform = Instantiate(buildingSelect, transform);
        xform.gameObject.SetActive(true);
        xform.Find("image").GetComponent<Image>().sprite = buildingType != null ? buildingType.sprite : arrowSprite;
        // Debug.Log("Set up button for " + (!isArrow ? buildingType.nameString : "Arrow"));
        if (!isArrow) {
            xform.GetComponent<Button>().onClick.AddListener(() => {
                // Debug.Log("Click = " + buildingType.ToString());
                // Debug.Log("Clicked on " + buildingType != null ? buildingType.nameString : "Arrow");
                BuildingManager.Instance.SelectBuildingType(buildingType);
                UpdateSelectedBuildingType(buildingType);
            });
            _buildingButtons[buildingType] = xform;
        } else {
            xform.GetComponent<Button>().onClick.AddListener(() => {
                // Debug.Log("Clicked on Arrow");
                BuildingManager.Instance.SelectBuildingType(null);
                UpdateSelectedBuildingType(null);
            });
        }
        RectTransform rect = xform.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(pos, 0, 0);
        return xform;
    }

    private void UpdateSelectedBuildingType(BuildingTypeScriptableObject current) {
        _arrowButton.Find("selected").gameObject.SetActive(true);
        foreach (BuildingTypeScriptableObject buildingType in _buildingButtons.Keys) {
            Transform xform = _buildingButtons[buildingType];
            xform.Find("selected").gameObject.SetActive(buildingType == current);
            if (buildingType == current) {
                _arrowButton.Find("selected").gameObject.SetActive(false);
            }
        }
    }
}
