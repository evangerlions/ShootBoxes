using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	Transform GunPoint;
	Transform AimCenter;
	Camera _Camera;

	// Use this for initialization
	void Start () {
		GunPoint = transform.Find("GunPoint");
		AimCenter = GameObject.FindGameObjectWithTag("AimCenter").transform;
		_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			Attack();
		}
	}

	void Attack(){
		Ray ray	= _Camera.ScreenPointToRay(AimCenter.position);
		Vector3 targetDirection;
		RaycastHit hit;
		// 命中目标将打击点设置到此碰撞点
		if(Physics.Raycast(ray, out hit, 1000f)){
			if(hit.collider.tag.Equals("Box")){
				hit.collider.gameObject.GetComponent<BoxProperty>().DestoryBox();
			}
			targetDirection = hit.point - GunPoint.position;
			Debug.DrawRay(GunPoint.position, targetDirection, Color.black,10);
		}
	}
}
