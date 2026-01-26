using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    public Vector3 startPosition;
    private float moveSpeed = 5f;
    public float radius = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
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
