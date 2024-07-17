using UnityEngine;

public abstract class PointController : MonoBehaviour {

    [SerializeField]
    private int value;
    public int Value => value;


    protected virtual void Start() {
        var pointRigidbody = GetComponent<Rigidbody>();

        pointRigidbody.velocity = new Vector3(
            0,
            0,
            -GameManager.Instance.GameVelocity
        );
    }

    protected virtual void Update() {
        if (transform.position.z < GameManager.Instance.GameLowerBoundaries.z) {
            Destroy(
                gameObject
            );
        }
    }
}
