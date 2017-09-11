using UnityEngine;
using System.Collections;

public class BallIntroBehavior : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(1f, 1f, 0f) * Time.deltaTime * 60f);
    }
}
