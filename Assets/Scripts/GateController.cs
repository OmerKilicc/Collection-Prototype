using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private enum GateType
    {
        PositiveGate,
        NegativeGate
    }

    [SerializeField] private GateType _gateType;
    [SerializeField] private int _gateValue;
    [SerializeField] private TMP_Text _valueText;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGateValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGateValue() { return _gateValue; }
    void GenerateGateValue()
    {
        switch (_gateType)
        {
            case GateType.PositiveGate:
                _gateValue = Random.Range(1, 10);
                _valueText.text = _gateValue.ToString();
                break;
            case GateType.NegativeGate:
                _gateValue = Random.Range(-10, -1);
                _valueText.text = _gateValue.ToString();
                break;
        }
    }
}
