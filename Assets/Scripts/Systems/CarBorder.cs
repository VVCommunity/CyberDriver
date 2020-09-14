using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBorder : MonoBehaviour
{
    [SerializeField] 
    private Transform car;
    [SerializeField]
    private float minBorderYCar;
    [SerializeField]
    private float maxBorderYCar;


    private void FixedUpdate()
    {
         
        car.position = new Vector3(car.position.x, Mathf.Clamp(car.position.y, minBorderYCar, maxBorderYCar), car.position.z);
    }
}
