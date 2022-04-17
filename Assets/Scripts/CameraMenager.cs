using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject cameraObject;
    [SerializeField] float cameraSpeed;
    [SerializeField] float cameraRotationSpeed;
    [SerializeField] float cameraPivotSpeed;
    [SerializeField] float minPivotAngle;
    [SerializeField] float maxPivotAngle;
    [SerializeField] float cameraCollisionRadius;
    [SerializeField] float cameraCollisionOffset;
    [SerializeField] float minCollisionOffset;
    [SerializeField] float cameraCollisionMoveSpeed;
    [SerializeField] LayerMask cameraColisionLayers;

    CameraInput cameraInput;

    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraPosition = Vector3.zero;
    private float lookAngle;
    private float pivotAngle;
    private float defaultPositionZ;

    private void Awake()
    {
        cameraInput = GetComponent<CameraInput>();
        defaultPositionZ = cameraObject.transform.position.z;
    }

    private void Update()
    {
        cameraInput.HandleAllInputs();
    }

    public void HandleAllMovment()
    {
        FollowPlayer();
        RotateCamera();
        HandleCameraColision();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(
            transform.position,
            player.transform.position,
            ref cameraFollowVelocity,
            cameraSpeed
            );

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle += cameraInput.mouseInput.x * cameraRotationSpeed;
        pivotAngle -= cameraInput.mouseInput.y * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRottation = Quaternion.Euler(rotation);
        transform.rotation = targetRottation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRottation = Quaternion.Euler(rotation);
        cameraPivot.transform.localRotation = targetRottation;
    }

    private void HandleCameraColision()
    {
        float targetPosition = defaultPositionZ;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivot.transform.position;
        direction.Normalize();

        bool isSthBetweenCameraAndPivot = Physics.SphereCast(
                                                             cameraPivot.transform.position,
                                                             cameraCollisionRadius,
                                                             direction,
                                                             out hit,
                                                             Mathf.Abs(targetPosition),
                                                             cameraColisionLayers);

        if (isSthBetweenCameraAndPivot)
        {
            float distance = Vector3.Distance(cameraPivot.transform.position, hit.point);
            targetPosition -= distance - cameraCollisionOffset;
        }

        if(Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition -= minCollisionOffset;
        }

        cameraPosition.z = Mathf.Lerp(cameraObject.transform.position.z, targetPosition, cameraCollisionMoveSpeed);
        cameraObject.transform.localPosition = cameraPosition;
    }
}
