using System.Collections;
using UnityEngine;

public class PointManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject[] pointPrefabs;


    [Header("Spawn Interval")]

    [SerializeField]
    private float minSpawnInterval;

    [SerializeField]
    private float maxSpawnInterval;


    [Header("Spawn Amount")]

    [SerializeField]
    private float minSpawnAmount;

    [SerializeField]
    private float maxSpawnAmount;


    private void Start() {
        if (GameManager.Instance.GameState == GameState.Running) {
            StartCoroutine(
                SpawnPoints()
            );
        }

        GameManager.Instance.onGameStateChanged += ChangeGameState;
    }


    private void ChangeGameState(GameState newState) {
        if (newState == GameState.Stopped) {
            DestroyCurrentPoints();

        } else {
            StartCoroutine(
                SpawnPoints()
            );
        }
    }


    private void DestroyCurrentPoints() {
        var points = FindObjectsByType<PointController>(
            FindObjectsSortMode.None
        );

        foreach (var point in points) {
            Destroy(
                point.gameObject
            );
        }
    }

    private IEnumerator SpawnPoints() {
        while (GameManager.Instance.GameState == GameState.Running) {
            var amountToSpawn = Random.Range(
                minSpawnAmount,
                maxSpawnAmount
            );

            for (int i = 0; i < amountToSpawn; i++) {
                SpawnRandomPoint();
            }

            yield return new WaitForSeconds(
                Random.Range(
                    minSpawnInterval,
                    maxSpawnInterval
                )
            );
        }
    }

    private void SpawnRandomPoint() {
        Instantiate(
            pointPrefabs[
                Random.Range(
                    0,
                    pointPrefabs.Length
                )
            ],
            GetRandomSpawnPosition(),
            Quaternion.identity
        );
    }

    private Vector3 GetRandomSpawnPosition() {
        return new Vector3(
            Random.Range(
                GameManager.Instance.GameLowerBoundaries.x,
                GameManager.Instance.GameUpperBoundaries.x
            ),
            Random.Range(
                GameManager.Instance.GameLowerBoundaries.y,
                GameManager.Instance.GameUpperBoundaries.y
            ),
            GameManager.Instance.GameUpperBoundaries.z
        );
    }
}
