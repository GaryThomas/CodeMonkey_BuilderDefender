using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    private static Camera _cam;

    public static Vector3 GetMouseWorldPosition() {
        if (_cam == null) {
            _cam = Camera.main;
        }
        Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}
