using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameData : MonoBehaviour {

    public static GameData Instance { get; private set; }


    public BestScore bestScore { get; private set; }

    public string currentUsername;


    private void Awake() {
        if (Instance == null) {
            Instance = this;

            DontDestroyOnLoad(
                gameObject
            );

        } else {
            Destroy(
                gameObject
            );
        }
    }


    public void RegisterIfBestScore(int score) {
        if (score > bestScore.score) {
            bestScore = new() {
                username = currentUsername,
                score = score,
            };
        }
    }
}
