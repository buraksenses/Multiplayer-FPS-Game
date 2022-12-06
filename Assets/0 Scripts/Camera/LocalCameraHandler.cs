using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    private Camera localCamera;

    public Transform cameraAnchorPoint;

    private float cameraRotaionX = 0f;
    private float cameraRotaionY = 0f;

    private Vector2 viewInput;

    private NetworkCharacterControllerPrototypeCustom _networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        _networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
    }

    private void Start()
    {
        if (localCamera.enabled)
            localCamera.transform.parent = null;
    }

    private void LateUpdate()
    {
        if (cameraAnchorPoint == null)
            return;
        if(!localCamera.enabled)
            return;
        
        localCamera.transform.position = cameraAnchorPoint.transform.position;
        
        //Calculate rotation

        cameraRotaionX += -(viewInput.y * Time.deltaTime *
                          _networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed);
        cameraRotaionX = Mathf.Clamp(cameraRotaionX, -90, 90);

        cameraRotaionY += viewInput.x * Time.deltaTime * _networkCharacterControllerPrototypeCustom.rotationSpeed;
        
        localCamera.transform.rotation = Quaternion.Euler(cameraRotaionX,cameraRotaionY,0);
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
