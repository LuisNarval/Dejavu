using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour{

    [Header("CONFIGURACION")]
    public float tiempoMinEntreMurcielagos;
    public float tiempoMaxEntreMurcielagos;

    [Header("REFERENCIAS A ESCENA")]
    public Murcielago[] murcielagos;
    public MageController code_MageController;

    [Header("Info. murciélago a enviar")]
    [Header("CONSULTA")]
    public int murcielagoElegido;
    public int rutaElegida;
    public bool[] RutaOcupada;

    [Header("Info. de la oleada actual")]
    public bool EnOleada = false;
    public int cantidadOleada;
    public int maximoPorOleada;

    [Header("Info. enemigos en juego")]
    public int enemigosEnviados = 0;
    public int enemigosDestruidos = 0;
    public int simultaneosActual = 0;
    

    // Start is called before the first frame update
    void Start(){
        RutaOcupada = new bool[9];
        for (int i = 0; i < RutaOcupada.Length; i++) {
            RutaOcupada[i] = false;
        }
        EnOleada = false;
    }

    // Update is called once per frame
    void Update(){
        
    }


    public void InvocarOleada(int cantidad, int maximoSimultaneo){
        cantidadOleada = cantidad;
        maximoPorOleada = maximoSimultaneo;
        StartCoroutine(corrutina_dosificar());
    }

    public void EnemigoDestruido(int rutaLiberada){
        enemigosDestruidos++;
        simultaneosActual--;
        RutaOcupada[rutaLiberada] = false;
    }

    IEnumerator corrutina_dosificar(){
        enemigosEnviados = 0;
        enemigosDestruidos = 0;
        simultaneosActual = 0;
        
        while (enemigosEnviados < cantidadOleada){
            if (simultaneosActual < maximoPorOleada){
                enviarMurcielago();
                yield return new WaitForSeconds(Random.Range(tiempoMinEntreMurcielagos, tiempoMaxEntreMurcielagos));
            }
            yield return new WaitForSeconds(1.0f);
        }

        while (enemigosDestruidos < cantidadOleada){
            yield return new WaitForSeconds(2.0f);
        }

        code_MageController.OleadaDerrotada();
    }





    void enviarMurcielago(){
        bool murcielagosDisponibles = false;
        for (int i = 0; i < murcielagos.Length; i++){
            if (murcielagos[i].Activo == false)
                murcielagosDisponibles = true;
        }

        if (murcielagosDisponibles){
            elegirMurcielago();
            elegirRuta();
            murcielagos[murcielagoElegido].Perseguir(rutaElegida);
            enemigosEnviados++;
            simultaneosActual++;
        }
        else
            Debug.Log("Todos los murcielagos están ocupados");
    }

    void elegirMurcielago(){
        while (true){
            murcielagoElegido = (int)Random.Range(0.0f, 9.0f);
            if (murcielagos[murcielagoElegido].Activo == false) {
                Debug.Log("LIBRE -> Murcielago número : " + murcielagoElegido);
                break;
            }
            else{
                Debug.Log("OCUPADO -> Murcielago número : " + murcielagoElegido);
            }
        }
        murcielagos[murcielagoElegido].Activo = true;
    }

    void elegirRuta(){
        while (true){
            rutaElegida = (int)Random.Range(0.0f, 9.0f);
            if (RutaOcupada[rutaElegida] == false){
                Debug.Log("LIBRE -> Ruta número : " + rutaElegida);
                break;
            }
            else{
                Debug.Log("OCUPADO -> Ruta número : " + rutaElegida);
            }
        }
        RutaOcupada[rutaElegida] = true;
    }


}