using System;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour {

    [SerializeField]
    private Vector3 gameLowerBoundaries;
    public Vector3 GameLowerBoundaries => gameLowerBoundaries;

    [SerializeField]
    private Vector3 gameUpperBoundaries;
    public Vector3 GameUpperBoundaries => gameUpperBoundaries;

    [SerializeField]
    private float gameVelocity;
    public float GameVelocity => gameVelocity;


    [SerializeField]
    private float gameDuration;


    [SerializeField]
    private GameObject endOfGameView;


    public Action<GameState> onGameStateChanged;
    private GameState _gameState = GameState.Stopped;
    public GameState GameState {
        get => _gameState;
        private set {
            if (_gameState != value) {
                _gameState = value;

                onGameStateChanged?.Invoke(
                    _gameState
                );
            }
        }
    }


    public Action<int> onCurrentScoreChanged;
    private int _currentScore = 0;
    public int CurrentScore {
        get => _currentScore;
        set {
            if (_currentScore != value) {
                _currentScore = value;

                onCurrentScoreChanged?.Invoke(
                    _currentScore
                );
            }
        }
    }


    public Action<float> onCurrentTimeChanged;
    private float _currentTime;
    public float CurrentTime {
        get => _currentTime;
        private set {
            if (_currentTime != value) {
                _currentTime = value;

                onCurrentTimeChanged?.Invoke(
                    _currentTime
                );
            }
        }
    }


    public static GameManager Instance { get; private set; }


    private void Awake() {
        if (Instance == null) {
            Instance = this;

            Initialize();

        } else {
            Destroy(
                gameObject
            );
        }
    }

    private void OnDestroy() {
        Instance = null;
    }

    private void Update() {
        if (GameState == GameState.Running) {
            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0) {
                GameState = GameState.Stopped;

                endOfGameView.SetActive(
                    true
                );

                if (GameData.Instance != null) {
                    GameData.Instance.RegisterIfBestScore(
                        CurrentScore
                    );
                }
            }
        }
    }


    public void Initialize() {
        CurrentScore = 0;
        CurrentTime = gameDuration;
        GameState = GameState.Running;
    }
}
