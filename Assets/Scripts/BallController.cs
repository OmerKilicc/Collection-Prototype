using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    [SerializeField] private TMP_Text _ballCountText;
    [SerializeField] private List<GameObject> _balls = new List<GameObject>();
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _horizontalLimit;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private GameObject _ballPrefab;
    
    private float _horizontalInput;
    private int _gateValue;
    private int _targetCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSideways();
        MoveForward();
        UpdateText();
        if (_balls.Count <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    void MoveSideways()
    {
        float _newXPosition;
        //TODO make it touchable with new Input Manager
        if (Input.GetMouseButton(0))
        {
            _horizontalInput = Input.GetAxisRaw("Mouse X");
        }
        else
        {
            _horizontalInput = 0;
        }
        _newXPosition = transform.position.x + _horizontalInput * _horizontalSpeed * Time.deltaTime;
        _newXPosition = Mathf.Clamp(_newXPosition, -_horizontalLimit, _horizontalLimit);

        transform.position = new Vector3(
            _newXPosition,
            transform.position.y,
            transform.position.z);
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stack"))
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.localPosition = new Vector3(0, 0,
                _balls[_balls.Count - 1].transform.localPosition.z - 1f);
            _balls.Add(other.gameObject);
        }

        if (other.gameObject.CompareTag("Gate"))
        {
            _gateValue = other.gameObject.GetComponent<GateController>().GetGateValue();
            _targetCount = _balls.Count + _gateValue;
            if (_gateValue > 0)
            {
                IncreaseBallCount();
            }
            else if (_gateValue < 0)
            {
                DecreaseBallCount();
            }
        }
    }

    private void UpdateText()
    {
        _ballCountText.text = _balls.Count.ToString();
    }

    private void IncreaseBallCount()
    {
        for (int i = 0; i < _gateValue; i++)
        {
            GameObject _newBall = Instantiate(_ballPrefab);
            _newBall.transform.SetParent(transform);
            _newBall.gameObject.GetComponent<SphereCollider>().enabled = false;
            _newBall.transform.localPosition = new Vector3(0, 0,
                _balls[_balls.Count - 1].transform.localPosition.z - 1f);
            _balls.Add(_newBall);
        }
    }

    private void DecreaseBallCount()
    {
        for (int i = _balls.Count-1; i >= _targetCount; i--)
        {
            Destroy(_balls[i]);
            _balls.RemoveAt(i);
        }
    }
}
