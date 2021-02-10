using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirar : MonoBehaviour
{

    [Header("REFERENCIAS A ESCENA")]
    public Transform Objetivo;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        this.transform.LookAt(Objetivo);       
    }


}