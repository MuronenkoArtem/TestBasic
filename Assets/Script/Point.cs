using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public Text text;

    public float X = 0;
    public float Y = 0;
    public int indx = 0;

    void Start()
    {
        X = transform.position.x;
        Y = transform.position.y;
        indx = int.Parse(name);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = Input.mousePosition;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        transform.position = new Vector3(transform.position.x, curPosition.y);

        X = transform.position.x;
        Y = transform.position.y;

        text.text = "X: " + X + "\nY: " + Y;
        
    }
}
