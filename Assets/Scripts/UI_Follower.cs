using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Follower : MonoBehaviour{

	public Transform camaraUsuario;
	public Vector3 offset;
	public float speed;
	public bool lookAt;
	void Start (){
		
	}

	

	void Update (){
		Vector3 displacement = (camaraUsuario.transform.right * offset.x) + (camaraUsuario.transform.up * offset.y) + (camaraUsuario.transform.forward * offset.z);
		this.transform.position = Vector3.Lerp(this.transform.position, camaraUsuario.transform.position + displacement, Time.deltaTime * speed);
		if (lookAt){
			this.transform.LookAt(camaraUsuario, Vector3.up);
			this.transform.Rotate(Vector3.up*180);
		}
	}

	void FixedUpdate (){

	}

}