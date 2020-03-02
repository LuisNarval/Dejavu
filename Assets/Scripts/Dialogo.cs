using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    [Header("CONFIGURACION")]
    public string[] Lineas;
    public float tiempoEntreCaracteres;

    [Header("REFERENCIAS A ESCENA")]
    public Text txt_Texto;
    public Animator anim_Luis;

    public CanvasGroup CG_CajaTexto;
    public CanvasGroup CG_Texto;
    public CanvasGroup CG_Siguiente;

    [Header("CONSULTA")]
    public int LineaActual = 0;
    public char[] caracteres;
    public bool CajaAbierta = false;
    public bool EscrituraTerminada = false;

    // Start is called before the first frame update
    void Start(){
        inicializar();
    }

    private void LateUpdate(){

        if (Input.GetKeyDown(KeyCode.Return)|| 
            OVRInput.Get(OVRInput.RawButton.LIndexTrigger)||
            OVRInput.Get(OVRInput.RawButton.RIndexTrigger) ){
            if (CajaAbierta){
                if (EscrituraTerminada){
                    Siguiente();
                }else{
                    StopAllCoroutines();
                    txt_Texto.text = Lineas[LineaActual];
                    CG_Siguiente.alpha = 1.0f;
                    EscrituraTerminada = true;
                    anim_Luis.SetTrigger("Sonreir");
                }
            }
            else{
                StartCoroutine(corrutina_Aparecer());
            }
        }

    }


    void inicializar(){
        CG_CajaTexto.alpha = 0;
        CG_Siguiente.alpha = 0;
        CG_Texto.alpha = 1;
        txt_Texto.text = "";
        CajaAbierta = false;
    }

    IEnumerator corrutina_Aparecer(){
        float tiempo = 0;

        while (tiempo<1.0f){
            tiempo += Time.deltaTime;
            CG_CajaTexto.alpha = tiempo;
            yield return new WaitForEndOfFrame();
        }
        CG_CajaTexto.alpha = 1.0f;

        CajaAbierta = true;

        StartCoroutine(corrutina_Siguiente());
    }

    public void Siguiente(){
        StopAllCoroutines();
        StartCoroutine(corrutina_QuitarMensajeAnterior());
    }


    IEnumerator corrutina_QuitarMensajeAnterior(){
        txt_Texto.text = Lineas[LineaActual];
        
        while(CG_Texto.alpha > 0.0f){
            CG_Texto.alpha -= Time.deltaTime*2;
            CG_Siguiente.alpha -= Time.deltaTime*2;
            yield return new WaitForEndOfFrame();
        }
        CG_Texto.alpha = 0;
        CG_Siguiente.alpha = 0;

        yield return new WaitForSeconds(0.2f);

        txt_Texto.text = "";
        LineaActual++;
        StartCoroutine(corrutina_Siguiente());
    }
    

    IEnumerator corrutina_Siguiente(){
        CG_Texto.alpha = 1;

        caracteres = null;
        caracteres = Lineas[LineaActual].ToCharArray();

        EscrituraTerminada = false;
        anim_Luis.SetTrigger("Hablar");

        int caracterActual = 0;
        string cadena;

        while (caracterActual < caracteres.Length){

            cadena = null;
            for(int i = 0; i <= caracterActual; i++){
                if(caracteres[i] == '>'){
                    Debug.Log("Salto de escape");
                }
                cadena = cadena + caracteres[i].ToString();
            }

            txt_Texto.text = cadena;

            caracterActual++;
            yield return new WaitForSeconds(tiempoEntreCaracteres);
        }


        EscrituraTerminada = true;
        anim_Luis.SetTrigger("Enojado");

        while (CG_Siguiente.alpha<1.0f){
            CG_Siguiente.alpha += Time.deltaTime * 2.0f;
            yield return new WaitForEndOfFrame();
        }

        
    }




}