using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour {
    [SerializeField] private float precision = 5f;
    [SerializeField] private bool fixedPos = false;

    private SpriteRenderer _sr;

    private void Awake() {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Select sprite order in layer based on Y position
    private void LateUpdate() {
        _sr.sortingOrder = (int)(-transform.position.y * precision);
        if (fixedPos) {
            // This sprite/object will never move so disable this script [SCARY]
            Destroy(this);
        }
    }
}
