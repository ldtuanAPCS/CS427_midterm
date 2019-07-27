using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
        dif.Normalize();
        float rotz = Mathf.Atan2 (dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0f,0f,rotz);
    }
}
