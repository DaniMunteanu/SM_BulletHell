using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PatternGenerator : MonoBehaviour
{
    [Header("Bullets Settings")]
    public int numberOfBullets;
    public float horizontalAngleStep;
    public float radius = 1f;
    public float wiggleSpeed = 0;
    public float horizontalWiggleSize = 0;
    public float verticalWiggleSize = 0;
    public bool sphereMode = false;
    public int numberOfSphereParts = 1;
    public float bulletSpeed;
    public float acceleration = 0;
    public float verticalAngleStep;
    public float phi;
    public float firingRate;
    public bool shooting;
    public GameObject BulletPrefab;

    [Header("Private Bullets Settings")]
    private Vector3 startPoint;
    private float horizontalAngle = 0f;
    // private
    private float verticalAngle = 0f;
    private float verticalLayerIndex = 0;
    private int currentShot = 0; // K

    public void SpawnBullet(int indexInRing, float horizontalAngleSpacing, float horizontalWiggleModifier, float verticalWiggleModifier)
    {
        float bulletDirXPosition = startPoint.x + Mathf.Cos(((horizontalAngle + indexInRing * horizontalAngleSpacing + horizontalWiggleModifier) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;
        float bulletDirYPosition = startPoint.y + Mathf.Sin(verticalAngle + verticalWiggleModifier) * radius;
        float bulletDirZPosition = startPoint.z + Mathf.Sin(((horizontalAngle + indexInRing * horizontalAngleSpacing + horizontalWiggleModifier) * Mathf.PI) / 180f) * Mathf.Cos(verticalAngle) * radius;

        Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, bulletDirZPosition);
        Vector3 bulletDirection= (newPositionVector - startPoint).normalized;

        //Bullet newBullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
        Bullet newBullet = ObjectPool.SharedInstance.GetPooledObject()?.GetComponent<Bullet>();
        if (newBullet == null)
        {
            return;
        }
        else
        {
            newBullet.gameObject.SetActive(true);
        }
        newBullet.transform.position = startPoint;
        newBullet.startPosition = startPoint;
        newBullet.radius = radius;

        // pentru alte traiectorii
        // newBullet.transform.rotation = transform.rotation * new Quaternion(x,y,z,w); 
        newBullet.transform.rotation = transform.rotation * Quaternion.identity;
    
        newBullet.SetMoveSpeed(bulletSpeed);
        newBullet.SetAcceleration(acceleration);
        newBullet.SetMoveDirection(bulletDirection);

        newBullet.horizontalAngle = horizontalAngle;
        newBullet.horizontalAngleSpacing = indexInRing * horizontalAngleSpacing;
        newBullet.verticalAngle = verticalAngle;
        newBullet.wiggleSpeed = wiggleSpeed;
        newBullet.horizontalWiggleSize = horizontalWiggleSize;
        newBullet.verticalWiggleSize = verticalWiggleSize;

        if (horizontalAngle >= 360f)
            horizontalAngle -= 360f;
    }

    public void Fire()
    {
        shooting = !shooting;
        if (shooting)
        {
            InvokeRepeating("FireBullets", 0f, firingRate);
        }
        else
        {
            CancelInvoke("FireBullets");
        }
    }
    
    public void FireBullets()
    {
        startPoint = transform.position;
        float horizontalAngleSpacing = 360f / numberOfBullets;
        float horizontalWiggleModifier = horizontalWiggleSize * Mathf.Sin(wiggleSpeed * Time.time);
        float verticalWiggleModifier = verticalWiggleSize * Mathf.Cos(wiggleSpeed * Time.time);
        
        if (sphereMode)
        {
            // Sphere mode
            for (verticalLayerIndex = 0; verticalLayerIndex <= numberOfSphereParts; verticalLayerIndex++)
            {
                verticalAngle = Mathf.Asin(1 - ((2 * verticalLayerIndex)/numberOfSphereParts)) + verticalAngleStep * Mathf.PI / 180f * Mathf.Sin(phi * currentShot);
                
                for (int i = 0; i < numberOfBullets; i++)
                {
                    SpawnBullet(i, horizontalAngleSpacing, horizontalWiggleModifier, verticalWiggleModifier);
                }
            }
        }
        else
        {
            verticalAngle = 0;
            verticalWiggleSize = 0;

            // Normal mode
            for (int i = 0; i < numberOfBullets; i++)
            {
                SpawnBullet(i, horizontalAngleSpacing, horizontalWiggleModifier, verticalWiggleModifier);
            }
        }

        horizontalAngle += horizontalAngleStep;
        currentShot += 1;
        // Debug.LogFormat("Current shot: {0}", currentShot);
    }


    public void OnFiringSpeedValueChanged() {
        if (shooting)
        {
            CancelInvoke("FireBullets");
            InvokeRepeating("FireBullets", 0.05f, firingRate);
        }
    }
}
