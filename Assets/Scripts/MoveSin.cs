using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    bool stayDetermined = true;

    Vector3 pos, localScale;

    void Start()
    {
        pos = transform.position;

        localScale = transform.localScale;
    }

    void Update()
    {
        CheckWhereToFace();

        if (stayDetermined)
            MoveRight();
        else
            MoveLeft();
    }

    void CheckWhereToFace()
    {
        if (pos.x < -6f)
            stayDetermined = true;
        else if (pos.x > 8f)
            stayDetermined = false;

        if (((stayDetermined) && (localScale.x < 0)) || ((stayDetermined) && (localScale.x > 0)))
            localScale.x *= -1;
        transform.localScale = localScale;
    }

    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
