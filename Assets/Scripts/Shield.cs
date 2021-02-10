using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour{

    public Transform objetivo;
    public Vector3 offset;
    private void LateUpdate(){
        this.transform.position = objetivo.transform.position+offset;
    }
}
