using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length;
    float startPosX;
    GameObject cam;
    [SerializeField] float parallaxEffect;

    private void Start()
    {
        cam = Camera.main.gameObject;

        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        if (cam != null)
        {
            float tempX = (cam.transform.position.x * (1 - parallaxEffect));
            float distX = (cam.transform.position.x * parallaxEffect);
            transform.position = new Vector3(startPosX + distX, cam.transform.position.y, transform.position.z);
            if (tempX > startPosX + length) startPosX += length;
            else if (tempX < startPosX - length) startPosX -= length;
        }
    }

}
