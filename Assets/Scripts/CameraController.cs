using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    private Transform _ballTransform;
    private Vector3 _offset;
    [SerializeField] private float _lerpTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _ballTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = transform.position - _ballTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 _newCameraPos = Vector3.Lerp(
            transform.position, 
            _ballTransform.position + _offset, 
            _lerpTime * Time.deltaTime);

        transform.position = _newCameraPos;
    }
}
