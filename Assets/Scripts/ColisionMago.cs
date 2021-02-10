using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionMago : MonoBehaviour{


    [Header("REFERENCIAS A ESCENA")]
    public MageController code_Mage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("BalaJugador")){
            code_Mage.Golpeado();
        }
    }

}
