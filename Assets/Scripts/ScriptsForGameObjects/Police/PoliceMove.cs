using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 17.5f;
    [SerializeField]
    private float accelerationFactor = 0.25f;
    [SerializeField]
    private float gapBetweenAccelerations = 6f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform body;

    private float approximationFactor;

    new private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        approximationFactor = 90f / (GameManager.DistanceBetweenWalls / 2);
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        var x = transform.position.x;
        body.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, approximationFactor * x));
        var targetZPosition = transform.position.z + (forwardSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(new Vector3(target.position.x, target.position.y, targetZPosition));
    }
    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            forwardSpeed += accelerationFactor;
            yield return new WaitForSeconds(gapBetweenAccelerations);
        }
    }
}
