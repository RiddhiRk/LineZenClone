using UnityEngine;
using System.Collections;

public class ObjectFollow : MonoBehaviour
{

    #region Public_Variables
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    public Vector3 new_Position = new Vector3(0f, 0.17f, 0f);
    public static ObjectFollow instance;
    #endregion

    #region Private_Variables
    Vector3 _offset;
    private float _backupSmoothing;
    #endregion

    #region Events
    #endregion

    #region Unity_CallBacks
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

    void OnEnable()
    {

    }

    // Use this for initialization
    void Start()
    {
        //AdManager.Instance.ShowInterstitial();

        // Calculate the initial offset.
        _offset = transform.position - target.position;
        _backupSmoothing = smoothing;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + _offset;
        float x = Vector3.Lerp(transform.position, targetCamPos, 5* Time.deltaTime).x;
        float y = Vector3.Lerp(transform.position, targetCamPos - new Vector3(0, 4, 0), smoothing * Time.deltaTime).y;
        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = new Vector3(x, y, targetCamPos.z);
    }

    void OnDisable()
    {

    }
    #endregion

    #region Private_Methods
    #endregion

    #region Public_Methods

    public void ResetPositions()
    {
        smoothing = _backupSmoothing;        // The speed with which the camera will be following.
        new_Position = new Vector3(0f, 0.17f, 0f);
        _offset = transform.position - target.position;
    }

    #endregion

    #region Coroutines
    #endregion

    #region Custom_CallBacks
    #endregion
}
