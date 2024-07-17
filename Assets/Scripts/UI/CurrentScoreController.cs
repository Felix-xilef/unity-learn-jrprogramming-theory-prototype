using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class CurrentScoreController : MonoBehaviour {

    private Label scoreText;

    private struct ElementId {
        public const string ScoreText = "score-text";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        scoreText = ui.Q<Label>(
            ElementId.ScoreText
        );

        GameManager.Instance.onCurrentScoreChanged += UpdateText;
    }


    private void UpdateText(int newScore) {
        scoreText.text = $"{newScore}";
    }
}
