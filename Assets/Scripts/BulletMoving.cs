using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    public int BulletSpeed = 200;
    // Update is called once per frame
    void Update()
    {
        transform.Translate (Vector3.right * Time.deltaTime * BulletSpeed);
        Destroy(gameObject, 1);
    }
}
