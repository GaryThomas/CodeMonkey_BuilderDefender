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

    public static Vector3 GetRandomDir() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float VectorAngle(Vector3 dir) {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
