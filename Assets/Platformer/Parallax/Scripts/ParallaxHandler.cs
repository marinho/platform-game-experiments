using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParallaxHandler : MonoBehaviour
{
    // [SerializeField] float cameraMovementDifferenceRatio;
    [SerializeField] Camera cam;
    [SerializeField] Transform subject;
    [SerializeField] bool parallaxVertical = true;
    [SerializeField] bool parallaxHorizontal = true;

    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject / clippingPlane);

    void Start() {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    void Update() {
        Vector2 newPosition = startPosition + travel * parallaxFactor;
        float x = parallaxHorizontal ? newPosition.x : cam.transform.position.x;
        float y = parallaxVertical ? newPosition.y : cam.transform.position.y;
        transform.position = new Vector3(x, y, startZ);
    }
}
