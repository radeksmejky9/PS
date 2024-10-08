using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform Assets;
    [SerializeField]
    private ARTrackedImageManager imageManager;

    private Transform cam;
    private GameObject currentCube;
    private Transform Model;

    void Start()
    {
        Model = Assets.parent;
        cam = Camera.main.transform;
    }

    void OnEnable()
    {
        BarcodeScanner.QRChanged += Move;
    }

    void OnDisable()
    {
        BarcodeScanner.QRChanged -= Move;
    }
    public void Move(SnappingPoint snappingPoint)
    {
        if (currentCube != null && Assets.parent != null)
        {
            Assets.parent = Model;
            Destroy(currentCube);
        }

        Assets.SetPositionAndRotation(new Vector3(
                -snappingPoint.Position.X + cam.position.x,
                -snappingPoint.Position.Y + cam.position.y,
                -snappingPoint.Position.Z + cam.position.z
                ), new quaternion(0, 0, 0, 0));

        currentCube = CreateAnchor();

        Quaternion snappingPointRotation = Quaternion.Euler(0, snappingPoint.Rotation.Y, 0);

        float camYRotation = Camera.main.transform.rotation.eulerAngles.y;
        Quaternion camYRotationOnly = Quaternion.Euler(0, camYRotation, 0);
        currentCube.transform.rotation = snappingPointRotation * camYRotationOnly;
        Assets.gameObject.SetActive(true);
    }

    private GameObject CreateAnchor()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().enabled = false;
        cube.transform.position = cam.position;
        cube.transform.parent = Model;
        Assets.parent = cube.transform;
        return cube;
    }
}
