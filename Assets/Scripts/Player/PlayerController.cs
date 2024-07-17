using UnityEngine;

public class PlayerController : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<PointController>(out var pointController)) {
            GameManager.Instance.CurrentScore += pointController.Value;

            Destroy(
                other.gameObject
            );
        }
    }
}
