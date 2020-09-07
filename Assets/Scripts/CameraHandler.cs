using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float minOrthographicSize = 8;
    [SerializeField] private float maxOrthographicSize = 30;

    private float orthographicSize;
    private float targetOrthographicSize;

    private void Start() {
        orthographicSize = cam.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update() {
        HandleMovement();
        HandleZoom();
    }

    private void HandleZoom() {
        // Zoom in/out with mouse scroll wheel
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cam.m_Lens.OrthographicSize = orthographicSize;
    }

    private void HandleMovement() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, y).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
