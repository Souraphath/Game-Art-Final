﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public float lookSensitivity = 5f;
    [HideInInspector]
    public float currentTargetCameraAngle = 60f;
    float ratioZoomV;

    [HideInInspector]
    public float xRot, yRot;
    [HideInInspector]
    public float curYRot, curXRot;
    [HideInInspector]
    public float yRotV, xRotV;
    [HideInInspector]
    public float lookSmoothDamp = 0.3f; // bigger is smoother
    [HideInInspector]
    public bool in2DMode;
	public bool canShift;

	private Transform parentTransform;

    [HideInInspector]
    public float currentAimRatio = 1.0f;

    float zPos;

    Camera cameraRef;

    void Awake()
    {
        in2DMode = true;
        zPos = -3f;
        transform.localPosition = new Vector3(0, -0.225f, zPos);
        cameraRef = GetComponent<Camera>();
		canShift = true;
    }

    void LateUpdate()
    {
		parentTransform = GetComponentInParent<Transform>();
        if (canShift && Input.GetButtonDown ("Switch")) {
			switchPerspective();
		}

        curXRot = Mathf.SmoothDamp(curXRot, xRot, ref xRotV, lookSmoothDamp);

        if (in2DMode) {
            curYRot = Mathf.SmoothDamp(curYRot, 0, ref yRotV, lookSmoothDamp);
            transform.localPosition = new Vector3(0, -0.225f, zPos);
        } else {
            transform.localPosition = new Vector3(0, 0, zPos);
            curYRot = Mathf.SmoothDamp(curYRot, 90 + yRot, ref yRotV, lookSmoothDamp);

        }

        transform.rotation = Quaternion.Euler(0, curYRot, 0);

        if (transform.position.y <= -15)
            transform.position = new Vector3(transform.position.x, -15f, transform.position.z);
    }

	public void switchPerspective(){
		in2DMode = !in2DMode;
		if (in2DMode) {
			zPos = Mathf.SmoothDamp (-3f, -2, ref yRotV, 2);
			cameraRef.orthographic = true;
		} else {
			zPos = Mathf.SmoothDamp (-2, -3f, ref yRotV, 2);
			cameraRef.orthographic = false;
		}
	}
}
