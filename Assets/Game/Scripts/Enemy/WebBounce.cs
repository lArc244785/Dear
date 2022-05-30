using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBounce : MonoBehaviour
{
    [SerializeField]
    public float angle = 0;
    private float lerpTime = 0;
    [SerializeField]
    private float speed = 2f;

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;


        transform.rotation = CalculateMovementOfPendulum();
    }

    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
}
