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
    private float _horizontalInput;
    
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
            other.gameObject.transform.localPosition = new Vector3(0, 0,
                _balls[_balls.Count - 1].transform.localPosition.z + 1f);
            _balls.Add(other.gameObject);


        }
    }

    void UpdateText()
    {
        _ballCountText.text = _balls.Count.ToString();
    }
}
