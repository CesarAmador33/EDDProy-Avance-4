using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDemo.Estructuras_No_Lineales
{
    public class ArbolBusqueda
    {
        public NodoBinario Raiz;

        // Métodos para insertar, mostrar el árbol, recorrer el árbol (PreOrden, InOrden, PostOrden)

        public NodoBinario RegresaRaiz() => Raiz;

        public void InsertaNodo(int dato, ref NodoBinario raiz)
        {
            if (raiz == null)
            {
                raiz = new NodoBinario(dato);
            }
            else if (dato < raiz.Dato)
            {
                InsertaNodo(dato, ref raiz.HijoIzquierdo);
            }
            else
            {
                InsertaNodo(dato, ref raiz.HijoDerecho);
            }
        }

        // Método para recorrer el árbol por niveles (Amplitud)
        public void RecorrerPorNiveles(NodoBinario raiz)
        {
            if (raiz == null) return;

            Queue<NodoBinario> queue = new Queue<NodoBinario>();
            queue.Enqueue(raiz);

            while (queue.Count > 0)
            {
                NodoBinario nodo = queue.Dequeue();
                strRecorrido += nodo.Dato + " ";

                if (nodo.HijoIzquierdo != null)
                    queue.Enqueue(nodo.HijoIzquierdo);
                if (nodo.HijoDerecho != null)
                    queue.Enqueue(nodo.HijoDerecho);
            }
        }

        // Método para eliminar nodo por predecesor
        public NodoBinario EliminarNodoPredecesor(int valor, ref NodoBinario raiz)
        {
            if (raiz == null) return raiz;

            if (valor < raiz.Dato)
            {
                raiz.HijoIzquierdo = EliminarNodoPredecesor(valor, ref raiz.HijoIzquierdo);
            }
            else if (valor > raiz.Dato)
            {
                raiz.HijoDerecho = EliminarNodoPredecesor(valor, ref raiz.HijoDerecho);
            }
            else
            {
                if (raiz.HijoIzquierdo == null)
                {
                    return raiz.HijoDerecho;
                }
                else if (raiz.HijoDerecho == null)
                {
                    return raiz.HijoIzquierdo;
                }

                NodoBinario predecesor = ObtenerMaximo(raiz.HijoIzquierdo);
                raiz.Dato = predecesor.Dato;
                raiz.HijoIzquierdo = EliminarNodoPredecesor(predecesor.Dato, ref raiz.HijoIzquierdo);
            }

            return raiz;
        }

        // Método para eliminar nodo por sucesor
        public NodoBinario EliminarNodoSucesor(int valor, ref NodoBinario raiz)
        {
            if (raiz == null) return raiz;

            if (valor < raiz.Dato)
            {
                raiz.HijoIzquierdo = EliminarNodoSucesor(valor, ref raiz.HijoIzquierdo);
            }
            else if (valor > raiz.Dato)
            {
                raiz.HijoDerecho = EliminarNodoSucesor(valor, ref raiz.HijoDerecho);
            }
            else
            {
                if (raiz.HijoDerecho == null)
                {
                    return raiz.HijoIzquierdo;
                }
                else if (raiz.HijoIzquierdo == null)
                {
                    return raiz.HijoDerecho;
                }

                NodoBinario sucesor = ObtenerMinimo(raiz.HijoDerecho);
                raiz.Dato = sucesor.Dato;
                raiz.HijoDerecho = EliminarNodoSucesor(sucesor.Dato, ref raiz.HijoDerecho);
            }

            return raiz;
        }

        // Método para obtener el máximo de un subárbol
        private NodoBinario ObtenerMaximo(NodoBinario nodo)
        {
            while (nodo.HijoDerecho != null)
            {
                nodo = nodo.HijoDerecho;
            }
            return nodo;
        }

        // Método para obtener el mínimo de un subárbol
        private NodoBinario ObtenerMinimo(NodoBinario nodo)
        {
            while (nodo.HijoIzquierdo != null)
            {
                nodo = nodo.HijoIzquierdo;
            }
            return nodo;
        }
    }
}