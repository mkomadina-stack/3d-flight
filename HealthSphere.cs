using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSphere : MonoBehaviour
{

    private Color _myColor;
    private float _alphaVal = 1.0f;
    private float _direction = -1.0f;
    private float _increment = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
      _myColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
      if (_alphaVal >= 0.9f) {
        _direction = -1.0f;
      }
      else if (_alphaVal <= 0.1f) {
        _direction = 1.0f;
      }

      _alphaVal += _increment * _direction;

      Pulse(_alphaVal);
    }


    private void Pulse(float alpha)
    {
      //  Debug.Log(_myColor);
        Color tempColor = gameObject.GetComponent<Renderer>().material.color;
        tempColor.a = 0.1f;
        gameObject.GetComponent<Renderer>().material.color = tempColor;
    }
}
