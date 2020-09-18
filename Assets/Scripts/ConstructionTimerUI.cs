using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private BuildingUnderConstruction buildingUnderConstruction;

    private void Update() {
        image.fillAmount = 1f - buildingUnderConstruction.GetConstructionTimerNormalized();
    }
}
