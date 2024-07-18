using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameData : MonoBehaviour {

    private const string saveFileName = "andorinha-data.json";

    private string saveFilePath;


    public static GameData Instance { get; private set; }


    [DoNotSerialize]
    public string currentUsername;


    public Action<BestScore> onBestScoreChanged;
    public BestScore _bestScore;
    public BestScore BestScore {
        get => _bestScore;
        private set {
            if (_bestScore != value) {
                _bestScore = value;

                onBestScoreChanged?.Invoke(
                    _bestScore
                );
            }
        }
    }


    private void Awake() {
        if (Instance == null) {
            saveFilePath = $"{Application.persistentDataPath}/{saveFileName}";

            LoadData();

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


    private void LoadData() {
        if (File.Exists(saveFilePath)) {
            BestScore = JsonUtility.FromJson<BestScore>(
                File.ReadAllText(
                    saveFilePath
                )
            );

        } else {
            BestScore = new() {
                username = "[no one]",
                score = 0,
            };
        }
    }

    public void RegisterIfBestScore(int score) {
        if (score > BestScore.score) {
            BestScore = new() {
                username = currentUsername,
                score = score,
            };

            SaveData();
        }
    }

    private void SaveData() {
        File.WriteAllText(
            saveFilePath,
            JsonUtility.ToJson(
                BestScore
            )
        );
    }
}
