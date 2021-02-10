using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : MonoBehaviour{

    [Header("CONFIGURACION")]
    public float velocidad;
    public float Fuerza;
    public float TiempoMinimoAtaque;
    public float TiempoMaximoAtaque;

    [Header("REFERENCIAS A ESCENA")]
    public GameObject Particula_Muerte;
    public Animator anim_Murcielago;
    public Transform puntoDisparo;

    [Header("REFERENCIAS A PROYECTO")]
    public GameObject Disparo;
        
    [Header("CONSULTA")]
    public BatController code_BatController;
    public Animator anim_Ruta;
    public Transform Jugador;
    public Vector3 posicionOriginal;
    public bool Siguiendo;
    public bool Activo;
    public int RutaActual;
    // Start is called before the first frame update
    void Start(){
        anim_Ruta = this.gameObject.GetComponent<Animator>();
        Jugador = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
        code_BatController = GameObject.Find("BatController").GetComponent<BatController>();
        posicionOriginal = this.transform.position;
    }

    
    public void Perseguir(int camino){
        Activo = true;
        RutaActual = camino;
        StopAllCoroutines();

        switch (camino){
            case 0:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta1");
                break;
            case 1:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta2");
                break;
            case 2:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta3");
                break;
            case 3:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta4");
                break;
            case 4:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta5");
                break;
            case 5:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta6");
                break;
            case 6:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta7");
                break;
            case 7:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta8");
                break;
            case 8:
                anim_Ruta.enabled = true;
                anim_Ruta.Play("Ruta9");
                break;
        }

        Invoke("Atacar", 6.0f);    
    }

    void Atacar(){
        StopAllCoroutines();
        StartCoroutine(corrutina_Ataque());
    }

    IEnumerator corrutina_Ataque(){

        anim_Ruta.enabled = false;

        float TiempoEspera;
        float Tiempo;

        this.transform.LookAt(Jugador);

        anim_Murcielago.Play("Attack");
        yield return new WaitForSeconds(0.7f);
        Disparar();

        while (true){

            TiempoEspera = Random.Range(TiempoMinimoAtaque, TiempoMaximoAtaque);
            Tiempo = 0.0f;

            while (Tiempo < TiempoEspera){
                yield return new WaitForEndOfFrame();
                Tiempo += Time.deltaTime;
                this.transform.LookAt(Jugador);
            }
            anim_Murcielago.Play("Attack");
            yield return new WaitForSeconds(0.7f);
            Disparar();
        }

    }


    void Disparar(){
        GameObject bala = Instantiate(Disparo, puntoDisparo.position, puntoDisparo.rotation);
        bala.GetComponent<Rigidbody>().AddForce(this.transform.forward*Fuerza, ForceMode.Acceleration);
        Destroy(bala, 10.0f);
    }

    void Respawn(){
        StopAllCoroutines();
        anim_Ruta.enabled = false;
        this.transform.position = posicionOriginal;
        code_BatController.EnemigoDestruido(RutaActual);
    }


    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "BalaJugador" && Activo){
            Activo = false;
            Particula_Muerte.gameObject.SetActive(false);
            Particula_Muerte.transform.position = this.transform.position;
            Particula_Muerte.transform.rotation = this.transform.rotation;
            Particula_Muerte.gameObject.SetActive(true);
            GameObject.Find("SFX_BatDeath").GetComponent<AudioSource>().Play();
            GameObject.Find("SFX_BatDeath2").GetComponent<AudioSource>().Play();
            Respawn();
        }
    }


}