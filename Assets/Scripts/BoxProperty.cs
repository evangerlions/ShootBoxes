using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProperty : MonoBehaviour
{

    public bool IsRemoveBounce = false;
    public Rigidbody Rb = null;
    private int _Col { get; set; }
    public int Col
    {
        get
        {
            return _Col;
        }
        set
        {
            _Col = value;
        }
    }
    private int _Row { get; set; }
    public int Row
    {
        get
        {
            return _Row;
        }
        set
        {
            _Row = value;
        }
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (IsRemoveBounce && Rb.isKinematic)
        {
            Rb.isKinematic = false;
            Rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (IsRemoveBounce)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void DestoryBox()
    {
        transform.parent.GetComponent<BoxesHandler>().DestoryTheBox(_Row, _Col);
    }

    public void setRowCol(int row, int col){
        _Row = row;
        _Col = col;
    }
}
