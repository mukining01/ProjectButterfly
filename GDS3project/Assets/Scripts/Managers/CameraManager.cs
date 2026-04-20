using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    public Camera base_camera_p;
    static Camera base_camera;

    public List<CameraProperties> cameras_p = new List<CameraProperties>();
    static List<CameraProperties> cameras = new List<CameraProperties>();

    static CinemachineBrain cinemachine_brain;
    static CinemachineCamera current_camera = null;

    private void Awake()
    {
        base_camera = base_camera_p;
        cameras = cameras_p;

        for (var i = 0; i < cameras.Count; i++)
        {
            CinemachineCamera _cam = cameras[i].camera;
            if (_cam != null) _cam.Priority = 0;
        }

        cinemachine_brain = FindFirstObjectByType<CinemachineBrain>();

        Switch_Camera(Camera_Types.MainCamera);
    }

    public static void Switch_Camera(Camera_Types new_camera_Types)
    {
        if(cinemachine_brain == null) return;

        CameraProperties _cameraProperties = null;

        for(var i = 0; i < cameras.Count; i++)
        {
            if (cameras[i].cameraType != new_camera_Types) continue;

            _cameraProperties = cameras[i];
            break;
        }

        if (_cameraProperties == null) return;

        float _transition_time = _cameraProperties.transition_time;

        if (current_camera != null) current_camera.Priority = 0;
        else _transition_time = 0;

        CinemachineBlendDefinition _blend_def = new(_cameraProperties.blend_type, _transition_time);
        cinemachine_brain.DefaultBlend = _blend_def;

        current_camera = _cameraProperties.camera;
        current_camera.Priority = 50;  
    }

    public static Camera Get_Base_Camera()
    {
        return base_camera;
    }
}

[System.Serializable]
public class CameraProperties
{
    public Camera_Types cameraType;
    public CinemachineCamera camera;
    public CinemachineBlendDefinition.Styles blend_type;
    public float transition_time;
}

public enum Camera_Types
{
    MainCamera,
    Evolution_Camera_0,
    Evolution_Camera_1,
    SmallMap,
    LargeMap
}
