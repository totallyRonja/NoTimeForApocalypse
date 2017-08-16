using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMagic : MonoBehaviour {

	public TransparencySortMode mode;
	public Vector3 axis = Vector3.up;

//    private Camera[] cam = null;

	// Use this for initialization
	void Start () {
        cameraForThis = GetComponent<Camera>();
        cameraForThis.transparencySortMode = mode;
        cameraForThis.transparencySortAxis = axis;
    }


    public void OnEnable()
    {
        m_originalFOV = GetComponent<Camera>().orthographicSize;
        Camera.onPreCull += PreCullAdjustFOV;
        Camera.onPreRender += PreRenderAdjustFOV;
    }

    public void OnDisable()
    {
        Camera.onPreCull -= PreCullAdjustFOV;
        Camera.onPreRender -= PreRenderAdjustFOV;
    }

    #region avoid frustum culling
    float m_originalFOV;
    Camera cameraForThis;
    public Vector3 frustumRotation = new Vector3(0, 180, 0);
    Vector3 m_oldRotation;
    public void PreRenderAdjustFOV(Camera cam)
    {
        if (cam == cameraForThis)
        {
            cam.orthographicSize = m_originalFOV * 1.0f;
        }
    }

    public void PreCullAdjustFOV(Camera cam)
    {
        m_originalFOV = cam.orthographicSize;
        if (cam == cameraForThis)
        {
            cam.orthographicSize = m_originalFOV * 1.2f;
        }
    }
    #endregion
}
