using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private InputActionReference movementAction;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float smoothTime;


    private Rigidbody playerRigidbody;

    private Vector2 currentSmoothDampVelocity;


    private Vector3 initialPosition;
    private Quaternion initialRotation;


    private void Start() {
        playerRigidbody = GetComponent<Rigidbody>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        GameManager.Instance.onGameStateChanged += GameStateChanged;
    }

    private void OnDestroy() {
        if (GameManager.Instance != null) {
            GameManager.Instance.onGameStateChanged -= GameStateChanged;
        }
    }

    private void Update() {
        if (GameManager.Instance.GameState == GameState.Running) {
            CalculateNewVelocity();

            CheckBoundaries();

            RotateByVelocity();
        }
    }


    private void CalculateNewVelocity() {
        var newVelocity = speed * movementAction.action.ReadValue<Vector2>();

        newVelocity = Vector2.SmoothDamp(
            playerRigidbody.velocity,
            newVelocity,
            ref currentSmoothDampVelocity,
            smoothTime
        );

        playerRigidbody.velocity = newVelocity;
    }

    private void CheckBoundaries() {
        var lowerBoundaries = GameManager.Instance.GameLowerBoundaries;
        var upperBoundaries = GameManager.Instance.GameUpperBoundaries;

        var position = transform.position;
        var velocity = playerRigidbody.velocity;

        if (position.x < lowerBoundaries.x) {
            position.x = lowerBoundaries.x;
            velocity.x = 0;

        } else if (position.x > upperBoundaries.x) {
            position.x = upperBoundaries.x;
            velocity.x = 0;
        }

        if (position.y < lowerBoundaries.y) {
            position.y = lowerBoundaries.y;
            velocity.y = 0;

        } else if (position.y > upperBoundaries.y) {
            position.y = upperBoundaries.y;
            velocity.y = 0;
        }

        transform.position = position;
        playerRigidbody.velocity = velocity;
    }

    private void RotateByVelocity() {
        var direction = Quaternion.LookRotation(
            playerRigidbody.velocity + (Vector3.forward * speed)
        );

        var rotation = Quaternion.Slerp(
            transform.rotation,
            direction,
            turnSpeed * Time.deltaTime
        );

        playerRigidbody.MoveRotation(
            rotation
        );
    }


    private void GameStateChanged(GameState newState) {
        if (newState == GameState.Stopped) {
            playerRigidbody.velocity = Vector3.zero;

            transform.SetPositionAndRotation(
                initialPosition,
                initialRotation
            );
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<PointController>(out var pointController)) {
            GameManager.Instance.CurrentScore += pointController.Value;

            Destroy(
                other.gameObject
            );
        }
    }
}
