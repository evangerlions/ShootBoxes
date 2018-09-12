using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesHandler : MonoBehaviour
{
    public int Row = 10;
    [SerializeField]
    public int Column = 5;
    [SerializeField]
	Vector3 PrimalGeneratePoint = Vector3.zero;
	Vector3 CrtGeneratePoint = Vector3.zero;
	GameObject Box = null;
    float BoxHeight = 1;
	float BoxInterval = 2f;
    Transform _Transfrom = null;
    GameObject[,] BoxesArr;

	void Awake()
	{
        _Transfrom = transform;
        InitHandler();
		PrimalGeneratePoint = transform.localPosition;
		CrtGeneratePoint = PrimalGeneratePoint;
		Box = Resources.Load<GameObject>("BaseBox");
        BoxesArr = new GameObject[Row, Column];
	}

    void Start()
    {
		StartCoroutine(InitCreateBoxes());
    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator InitCreateBoxes()
    {
		CrtGeneratePoint = PrimalGeneratePoint;
        for (int RowCrt = 0; RowCrt < Row; RowCrt++)
        {
            for (int ColumnCrt = 0; ColumnCrt < Column; ColumnCrt++)
            {

                BoxesArr[RowCrt, ColumnCrt] = Instantiate(Box, CrtGeneratePoint, _Transfrom.rotation, _Transfrom);

                BoxesArr[RowCrt, ColumnCrt].GetComponent<BoxProperty>().setRowCol(RowCrt, ColumnCrt);
                
				CrtGeneratePoint += new Vector3(0, 0, BoxInterval);
				yield return new WaitForSeconds(0.2f);
            }
			CrtGeneratePoint.z = PrimalGeneratePoint.z;
        }
    }

    private void InitHandler(){
        // 设置旋转角为0
        _Transfrom.localEulerAngles = new Vector3(0, 0, 0);
        // 如果handler高度小于最低高度，那么将它修正到最低高度
        RaycastHit hit;
        GameObject box = Box as GameObject;
        float maxDistance = (Row + 5) * BoxHeight;
        if(_Transfrom.position.y <= 0){
            _Transfrom.Translate(0, -_Transfrom.position.y + maxDistance, 0);
        }else if(Physics.Raycast(_Transfrom.position, Vector3.down, out hit, maxDistance)){
            _Transfrom.Translate(new Vector3(0, maxDistance - hit.distance, 0));
        }
    }

    public void DestoryTheBox(int row, int column){
        Destroy(BoxesArr[row, column]);
        GameObject box = GenerateBox(column);
        box.GetComponent<BoxProperty>().setRowCol(row, column);
        BoxesArr[row, column] = box;
    }

    private GameObject GenerateBox(int column)
    {
        Vector3 pos = PrimalGeneratePoint + new Vector3(0, 0, BoxInterval * column);
        return Instantiate(Box, pos, _Transfrom.rotation, _Transfrom);
    }
}
