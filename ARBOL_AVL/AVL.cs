using System;
using System.Collections.Generic;
namespace ARBOLES_AVL
{
    class Nodo
    {
        public string valor;
        public Nodo izquierdo;
        public Nodo derecho;
        public int altura ;

        public Nodo(string valor)
        {
            this.valor = valor;
            this.izquierdo = null;
            this.derecho = null;
            this.altura = 1;
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

    private Nodo InsertarNodoRecursivo(Nodo nodo, string valor)
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
               return nodo;
        }

     
        //en las siguientes lineas se incluye el balanceo del arbol AVL

        nodo.altura = 1 + Math.Max(ObtenerAltura(nodo.izquierdo), ObtenerAltura(nodo.derecho));
        int factorEquilibrio = ObtenerFactorEquilibrio(nodo);
        // para casos de rotación
        // Caso Izquierda-Izquierda
        if (factorEquilibrio > 1 && direccion == "izquierdo")
            return RotacionDerecha(nodo);

        // Caso Derecha-Derecha
        if (factorEquilibrio < -1 && direccion == "derecho")
            return RotacionIzquierda(nodo);

        // Caso Izquierda-Derecha
        if (factorEquilibrio > 1 && direccion == "derecho")
        {
            nodo.izquierdo = RotacionIzquierda(nodo.izquierdo);
            return RotacionDerecha(nodo);
        }
        
        // Caso Derecha-Izquierda
        if (factorEquilibrio < -1 && direccion == "izquierdo")
        {
            nodo.derecho = RotacionDerecha(nodo.derecho);
            return RotacionIzquierda(nodo);
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
    return Math.Max(Altura(nodo.izquierdo), Altura(nodo.derecho)) + 1;
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

    //implementacion de metodos requeridos para el arbol AVl
    public int ObtenerAltura(Nodo nodo)
    {
        if (nodo == null)
            return 0;
        return nodo.altura;
    }

    public int ObtenerFactorEquilibrio(Nodo nodo)
    {
        if (nodo == null)
            return 0;
        return ObtenerAltura(nodo.izquierdo) - ObtenerAltura(nodo.derecho);
    }

     public Nodo RotacionDerecha(Nodo y)
    
    {
        Nodo x = y.izquierdo;
        Nodo T2 = x.derecho;

        x.derecho = y;
        y.izquierdo = T2;

        // las siguientes lineas actualizan las alturas de los nodos
        y.altura = Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho)) + 1;
        x.altura = Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho)) + 1;

        return x;
    }

      public Nodo RotacionIzquierda(Nodo x)
    {
        Nodo y = x.derecho;
        Nodo T2 = y.izquierdo;

        y.izquierdo = x;
        x.derecho = T2;

        // Actualizar alturas
        x.altura = Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho)) + 1;
        y.altura = Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho)) + 1;

        return y;
    }
    public bool EstaBalanceado(Nodo nodo)
    {
        if (nodo == null)
            return true;

        int balance = ObtenerFactorEquilibrio(nodo);
        
        if (Math.Abs(balance) > 1)
            return false;

        return EstaBalanceado(nodo.izquierdo) && EstaBalanceado(nodo.derecho);
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

        // Actualizar altura
        raiz.altura = 1 + Math.Max(ObtenerAltura(raiz.izquierdo), ObtenerAltura(raiz.derecho));

        // Rebalancear
        int balance = ObtenerFactorEquilibrio(raiz);

        // Casos de rotación
        if (balance > 1 && ObtenerFactorEquilibrio(raiz.izquierdo) >= 0)
            return RotacionDerecha(raiz);

        if (balance > 1 && ObtenerFactorEquilibrio(raiz.izquierdo) < 0)
        {
            raiz.izquierdo = RotacionIzquierda(raiz.izquierdo);
            return RotacionDerecha(raiz);
        }

        if (balance < -1 && ObtenerFactorEquilibrio(raiz.derecho) <= 0)
            return RotacionIzquierda(raiz);

        if (balance < -1 && ObtenerFactorEquilibrio(raiz.derecho) > 0)
        {
            raiz.derecho = RotacionDerecha(raiz.derecho);
            return RotacionIzquierda(raiz);
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
}