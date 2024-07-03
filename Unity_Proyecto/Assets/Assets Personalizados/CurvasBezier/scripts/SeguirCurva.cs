using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CurvasBezier
{
    [RequireComponent(typeof(Rigidbody))]
    public class SeguirCurva : MonoBehaviour
    {
        /// <summary>
        /// Rigidbody del objeto
        /// </summary>
        Rigidbody rb;

        /// <summary>
        /// Una lista de los caminos que va a seguir el objeto
        /// </summary>
        public List<CurvaBezier> camino;

        /// <summary>
        /// El tiempo actual que tiene el objeto moviendose en una curva
        /// </summary>
        float tiempo = 0;

        int indiceCamino = 0;
        /// <summary>
        /// Booleano que nos dice se el objeto se esta moviendo
        /// </summary>
        bool moviendoseEnCurva = false;
        /// <summary>
        /// Booleano que nos dice si el objeto esta siguiendo la ruta
        /// </summary>
        [HideInInspector]
        public bool siguiendoRuta = false;
        /// <summary>
        /// Velocidad que tiene el objeto al salir de la curva
        /// </summary>
        Vector3 vectorVelocidadAlSalirCurva;

        Coroutine corrutinaMoverCamino;

        /// <summary>
        /// Tangente de la curva en ese tiempo
        /// </summary>
        public Vector3 GetTangente()
        {
            //  Ubicacion de los nodos
            Vector3 P0 = camino[indiceCamino].transform.GetChild(0).position;
            Vector3 P1 = camino[indiceCamino].transform.GetChild(1).position;
            Vector3 P2 = camino[indiceCamino].transform.GetChild(2).position;
            Vector3 P3 = camino[indiceCamino].transform.GetChild(3).position;
            return camino[indiceCamino].DerivarCurva(P0, P1, P2, P3, tiempo).normalized;
        }

        #region Velocidades
        [Header("Velocidades")]
        /// <summary>
        /// Velocidad que se toma al entrar en una curva
        /// </summary>
        [Range(0.01f, 20f)]
        public float velocidadEntrarCurva = 0.3f;
        /// <summary>
        /// Velocidad que se toma al salir en una curva
        /// </summary>
        [Range(0.01f, 20f)]
        public float velocidadSalirCurva = 1f;
        #endregion

        #region Opciones
        [Header("Opciones")]
        /// <summary>
        /// Reproducir el movimiento al iniciar la aplicacion
        /// </summary>
        public bool reproducirEnStart = true;
        /// <summary>
        /// El movimiento del objeto esta el loop.
        /// </summary>
        public bool bucle = true;
        /// <summary>
        /// Nos dice si al moverse su forward apunta a la tangente de la curva, de esta forma el objeto apunta a la direccion que se mueve.
        /// </summary>
        public bool rotacionDinamica = false;
        /// <summary>
        /// Pausa el movimiento del objeto en la curva
        /// </summary>
        [HideInInspector]
        public bool pausa = false;
        #endregion

        void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        void Start()
        {
            indiceCamino = 0;
            tiempo = 0;

            if (reproducirEnStart)
            {
                ReproducirMovimiento();
            }
            else
            {
                moviendoseEnCurva = false;
                siguiendoRuta = false;
            }
        }
        void FixedUpdate()
        {
            if (camino.Count > 0 && !moviendoseEnCurva && siguiendoRuta)
            {
                ComenzarCamino();
            }
        }

        /// <summary>
        /// Se asignan las configuraciones
        /// </summary>
        /// <param name="bucle">El objeto se mueve en bucle</param>
        /// <param name="iniciarCamino">Empieza a reproducirse</param>
        public void AsignarConfiguraciones(float velocidadEntrarCurva = 0.3f, float velocidadSalirCurva = 2, bool reproducirEnStart = true, bool bucle = true, bool rotacionDinamica = true)
        {
            this.velocidadEntrarCurva = velocidadEntrarCurva;
            this.velocidadSalirCurva = velocidadSalirCurva;
            this.reproducirEnStart = reproducirEnStart;
            this.bucle = bucle;
            this.rotacionDinamica = rotacionDinamica;
        }
        /// <summary>
        /// Asignacion de caminos
        /// </summary>
        /// <param name="camino"></param>
        public void AsignarCamino(ListaCaminos caminos) => camino = caminos.caminos;
        /// <summary>
        /// Si no esta moviendose en una curva y hay caminos por recorrer entonces empieza a moverse
        /// </summary>
        void ComenzarCamino()
        {
            corrutinaMoverCamino = StartCoroutine(MoverCamino(indiceCamino));
            indiceCamino = (indiceCamino + 1) % camino.Count;
        }
        /// <summary>
        /// Si el objeto se esta moviendo en el camino entonces se detiene y este sigue avanzando con direccion de la tangente del punto donde se detiene
        /// </summary>
        public void SalirCamino()
        {
            //  Detener seguimiento de camino
            if (corrutinaMoverCamino != null)
            {
                StopCoroutine(corrutinaMoverCamino);
            }
            corrutinaMoverCamino = null;
            //  Ya no se mueve en la curva y por ende ya no sigue la ruta
            moviendoseEnCurva = false;
            siguiendoRuta = false;
            //  Irse derecho
            int indice = indiceCamino - 1 < 0 ? camino.Count - 1 : indiceCamino - 1;
            Vector3 P0 = camino[indice].transform.GetChild(0).position;
            Vector3 P1 = camino[indice].transform.GetChild(1).position;
            Vector3 P2 = camino[indice].transform.GetChild(2).position;
            Vector3 P3 = camino[indice].transform.GetChild(3).position;
            vectorVelocidadAlSalirCurva = camino[indice].DerivarCurva(P0, P1, P2, P3, tiempo).normalized;
            rb.velocity = vectorVelocidadAlSalirCurva;
        }
        /// <summary>
        /// El objeto se mueve en una curva dada por su indice en una lista
        /// </summary>
        /// <param name="indiceCamino">la curva en la que se va a mover</param>
        /// <returns></returns>
        IEnumerator MoverCamino(int indiceCamino)
        {
            //  Ubicacion de los nodos
            Vector3 P0 = camino[indiceCamino].transform.GetChild(0).position;
            Vector3 P1 = camino[indiceCamino].transform.GetChild(1).position;
            Vector3 P2 = camino[indiceCamino].transform.GetChild(2).position;
            Vector3 P3 = camino[indiceCamino].transform.GetChild(3).position;
            //  Inicializacion de valores
            tiempo = 0;
            moviendoseEnCurva = true;
            //  Movimiento
            while (tiempo < 1)
            {
                //  Pausar y reanudar movimiento en el camino
                yield return new WaitUntil(() => !pausa);
                //  Moverse siguiendo una trayectoria
                tiempo += Time.deltaTime * velocidadEntrarCurva;
                rb.MovePosition(camino[indiceCamino].Curva(P0, P1, P2, P3, tiempo));
                //  Forward del objeto apuntando a la tangente de la curva
                rb.gameObject.transform.forward = rotacionDinamica ? camino[indiceCamino].DerivarCurva(P0, P1, P2, P3, tiempo).normalized : Vector3.forward;

                yield return new WaitForFixedUpdate();
            }
            //  seguir siguiente camino
            if (indiceCamino < camino.Count - 1) moviendoseEnCurva = false;
            //  repetir bucle al terminar de recorrer todos los caminos
            else if (bucle)
            {
                rb.MovePosition(camino[0].transform.GetChild(0).position);
                moviendoseEnCurva = false;
            }
            //  no bucle => seguir derecho
            else
            {
                vectorVelocidadAlSalirCurva = camino[indiceCamino].DerivarCurva(P0, P1, P2, P3, 1).normalized * velocidadSalirCurva;
                rb.velocity = vectorVelocidadAlSalirCurva;
                moviendoseEnCurva = false;
                siguiendoRuta = false;
            }
        }
        #region Pausa, play y reiniciar
        public void PausarMovimiento()
        {
            rb.velocity = Vector3.zero;
            pausa = true;
        }

        public void ReproducirMovimiento()
        {
            if (pausa)
            {
                pausa = false;
                //  Si se salio del camino y esta pausado, al reanudarlo el objeto se vuelve a mover
                if (moviendoseEnCurva == false && moviendoseEnCurva == false)
                {
                    rb.velocity = vectorVelocidadAlSalirCurva;
                }
            }
            else
            {
                pausa = false;
                moviendoseEnCurva = false;
                siguiendoRuta = true;

                ComenzarCamino();
            }
        }
        public void ReiniciarMovimiento()
        {
            indiceCamino = 0;

            moviendoseEnCurva = false;
            siguiendoRuta = true;
            pausa = false;
            if (corrutinaMoverCamino != null)
            {
                StopCoroutine(corrutinaMoverCamino);
            }
            ReproducirMovimiento();
        }
        #endregion
    }
}
