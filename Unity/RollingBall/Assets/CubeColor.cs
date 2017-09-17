using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    private Color originalColor;

    private Vector3 originalPosition;

    // Use this for initialization
    private void Start()
    {
        var newColor = new Color(Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0));
        this.GetComponent<Renderer>().material.color = newColor;
        this.originalColor = newColor;
        this.originalPosition = this.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnSelect()
    {
        var cube = this.GetComponent<Renderer>();
        cube.material.color = Color.magenta;
    }

    private void OnReset()
    {
        var cube = this.GetComponent<Renderer>();
        cube.material.color = this.originalColor;
    }

    private void OnResetBlock()
    {
        this.transform.position = this.originalPosition;
    }
}