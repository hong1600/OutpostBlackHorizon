using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CameraFpsMove cameraFpsMove;
    [SerializeField] CameraFpsShake cameraFpsShake;
    [SerializeField] CameraFpsZoom cameraFpsZoom;
    [SerializeField] CameraFpsDead cameraFpsDead;
    [SerializeField] CameraTopMove cameraTopMove;
    [SerializeField] CameraTopZoom cameraTopZoom;
    [SerializeField] CameraTopToFps cameraTopToFps;
    [SerializeField] CameraTurretMove cameraTurretMove;
    [SerializeField] CutScene cutScene;

    protected override void Awake()
    {
        base.Awake();
    }

    public CameraFpsMove CameraFpsMove { get { return cameraFpsMove; } }
    public CameraFpsShake CameraFpsShake { get { return cameraFpsShake; } }
    public CameraFpsZoom CameraFpsZoom { get { return cameraFpsZoom; } }
    public CameraFpsDead CameraFpsDead { get { return cameraFpsDead; } }
    public CameraTopMove CameraTopMove { get { return cameraTopMove; } }
    public CameraTopZoom CameraTopZoom { get { return cameraTopZoom; } }
    public CameraTopToFps CameraTopToFps { get { return cameraTopToFps; } }
    public CameraTurretMove CameraTurretMove {  get { return cameraTurretMove; } }
    public CutScene CutScene { get { return cutScene; } }
}
