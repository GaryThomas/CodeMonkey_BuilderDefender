using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : Singleton<TooltipUI> {
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Vector2 _padding;

    private RectTransform _rect;
    private float _timer;

    public override void Awake() {
        base.Awake();
        _rect = GetComponent<RectTransform>();
        Hide();
    }

    private void Update() {
        if ((_timer > 0)) {
            _timer -= Time.deltaTime;
            if (_timer <= 0) {
                Hide();
                return;
            }
        }
        Vector2 anchoredPosition = Input.mousePosition / _canvas.localScale.x;
        if (anchoredPosition.x + _rect.rect.width > _canvas.rect.width) {
            anchoredPosition.x = _canvas.rect.width - _rect.rect.width;
        }
        if (anchoredPosition.y + _rect.rect.height > _canvas.rect.height) {
            anchoredPosition.y = _canvas.rect.height - _rect.rect.height;
        }
        _rect.anchoredPosition = anchoredPosition;
    }

    private void SetText(string msg) {
        _text.SetText(msg);
        _text.ForceMeshUpdate();
        _rect.sizeDelta = _text.GetRenderedValues(false) + _padding;
    }

    public void ShowMsg(string msg, float timeout = -1) {
        _timer = timeout;
        gameObject.SetActive(true);
        SetText(msg);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
