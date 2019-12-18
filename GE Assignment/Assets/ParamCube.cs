using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, ((AudioSpectrum._audioBandBuffer[_band] * _maxScale) + _startScale), transform.localScale.z);
        }
        if(!_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, ((AudioSpectrum._audioBand[_band] * _maxScale) + _startScale), transform.localScale.z);
        }
    }
}
