using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class EndOfGameController : MonoBehaviour {

    private Button playAgainButton;

    private struct ElementId {
        public const string PlayAgainButton = "play-again-button";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        playAgainButton = ui.Q<Button>(
            ElementId.PlayAgainButton
        );
        playAgainButton.clicked += PlayAgain;
    }

    private void OnDisable() {
        playAgainButton.clicked -= PlayAgain;
    }


    private void PlayAgain() {
        GameManager.Instance.Initialize();

        gameObject.SetActive(
            false
        );
    }
}
