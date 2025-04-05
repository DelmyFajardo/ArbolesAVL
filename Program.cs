using System;
using ARBOLES_AVL;
using System.Collections.Generic;
using System.Diagnostics;  
using ARBOLES_BINARIO;
using System.ComponentModel;

namespace ARBOLES_AVL
{
   class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Árbol Binario");
                Console.WriteLine("2. Árbol AVL");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        EjecutarArbolBinario();
                        break;
                    case "2":
                        EjecutarArbolAVL();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        static void EjecutarArbolBinario()
        {
            ArbolBinario arbol = new ArbolBinario();
            Console.WriteLine("\n=== ÁRBOL BINARIO ===");
            
            // Insertar nodos
            while (true)
            {
                Console.Write("Ingrese valor del nodo (fin para terminar): ");
                string valor = Console.ReadLine();
                if (valor.ToLower() == "fin") break;
                arbol.InsertarNodo(valor);
            }

            // Menú árbol binario
            while (true)
            {
                Console.WriteLine("\n=== OPERACIONES ÁRBOL BINARIO ===");
                Console.WriteLine("1. Insertar nuevo nodo");
                Console.WriteLine("2. Recorrido preorden");
                Console.WriteLine("3. Recorrido inorden");
                Console.WriteLine("4. Recorrido postorden");
                Console.WriteLine("5. Mostrar árbol");
                Console.WriteLine("6. Ver altura");
                Console.WriteLine("7. Ver grado");
                Console.WriteLine("8. Buscar valor");
                Console.WriteLine("9. Eliminar nodo");
                Console.WriteLine("10. Volver al menú principal");
                
                string opcion = Console.ReadLine();
                if (opcion == "10") break;
                
                EjecutarOperacionArbol(arbol, opcion, false);
            }
        }

        static void EjecutarArbolAVL()
        {
            ArbolBinario arbol = new ArbolBinario();
            Console.WriteLine("\n=== ÁRBOL AVL ===");
            
            // Insertar nodos
            while (true)
            {
                Console.Write("Ingrese valor del nodo (fin para terminar): ");
                string valor = Console.ReadLine();
                if (valor.ToLower() == "fin") break;
                arbol.InsertarNodo(valor);
                
                Console.WriteLine(arbol.EstaBalanceado(arbol.raiz) ? 
                    "Árbol balanceado" : 
                    "Rebalanceando árbol...");
            }

            // Menú árbol AVL
            while (true)
            {
                Console.WriteLine("\n=== OPERACIONES ÁRBOL AVL ===");
                Console.WriteLine("1. Insertar nuevo nodo");
                Console.WriteLine("2. Recorrido preorden");
                Console.WriteLine("3. Recorrido inorden");
                Console.WriteLine("4. Recorrido postorden");
                Console.WriteLine("5. Mostrar árbol");
                Console.WriteLine("6. Ver altura");
                Console.WriteLine("7. Ver grado");
                Console.WriteLine("8. Buscar valor");
                Console.WriteLine("9. Verificar balance");
                Console.WriteLine("10. Eliminar nodo");
                Console.WriteLine("11. Volver al menú principal");
                
                string opcion = Console.ReadLine();
                if (opcion == "11") break;
                
                EjecutarOperacionArbol(arbol, opcion, true);
            }
        }

        static void EjecutarOperacionArbol(ArbolBinario arbol, string opcion, bool esAVL)
        {
            var sw = new Stopwatch();
            switch (opcion)
            {
                case "1": // Insertar nuevo nodo
                    Console.Write("\nIngrese el valor del nuevo nodo: ");
                    string nuevoValor = Console.ReadLine();
                    sw.Restart();
                    arbol.InsertarNodo(nuevoValor);
                    sw.Stop();
                    Console.WriteLine($"Nodo insertado. Tiempo: {sw.ElapsedMilliseconds}ms");
                    if (esAVL)
                    {
                        Console.WriteLine(arbol.EstaBalanceado(arbol.raiz) ? 
                            "Árbol balanceado después de inserción" : 
                            "Rebalanceando árbol...");
                    }
                    break;

                case "2": // Recorrido preorden
                    Console.WriteLine("\nRecorrido preorden:");
                    sw.Restart();
                    arbol.PreOrden(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nTiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "3": // Recorrido inorden
                    Console.WriteLine("\nRecorrido inorden:");
                    sw.Restart();
                    arbol.InOrden(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nTiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "4": // Recorrido postorden
                    Console.WriteLine("\nRecorrido postorden:");
                    sw.Restart();
                    arbol.PostOrden(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nTiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "5": // Mostrar árbol
                    Console.WriteLine("\nRepresentación del árbol:");
                    sw.Restart();
                    arbol.GraficarArbol(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nTiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "6": // Ver altura
                    sw.Restart();
                    int altura = arbol.Altura(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nAltura: {altura}");
                    Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "7": // Ver grado
                    sw.Restart();
                    int grado = arbol.Grado(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine($"\nGrado: {grado}");
                    Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds}ms");
                    break;

                case "8": // Buscar valor
                    Console.Write("\nValor a buscar: ");
                    string valorBuscar = Console.ReadLine();
                    sw.Restart();
                    var nodo = arbol.BuscarNodo(arbol.raiz, valorBuscar);
                    sw.Stop();
                    if (nodo != null)
                    {
                        int nivel = arbol.NivelDeNodo(arbol.raiz, valorBuscar, 1);
                        Console.WriteLine($"Encontrado en nivel {nivel}");
                        Console.WriteLine($"Tiempo de búsqueda: {sw.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        Console.WriteLine("No encontrado");
                        Console.WriteLine($"Tiempo de búsqueda: {sw.ElapsedMilliseconds}ms");
                    }
                    break;

                case "9" when esAVL: // Verificar balance (solo AVL)
                    sw.Restart();
                    bool estaBalanceado = arbol.EstaBalanceado(arbol.raiz);
                    sw.Stop();
                    Console.WriteLine(estaBalanceado ? 
                        "\nÁrbol balanceado" : 
                        "\nÁrbol no balanceado");
                    Console.WriteLine($"Tiempo de verificación: {sw.ElapsedMilliseconds}ms");
                    break;

                case "10" when esAVL: // Eliminar nodo (AVL)
                case "9" when !esAVL: // Eliminar nodo (Binario)
                    Console.Write("\nIngrese el valor a eliminar: ");
                    string valorEliminar = Console.ReadLine();
                    sw.Restart();
                    arbol.raiz = arbol.EliminarNodo(arbol.raiz, valorEliminar);
                    sw.Stop();
                    Console.WriteLine($"Nodo eliminado. Tiempo: {sw.ElapsedMilliseconds}ms");
                    if (esAVL)
                    {
                        Console.WriteLine(arbol.EstaBalanceado(arbol.raiz) ? 
                            "Árbol balanceado después de eliminación" : 
                            "Rebalanceando árbol...");
                    }
                    break;

                default:
                    Console.WriteLine("\nOpción no válida");
                    break;
            }
            Console.WriteLine();
}
        static void RealizarPruebasRendimiento()
        {
                    const int NUMERO_ELEMENTOS = 10000;
                    Random random = new Random();
                    List<string> valores = new List<string>();

                    Console.WriteLine("\nGenerando datos de prueba...");
                    for (int i = 0; i < NUMERO_ELEMENTOS; i++)
                    {
                        valores.Add(random.Next(1, 1000000).ToString());
                    }

                    var sw = new Stopwatch();
                    var arbolBinario = new ARBOLES_BINARIO.ArbolBinario();
                    var arbolAVL = new ARBOLES_AVL.ArbolBinario();

                    // Pruebas árbol binario
                    Console.WriteLine("\n=== PRUEBAS ÁRBOL BINARIO ===");
                    MedirRendimiento(arbolBinario, valores, sw);

                    // Pruebas árbol AVL
                    Console.WriteLine("\n=== PRUEBAS ÁRBOL AVL ===");
                    MedirRendimiento(arbolAVL, valores, sw);

                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
        }
        static void MedirRendimiento(dynamic arbol, List<string> valores, Stopwatch sw)
        {
            sw.Restart();
            foreach (var valor in valores)
            {
                arbol.InsertarNodo(valor);
            }
            sw.Stop();
            Console.WriteLine($"Tiempo de inserción: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Altura final: {arbol.Altura(arbol.raiz)}");
        }
    }
}