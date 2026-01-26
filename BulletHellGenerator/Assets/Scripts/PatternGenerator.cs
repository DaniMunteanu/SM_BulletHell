using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PatternGenerator : MonoBehaviour
{
    [Header("Bullets Settings")]
    public int numberOfBullets;
    public float horizontalAngleStep;
    public float radius = 1f;
    public bool sphereMode = false;
    public int numberOfSphereParts = 1;
    public float bulletSpeed;
    public float firingRate;
    public bool shooting;
    public GameObject BulletPrefab;

    [Header("Private Bullets Settings")]
    private Vector3 startPoint;
    private float horizontalAngle = 0f;
    private float verticalAngle = 0f;
    private float verticalLayerIndex = 0;

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
            InvokeRepeating("SpawnBullets", 0f, firingRate);
        }
        else
        {
            CancelInvoke("SpawnBullets");
        }
    }
    
    public void SpawnBullets()
    {
        startPoint = transform.position;
        float horizontalAngleSpacing = 360f / numberOfBullets;
        
        if (sphereMode)
        {
            // Sphere mode
            for (verticalLayerIndex = 0; verticalLayerIndex <= numberOfSphereParts; verticalLayerIndex++)
            {
                verticalAngle = Mathf.Asin(1 - ((2 * verticalLayerIndex)/numberOfSphereParts));
                
                for (int i = 0; i < numberOfBullets; i++)
                {
                    float bulletDirXPosition = startPoint.x + Mathf.Cos(((horizontalAngle + i * horizontalAngleSpacing) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;
                    float bulletDirYPosition = startPoint.y + Mathf.Sin(verticalAngle) * radius;
                    float bulletDirZPosition = startPoint.z + Mathf.Sin(((horizontalAngle + i * horizontalAngleSpacing) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;

                    Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, bulletDirZPosition);
                    Vector3 bulletDirection= (newPositionVector - startPoint).normalized;

                    Bullet newBullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
                    newBullet.transform.position = startPoint;
                    newBullet.startPosition = startPoint;
                    newBullet.radius = radius;

                    // pentru alte traiectorii
                    // newBullet.transform.rotation = transform.rotation * new Quaternion(x,y,z,w); 
                    newBullet.transform.rotation = transform.rotation * Quaternion.identity;
                
                    newBullet.SetMoveSpeed(bulletSpeed);
                    newBullet.SetMoveDirection(bulletDirection);

                    if (horizontalAngle >= 360f)
                        horizontalAngle -= 360f;
                }
            }
        }
        else
        {
            // Normal mode
            for (int i = 0; i < numberOfBullets; i++)
            {
                float bulletDirXPosition = startPoint.x + Mathf.Cos(((horizontalAngle + i * horizontalAngleSpacing) * Mathf.PI) / 180f) * radius;
                float bulletDirYPosition = startPoint.y;
                float bulletDirZPosition = startPoint.z + Mathf.Sin(((horizontalAngle + i * horizontalAngleSpacing) * Mathf.PI) / 180f) * radius;

                Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, bulletDirZPosition);
                Vector3 bulletDirection= (newPositionVector - startPoint).normalized;

                Bullet newBullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
                newBullet.transform.position = startPoint;
                newBullet.startPosition = startPoint;
                newBullet.radius = radius;

                // pentru alte traiectorii
                // newBullet.transform.rotation = transform.rotation * new Quaternion(x,y,z,w); 
                newBullet.transform.rotation = transform.rotation * Quaternion.identity;
            
                newBullet.SetMoveSpeed(bulletSpeed);
                newBullet.SetMoveDirection(bulletDirection);
            
                if (horizontalAngle >= 360f)
                    horizontalAngle -= 360f;
            }
        }

        horizontalAngle += horizontalAngleStep;
    }

    public void OnFiringSpeedValueChanged() {
        if (shooting)
        {
            CancelInvoke("SpawnBullets");
            InvokeRepeating("SpawnBullets", 0.05f, firingRate);
        }
    }
}
