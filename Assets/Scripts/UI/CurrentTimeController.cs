using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class CurrentTimeController : MonoBehaviour {

    private Label timeText;

    private struct ElementId {
        public const string TimeText = "time-text";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        timeText = ui.Q<Label>(
            ElementId.TimeText
        );

        GameManager.Instance.onCurrentTimeChanged += UpdateText;
    }


    private void UpdateText(float newTime) {
        timeText.text = $"{Mathf.Ceil(newTime)}";
    }
}
