using UnityEngine;
using UnityEngine.InputSystem;

public class PatternGenerator : MonoBehaviour
{
    [Header("Bullets Settings")]
    public int numberOfBullets;
    public float angleStep;
    public float bulletSpeed;
    public float firingSpeed;
    public bool shooting;
    public GameObject BulletPrefab;

    [Header("Private Bullets Settings")]
    private Vector3 startPoint;
    private const float radius = 1f;
    private float angle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        shooting = !shooting;
        if (shooting)
        {
            InvokeRepeating("SpawnBullets", 0f, firingSpeed);
        }
        else
        {
            CancelInvoke("SpawnBullets");
        }
    }
    
    public void SpawnBullets()
    {
        startPoint = transform.position;
        float angleSpacing = 360f / numberOfBullets;                                
        for (int i = 0; i < numberOfBullets; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Sin(((angle + i * angleSpacing) * Mathf.PI) / 180f) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Cos(((angle + i * angleSpacing) * Mathf.PI) / 180f) * radius;

            Vector3 newPositionVector = new Vector3(bulletDirXPosition, 0, bulletDirYPosition);
            Vector3 bulletDirection= (newPositionVector - startPoint).normalized;

            Bullet newBullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
            newBullet.transform.position = startPoint;

            // pentru alte traiectorii
            // newBullet.transform.rotation = transform.rotation * new Quaternion(x,y,z,w); 
            newBullet.transform.rotation = transform.rotation * Quaternion.identity;
        
            newBullet.SetMoveSpeed(bulletSpeed);
            newBullet.SetMoveDirection(bulletDirection);
        
            // angle += 360f / numberOfBullets;

            if (angle >= 360f)
                angle -= 360f;
        }

        angle += angleStep;
    }

    public void OnFiringSpeedValueChanged() {
        if (shooting)
        {
            CancelInvoke("SpawnBullets");
            InvokeRepeating("SpawnBullets", 0.05f, firingSpeed);
        }
    }
}
