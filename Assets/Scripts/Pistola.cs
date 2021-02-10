using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour{

    [Header("CONFIGURACION")]
    public bool PistolaDerecha = false;
    public bool PistolaIzquierda = false;
    public float fuerza = 100;

    [Header("REFERENCIAS A ESCENA")]
    public Transform Canion;

    [Header("REFERENCIAS A PROYECTO")]
    public GameObject[] bala;

    [Header("CONSULTA")]
    public int balaActual = 0;
    public bool RecoilD = false;
    public bool CambiandoD = false;

    public bool RecoilI = false;
    public bool CambiandoI = false;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
            Disparar();

        if (Input.GetKeyDown(KeyCode.D))
            CambiarBala();

        if (PistolaIzquierda)
            InputIzquierdo();

        if (PistolaDerecha)
            InputDerecho();
    }

    void InputIzquierdo(){
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)){
            if (!RecoilI){
                Disparar();
                RecoilI = true;
            }
        }
        else
            RecoilI = false;
        
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.9f){
            if (!CambiandoI) {
                CambiarBala();
                CambiandoI = true;
            }
        }else 
            CambiandoI = false;
    }


    void InputDerecho(){
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){
            if (!RecoilD){
                Disparar();
                RecoilD = true;
            }
        }
        else
            RecoilD = false;

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0.9f){
            if (!CambiandoD){
                CambiarBala();
                CambiandoD = true;
            }
        }
        else
            CambiandoD = false;
    }


    void Disparar(){
        GameObject proyectil = Instantiate(bala[balaActual], Canion.position, Canion.rotation);
        proyectil.GetComponent<Rigidbody>().AddForce(Canion.forward * fuerza, ForceMode.Impulse);
        proyectil.gameObject.tag = "BalaJugador";
        Destroy(proyectil,10.0f);
    }

    void CambiarBala(){
        balaActual++;
        if (balaActual >= bala.Length)
            balaActual = 0;

        Debug.Log("Tipo: " + bala[balaActual].gameObject.name);
    }


}