using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offsetX = 2;
    public bool RightBuddy = false;
    public bool LeftBuddy = false;
    public bool ReverseScale = false;
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;
    // Start is called before the first frame update
    void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (RightBuddy == false || LeftBuddy == false){
            float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
            float edgeRightVision = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
            float edgeLeftVision = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

            if (cam.transform.position.x <= edgeLeftVision + offsetX && LeftBuddy == false){
                MakeNewBuddy(-1);
                LeftBuddy = true;

            } else if (cam.transform.position.x >= edgeRightVision + offsetX && RightBuddy == false){
                MakeNewBuddy(1);
                RightBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int direction){
        Vector3 newpos = new Vector3(myTransform.position.x + spriteWidth * direction, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newpos, myTransform.rotation) as Transform;

        if (ReverseScale == true){
            newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;
        if (direction > 0){
            newBuddy.GetComponent<Tiling>().LeftBuddy = true;
        } else {
            newBuddy.GetComponent<Tiling>().RightBuddy = true;
        }
    }
}
