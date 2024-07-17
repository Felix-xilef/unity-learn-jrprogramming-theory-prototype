using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private InputActionReference movementAction;

    [SerializeField]
    private float speed;


    private void Update() {
        var newPosition = transform.position + (Vector3) (speed * Time.deltaTime * movementAction.action.ReadValue<Vector2>());

        var lowerBoundary = GameManager.Instance.GameLowerBoundaries;
        var upperBoundary = GameManager.Instance.GameUpperBoundaries;

        if (newPosition.x < lowerBoundary.x) {
            newPosition.x = lowerBoundary.x;

        } else if (newPosition.x > upperBoundary.x) {
            newPosition.x = upperBoundary.x;
        }

        if (newPosition.y < lowerBoundary.y) {
            newPosition.y = lowerBoundary.y;

        } else if (newPosition.y > upperBoundary.y) {
            newPosition.y = upperBoundary.y;
        }

        transform.position = newPosition;
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
