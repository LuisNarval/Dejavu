using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageController : MonoBehaviour{

    [Header("CONFIGURACION")]
    [Header("Fuerza Disparos")]
    public float fuerzaDisparo;
    public float fuerzaProyectil;

    [Header("Barra de vida")]
    public int Vida_Round1;
    public int Vida_Round2;
    public int Vida_Round3;

    [Header("Oleadas Murcielagos")]
    public Vector2 BatWave1;
    public Vector2 BatWave2;
    public Vector2 BatWave3;


    [Header("Objetos")]
    [Header("REFERENCIAS A ESCENA")]
    public GameObject escudo;
    public Image img_Vida;

    [Header("Códigos")]
    public BatController code_BatController;
    public Mirar code_Mirar;

    [Header("Animaciones")]
    public Animator anim_MovimientoMago;
    public Animator anim_AtaquesMago;

    [Header("Posiciones")]
    public Transform posicionLibro;
    public Transform posicionCampana;

    [Header("AudioSource")]
    public AudioSource SFX_Danio1;
    public AudioSource SFX_Danio2;

    [Header("Disparos")]
    [Header("REFERENCIAS A PROYECTO")]
    public GameObject Disparo_Antimateria;
    public GameObject Disparo_Proton;
    public GameObject Disparo_Fireball;
    public Material mat_Mago;
    public AudioClip[] clips_Danio;

    [Header("CONSULTAS")]
    public int OleadaDeMurcielagos;
    public int Vida;

    // Start is called before the first frame update
    void Start(){
        inicializar();
    }

    void inicializar(){
        OleadaDeMurcielagos = 0;
        escudo.SetActive(false);
        anim_MovimientoMago.Play("MagoMov_AlFrente");
        anim_AtaquesMago.SetBool("FlyForward", false);
    }

    public void ComenzarPelea(){
        StartCoroutine(corrutina_iniciarPelea());
    }


    public void OleadaDerrotada(){
        switch (OleadaDeMurcielagos){
            case 1:
                StartCoroutine(corrutina_Round1());
            break;
            case 2:
                StartCoroutine(corrutina_Round2());
            break;
            case 3:
                StartCoroutine(corrutina_Round3());
            break;
        }
    }

    IEnumerator corrutina_iniciarPelea(){
        yield return StartCoroutine(corrutina_Ocultarse());
        SiguienteOleadaMurcielagos();
    }

    float vidaCompleta;
    IEnumerator corrutina_Round1(){
        yield return StartCoroutine(corrutina_Mostrarse());
        
        Vida = Vida_Round1;
        vidaCompleta = Vida;
        img_Vida.fillAmount = 1.0f;
        StartCoroutine(corrutina_Ataques(2.0f, 4.0f));
        StartCoroutine(corrutina_revisarVida());

        yield return new WaitForSeconds(0.5f);
        while (Vida > 0){
            switch((int) Random.Range(0.0f, 3.99f)){
                case 0:
                    anim_MovimientoMago.SetTrigger("LadoALado");
                    yield return new WaitForSeconds(Random.Range(5.0f,8.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(1.0f);
                break;

                case 1:
                    anim_MovimientoMago.SetTrigger("MediaLuna");
                    yield return new WaitForSeconds(Random.Range(8.0f, 12.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(2.0f);
                break;
                
                case 2:
                    anim_MovimientoMago.SetTrigger("CuadrilateroD");
                    yield return new WaitForSeconds(5.0f);
                break;
            
                case 3:
                    anim_MovimientoMago.SetTrigger("CuadrilateroI");
                    yield return new WaitForSeconds(5.0f);
                break;

            }
        }
    }


    IEnumerator corrutina_Round2(){
        yield return StartCoroutine(corrutina_Mostrarse());

        Vida = Vida_Round2;
        vidaCompleta = Vida;
        img_Vida.fillAmount = 1.0f;
        StartCoroutine(corrutina_Ataques(1.5f, 3.5f));
        StartCoroutine(corrutina_revisarVida());

        yield return new WaitForSeconds(0.5f);
        while (Vida > 0){
            switch ((int)Random.Range(0.0f, 5.99f)){
                case 0:
                    anim_MovimientoMago.SetTrigger("MediaLuna");
                    yield return new WaitForSeconds(Random.Range(5.0f, 8.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(2.0f);
                    break;

                case 1:
                    anim_MovimientoMago.SetTrigger("MediaLuna");
                    yield return new WaitForSeconds(Random.Range(8.0f, 12.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(2.0f);
                    break;

                case 2:
                    anim_MovimientoMago.SetTrigger("MovSD");
                    yield return new WaitForSeconds(9.0f);
                    break;

                case 3:
                    anim_MovimientoMago.SetTrigger("CuadrilateroI");
                    yield return new WaitForSeconds(5.0f);
                    break;

                case 4:
                    anim_MovimientoMago.SetTrigger("DobleGiroI");
                    yield return new WaitForSeconds(16.0f);
                    break;

                case 5:
                    anim_MovimientoMago.SetTrigger("DobleGiroD");
                    yield return new WaitForSeconds(8.0f);
                    break;
            }
        }
    }

    IEnumerator corrutina_Round3(){
        yield return StartCoroutine(corrutina_Mostrarse());

        Vida = Vida_Round3;
        vidaCompleta = Vida;
        img_Vida.fillAmount = 1.0f;
        StartCoroutine(corrutina_Ataques(1.0f, 2.0f));
        StartCoroutine(corrutina_revisarVida());

        yield return new WaitForSeconds(0.5f);
        while (Vida > 0){
            switch ((int)Random.Range(0.0f, 7.99f)){
                case 0:
                    anim_MovimientoMago.SetTrigger("LadoALado");
                    yield return new WaitForSeconds(Random.Range(5.0f, 8.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(1.0f);
                    break;

                case 1:
                    anim_MovimientoMago.SetTrigger("MediaLuna");
                    yield return new WaitForSeconds(Random.Range(8.0f, 12.0f));
                    anim_MovimientoMago.SetTrigger("IrAlFrente");
                    yield return new WaitForSeconds(2.0f);
                    break;

                case 2:
                    anim_MovimientoMago.SetTrigger("CuadrilateroD");
                    yield return new WaitForSeconds(5.0f);
                    break;

                case 3:
                    anim_MovimientoMago.SetTrigger("CuadrilateroI");
                    yield return new WaitForSeconds(5.0f);
                    break;

                case 4:
                    anim_MovimientoMago.SetTrigger("DobleGiroD");
                    yield return new WaitForSeconds(8.0f);
                    break;

                case 5:
                    anim_MovimientoMago.SetTrigger("DobleGiroI");
                    yield return new WaitForSeconds(16.0f);
                    break;

                case 6:
                    anim_MovimientoMago.SetTrigger("MovSD");
                    yield return new WaitForSeconds(9.0f);
                    break;

                case 7:
                    anim_MovimientoMago.SetTrigger("MovSI");
                    yield return new WaitForSeconds(9.0f);
                    break;
            }
        }
    }








    IEnumerator corrutina_Ataques(float tiempoMin, float tiempoMax){
        while (true){

            yield return new WaitForSeconds(Random.Range(tiempoMin,tiempoMax));

            GameObject proyectil;

            switch ( (int) Random.Range(0.0f, 1.99f)){
                case 0:
                    anim_AtaquesMago.SetTrigger("MeleeAttack");
                    yield return new WaitForSeconds(0.63f);

                    proyectil = Instantiate(Disparo_Fireball, posicionLibro.position, posicionLibro.rotation);
                    proyectil.tag = "DisparoEnemigo";
                    proyectil.GetComponent<Rigidbody>().AddForce(posicionLibro.forward * fuerzaProyectil, ForceMode.Acceleration);
                    Destroy(proyectil, 10.0f);
                break;

                case 1:
                    anim_AtaquesMago.SetTrigger("ThrowProjectile");
                    yield return new WaitForSeconds(0.73f);

                    if ((int)Random.Range(0.0f, 1.99f) < 1)
                        proyectil = Instantiate(Disparo_Antimateria, posicionCampana.position, posicionCampana.rotation);
                    else
                        proyectil = Instantiate(Disparo_Proton, posicionCampana.position, posicionCampana.rotation);

                    proyectil.tag = "DisparoEnemigo";
                    proyectil.GetComponent<Rigidbody>().AddForce(posicionCampana.forward * fuerzaDisparo, ForceMode.Acceleration);
                    Destroy(proyectil, 10.0f);
                break;
            }

        }
    }
    IEnumerator corrutina_Ocultarse(){
        yield return new WaitForSeconds(1.0f);
        code_Mirar.enabled = false;
        anim_AtaquesMago.SetTrigger("Summon");
        anim_MovimientoMago.SetTrigger("GirarEje");
        yield return new WaitForSeconds(0.5f);
        escudo.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        anim_AtaquesMago.SetBool("FlyForward", true);
        anim_MovimientoMago.SetTrigger("IrAtras");
    }
    IEnumerator corrutina_Mostrarse() {
        yield return new WaitForSeconds(1.0f);
        anim_MovimientoMago.SetTrigger("IrAlFrente");
        yield return new WaitForSeconds(3.0f);
        anim_AtaquesMago.SetBool("FlyForward", false);
        anim_AtaquesMago.SetTrigger("Summon");
        anim_MovimientoMago.SetTrigger("GirarEje");
        yield return new WaitForSeconds(1.75f);
        escudo.SetActive(false);
        code_Mirar.enabled = true;
    }




    //RECIBIR DAÑO
    //----------------------------------------------------------------------------------
    public void Golpeado(){
        Vida --;
        img_Vida.fillAmount = (float)Vida / vidaCompleta;
        //anim_AtaquesMago.Play("TakeDamage");
        StopCoroutine("corrutina_demostrarDanio");
        StartCoroutine("corrutina_demostrarDanio");
        SFX_Danio1.Play();
        SFX_Danio2.clip = clips_Danio[(int)Random.Range(0.0f,3.99f)];
        SFX_Danio2.Play();
    }

    float tiempoDanio = 0.0f;
    IEnumerator corrutina_demostrarDanio(){
        while (tiempoDanio < 1.0f){
            tiempoDanio += Time.deltaTime*4;
            mat_Mago.color = new Color(1.0f,1.0f-tiempoDanio,1.0f-tiempoDanio);
            yield return new WaitForEndOfFrame();
        }
        while (tiempoDanio > 0.0f){
            tiempoDanio -= Time.deltaTime*4;
            mat_Mago.color = new Color(1.0f, 1.0f-tiempoDanio, 1.0f-tiempoDanio);
            yield return new WaitForEndOfFrame();
        }
    }
    //-----------------------------------------------------------------------------------


    IEnumerator corrutina_revisarVida(){
        while (Vida > 0){
            yield return new WaitForSeconds(1.0f);
        }
        MagoDerrotado();
    }

    void MagoDerrotado(){
        StopAllCoroutines();
        mat_Mago.color = Color.white;
        StartCoroutine(corrutina_Retirada());
    }

    IEnumerator corrutina_Retirada(){
        anim_MovimientoMago.SetTrigger("Retirada");
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(corrutina_Ocultarse());
        SiguienteOleadaMurcielagos();
    }

    void SiguienteOleadaMurcielagos(){
        OleadaDeMurcielagos++;

        switch (OleadaDeMurcielagos){
            case 1:
                code_BatController.InvocarOleada((int)BatWave1.x, (int)BatWave1.y);
                break;
            case 2:
                code_BatController.InvocarOleada((int)BatWave2.x, (int)BatWave2.y);
                break;
            case 3:
                code_BatController.InvocarOleada((int)BatWave3.x, (int)BatWave3.y);
            break;
            case 4:
                Debug.Log("FIN DEL JUEGO");
            break;
        }
    }

}