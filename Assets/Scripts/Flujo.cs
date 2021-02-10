using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class Flujo : MonoBehaviour{

    [Header("REFERENCIAS A ESCENA")]
    public Dialogo code_Dialogo;
    public GameObject[] pistolas;

    [Header("REFERENCIAS A PROYECTO")]
    public Material mat_Negro;
    public Material mat_Texto;


    // Start is called before the first frame update
    void Start(){
        Inicializar();
        StartCoroutine(corrutina_EsperarPrimerClic());
    }

    void Inicializar(){
        for (int i = 0; i < pistolas.Length; i++)
            pistolas[i].SetActive(false);
        mat_Negro.SetColor("_Color", Color.black);
        mat_Texto.SetColor("_Color", new Color(1, 1, 1, 0.0f));
    }

    IEnumerator corrutina_EsperarPrimerClic(){
        
        yield return new WaitForSeconds(2.0f);

        float tiempo = 0.0f;
        while (tiempo < 1){
            tiempo += Time.deltaTime/2; 
            mat_Texto.SetColor("_Color", new Color(1,1,1,tiempo));
            yield return new WaitForEndOfFrame();
        }

        while (true){
            yield return new WaitForEndOfFrame();

            if (Input.GetKey(KeyCode.Return) ||
            OVRInput.Get(OVRInput.RawButton.LIndexTrigger) ||
            OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        while (tiempo > 0){
            tiempo -= Time.deltaTime / 2;
            mat_Negro.SetColor("_Color", new Color(0, 0, 0, tiempo));
            mat_Texto.SetColor("_Color", new Color(1, 1, 1, tiempo));
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(3.0f);
        code_Dialogo.IniciarPlatica();
    }




}