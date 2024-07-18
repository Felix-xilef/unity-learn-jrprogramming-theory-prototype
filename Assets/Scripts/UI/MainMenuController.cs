using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainMenuController : MonoBehaviour {

    private Label bestScoreText;
    private TextField usernameText;
    private Button startGameButton;
    private Button exitButton;

    private struct ElementId {
        public const string BestScoreText = "best-score-text";
        public const string UsernameText = "username-text";
        public const string StartGameButton = "start-game-button";
        public const string ExitButton = "exit-button";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        bestScoreText = ui.Q<Label>(
            ElementId.BestScoreText
        );

        if (GameData.Instance.BestScore != null) {
            UpdateBestScoreText(
                GameData.Instance.BestScore
            );
        }

        GameData.Instance.onBestScoreChanged += UpdateBestScoreText;

        usernameText = ui.Q<TextField>(
            ElementId.UsernameText
        );

        startGameButton = ui.Q<Button>(
            ElementId.StartGameButton
        );
        startGameButton.clicked += StartGame;

        exitButton = ui.Q<Button>(
            ElementId.ExitButton
        );
        exitButton.clicked += ExitGame;
    }

    private void OnDisable() {
        GameData.Instance.onBestScoreChanged -= UpdateBestScoreText;

        startGameButton.clicked -= StartGame;
        exitButton.clicked -= ExitGame;
    }


    private void UpdateBestScoreText(BestScore newValue) {
        bestScoreText.text = $"{newValue.username} - {newValue.score}";
    }

    private void StartGame() {
        GameData.Instance.currentUsername = usernameText.text;

        SceneManager.LoadScene(
            (int) SceneId.MainGame
        );
    }

    private void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
