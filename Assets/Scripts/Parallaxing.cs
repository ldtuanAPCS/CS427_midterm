using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] scales;
    public float smooth = 1f;
    private Transform cam;
    private Vector3 previousCampos;
    // Start is called before the first frame update

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCampos = cam.position;
        scales = new float[backgrounds.Length];
        for (int i=0; i<backgrounds.Length; i++)
        {
            scales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<backgrounds.Length; i++)
        {
            float parallax = (previousCampos.x - cam.position.x) * scales[i];
            float backgroundTargetPosX = backgrounds[i].position.x +parallax;
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smooth*Time.deltaTime);
        }
        previousCampos = cam.position;
    }
}
