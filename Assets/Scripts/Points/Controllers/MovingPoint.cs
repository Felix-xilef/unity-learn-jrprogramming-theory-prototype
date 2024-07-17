using UnityEngine;

public class MovingPointController : PointController {

    [SerializeField]
    private float movingVelocity;


    private Vector3 currentMovingDirection;


    protected override void Start() {
        base.Start();

        currentMovingDirection = new Vector3(
            Random.Range(-1, 1),
            Random.Range(-1, 1),
            0
        );
    }

    protected override void Update() {
        base.Update();

        transform.position += movingVelocity * Time.deltaTime * currentMovingDirection;

        if (
            transform.position.x < GameManager.Instance.GameLowerBoundaries.x ||
            transform.position.x > GameManager.Instance.GameUpperBoundaries.x
        ) {
            currentMovingDirection.x *= -1;
        }

        if (
            transform.position.y < GameManager.Instance.GameLowerBoundaries.y ||
            transform.position.y > GameManager.Instance.GameUpperBoundaries.y
        ) {
            currentMovingDirection.y *= -1;
        }
    }
}
