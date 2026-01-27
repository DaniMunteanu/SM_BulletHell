using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    public Vector3 startPosition;
    private float moveSpeed = 5f;
    public float radius = 2f;

    public float horizontalAngle = 0;
    public float horizontalAngleSpacing = 0;
    public float verticalAngle = 0;

    public float wiggleSpeed = 0;
    public float horizontalWiggleSize = 0;
    public float verticalWiggleSize = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalWiggleModifier = horizontalWiggleSize * Mathf.Sin(wiggleSpeed * Time.time);
        float verticalWiggleModifier = verticalWiggleSize * Mathf.Cos(wiggleSpeed * Time.time);

        float bulletDirXPosition = startPosition.x + Mathf.Cos(((horizontalAngle + horizontalAngleSpacing + horizontalWiggleModifier) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;
        float bulletDirYPosition = startPosition.y + Mathf.Sin(verticalAngle + verticalWiggleModifier) * radius;
        float bulletDirZPosition = startPosition.z + Mathf.Sin(((horizontalAngle + horizontalAngleSpacing + horizontalWiggleModifier) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;

        Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, bulletDirZPosition);
        moveDirection = (newPositionVector - startPosition).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(startPosition, transform.position) >= radius)
            Destroy(gameObject);
    }

    public void SetMoveDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
