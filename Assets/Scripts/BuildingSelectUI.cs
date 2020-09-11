using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour {
    [SerializeField] private Transform buildingSelect;
    [SerializeField] private float offset = 180f;
    [SerializeField] private float startingPos = 70f;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] List<BuildingTypeScriptableObject> ignoredBuildings;

    private BuildingTypeListScriptableObject _buildingTypes;
    private Dictionary<BuildingTypeScriptableObject, Transform> _buildingButtons;
    private Transform _arrowButton;

    private void Awake() {
        float pos = startingPos;
        _buildingButtons = new Dictionary<BuildingTypeScriptableObject, Transform>();
        buildingSelect.gameObject.SetActive(false);
        _buildingTypes = Resources.Load<BuildingTypeListScriptableObject>(typeof(BuildingTypeListScriptableObject).Name);
        // Set up special "nothing selected" button
        _arrowButton = GenButton(pos, null);
        foreach (BuildingTypeScriptableObject buildingType in _buildingTypes.types) {
            if (ignoredBuildings.Contains(buildingType)) { continue; }
            pos += offset;
            GenButton(pos, buildingType);
        }
        UpdateSelectedBuildingType(null);
    }

    private Transform GenButton(float pos, BuildingTypeScriptableObject buildingType) {
        Transform xform = Instantiate(buildingSelect, transform);
        xform.gameObject.SetActive(true);
        xform.Find("image").GetComponent<Image>().sprite = buildingType != null ? buildingType.sprite : arrowSprite;
        xform.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SelectBuildingType(buildingType);
            UpdateSelectedBuildingType(buildingType);
        });
        if (buildingType != null) {
            _buildingButtons[buildingType] = xform;
        }
        MouseEnterExitEvents mouseEnterExitEvents = xform.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs eventArgs) => {
            if (buildingType != null) {
                TooltipUI.Instance.ShowMsg(buildingType.nameString + "\n" + buildingType.GetProductionCostsString());
            } else {
                TooltipUI.Instance.ShowMsg("Arrow");
            }
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs eventArgs) => {
            TooltipUI.Instance.Hide();
        };
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
