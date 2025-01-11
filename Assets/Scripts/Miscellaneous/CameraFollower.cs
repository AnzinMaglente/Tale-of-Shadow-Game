using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField]                                // Allows the variable to be edited inside unity.
    private Transform _target;                      // This variable follows the player's transform value.
    private GameObject tPlayer;

    Camera _camera;

    // These variables act as boundaries for the camera.
    [SerializeField]
    private float _minX;
    [SerializeField]
    private float _maxX;
    [SerializeField]
    private float _minY;
    [SerializeField]
    private float _maxY;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                _target = tPlayer.transform;
            }
        }

        Vector3 _targetPosition = _target.position + _offset;

        float height = GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(_target.position.x, _minX + width, _maxX - width);
        position.y = Mathf.Clamp(_target.position.y, _minY, _maxY);
    }
}
