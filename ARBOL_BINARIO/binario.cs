using System;
using System.Collections.Generic;
namespace ARBOLES_BINARIO;
class Nodo
{
    public string valor;
    public Nodo izquierdo;
    public Nodo derecho;

    public Nodo(string valor)
    {
        this.valor = valor;
        this.izquierdo = null;
        this.derecho = null;
    }
}

class ArbolBinario
{
    public Nodo raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    public void InsertarNodo(string valor)
    {
        if (raiz == null)
        {
            raiz = new Nodo(valor);
        }
        else
        {
            raiz = InsertarNodoRecursivo(raiz, valor);
        }
    }

    public Nodo InsertarNodoRecursivo(Nodo nodo, string valor)
    {
        if (nodo == null)
        {
            return new Nodo(valor);
        }
        Console.WriteLine($"¿Dónde desea insertar el nodo {valor}? (izquierdo/derecho) de {nodo.valor}");
        string direccion = Console.ReadLine().ToLower();

        if (direccion == "izquierdo")
        {
            nodo.izquierdo = InsertarNodoRecursivo(nodo.izquierdo, valor);
        }
        else if (direccion == "derecho")
        {
            nodo.derecho = InsertarNodoRecursivo(nodo.derecho, valor);
        }
        else
        {
            Console.WriteLine("Dirección no válida. Intente de nuevo.");
        }

        return nodo;
    }

    public void PreOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.valor + " ");
            PreOrden(nodo.izquierdo);
            PreOrden(nodo.derecho);
        }
    }

    public void InOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            InOrden(nodo.izquierdo);
            Console.Write(nodo.valor + " ");
            InOrden(nodo.derecho);
        }
    }

    public void PostOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            PostOrden(nodo.izquierdo);
            PostOrden(nodo.derecho);
            Console.Write(nodo.valor + " ");
        }
    }
    public void GraficarArbol(Nodo raiz)
    {
    if (raiz == null)
        return;
        int altura = Altura(raiz);
        Queue<Nodo> cola = new Queue<Nodo>();
        cola.Enqueue(raiz);

        for (int nivel = 0; nivel < altura; nivel++)
        {
            int nivelNodos = cola.Count;
            int espacios = (int)Math.Pow(2, altura - nivel - 1) - 1;

            for (int i = 0; i < nivelNodos; i++)
            {
                Nodo actual = cola.Dequeue();

                if (i == 0)
                    Console.Write(new string(' ', espacios));

                Console.Write(actual.valor);
                if (i < nivelNodos - 1)
                    Console.Write(new string(' ', espacios * 2 + 1));

                if (actual.izquierdo != null)
                    cola.Enqueue(actual.izquierdo);
                else
                    cola.Enqueue(new Nodo(" "));

                if (actual.derecho != null)
                    cola.Enqueue(actual.derecho);
                else
    
                    cola.Enqueue(new Nodo(" "));
        }
            Console.WriteLine();
   }
}


    public int Altura(Nodo nodo)
    {
        if (nodo == null)
        {
            return 0;
        }
        else
        {
            int alturaIzquierda = Altura(nodo.izquierdo);
            int alturaDerecha = Altura(nodo.derecho);
            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }
    }

    public int Grado(Nodo nodo)
    {
        if (nodo == null)
            return 0;
        int gradoActual = (nodo.izquierdo != null ? 1 : 0) + (nodo.derecho != null ? 1 : 0);
        return Math.Max(gradoActual, Math.Max(Grado(nodo.izquierdo), Grado(nodo.derecho)));


    }

    public int NodosPorNivel(Nodo nodo, int nivel)
    {
        if (nodo == null)
        {
            return 0;
        }

        Queue<Nodo> cola = new Queue<Nodo>();
        cola.Enqueue(nodo);
        int nivelActual = 1;

        while (cola.Count > 0)
        {
            int nivelNodos = cola.Count;
            if (nivelActual == nivel)
            {
                return nivelNodos;
            } for (int i = 0; i < nivelNodos; i++)
            {
                Nodo actual = cola.Dequeue();
                if (actual.izquierdo != null)
                {
                    cola.Enqueue(actual.izquierdo);
                }
                if (actual.derecho != null)
                {
                    cola.Enqueue(actual.derecho);
                }
            }
            nivelActual++;
        }

        return 0;
    }

    public int NivelDeNodo(Nodo nodo, string valor, int nivel)
    {
        if (nodo == null)
        {
            return 0;
        }
        if (nodo.valor == valor)
        {
            return nivel;
        }

        int nivelIzquierdo = NivelDeNodo(nodo.izquierdo, valor, nivel + 1);
        if (nivelIzquierdo != 0)
        {
            return nivelIzquierdo;
        }

        int nivelDerecho = NivelDeNodo(nodo.derecho, valor, nivel + 1);
        return nivelDerecho;
    }

        public Nodo BuscarNodo(Nodo nodo, string valor)
    {
        if (nodo == null)
        {
            return null;
        }
        if (nodo.valor == valor)
        {
            return nodo;
        }
        Nodo encontrado = BuscarNodo(nodo.izquierdo, valor);
        if (encontrado == null)
        {
            encontrado = BuscarNodo(nodo.derecho, valor);
        }
        return encontrado;
            }
        public Nodo EliminarNodo(Nodo raiz, string valor)
        {
            if (raiz == null)
                return raiz;

            if (raiz.valor == valor)
            {
                // Nodo con un solo hijo o sin hijos
                if (raiz.izquierdo == null)
                    return raiz.derecho;
                else if (raiz.derecho == null)
                    return raiz.izquierdo;

                // Nodo con dos hijos
                Nodo sucesor = ObtenerMinimo(raiz.derecho);
                raiz.valor = sucesor.valor;
                raiz.derecho = EliminarNodo(raiz.derecho, sucesor.valor);
            }
            else
            {
                raiz.izquierdo = EliminarNodo(raiz.izquierdo, valor);
                raiz.derecho = EliminarNodo(raiz.derecho, valor);
            }

            return raiz;
        }

        private Nodo ObtenerMinimo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.izquierdo != null)
                actual = actual.izquierdo;
            return actual;
        }
}

