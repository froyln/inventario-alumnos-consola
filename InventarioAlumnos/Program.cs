using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InventarioAlumnos {
    internal class Program {

        static int select = 0;
        static string carpeta = Directory.GetCurrentDirectory() + "/Alumnos";
        static string archivo = carpeta + "/alumnos.txt";
        static string carpetaConf = Directory.GetCurrentDirectory() + "/Configuracion";
        static string archivoColorFondo = carpetaConf + "/ColorFondo.txt";
        static string archivoColorSeleccion = carpetaConf + "/ColorSeleccion.txt";
        static int color = 9;
        static int colorSeleccion = 12;

        static void Main(string[] args) {
            //Verificar si existe la carpeta de configuracion
            if (!Directory.Exists(carpetaConf))
            {
                Directory.CreateDirectory(carpetaConf);
            }

            //Verificar si existen los archivos de configuracion
            if (!File.Exists(archivoColorFondo))
            {
                File.Create(archivoColorFondo).Close();
                StreamWriter sw = new StreamWriter(archivoColorFondo);
                sw.Write("9");
                sw.Close();
            }

            if (!File.Exists(archivoColorSeleccion))
            {
                File.Create(archivoColorSeleccion).Close();
                StreamWriter sw = new StreamWriter(archivoColorSeleccion);
                sw.Write("12");
                sw.Close();
            }

            using (StreamReader sr = new StreamReader(archivoColorFondo))
                color = int.Parse(sr.ReadLine());
            using (StreamReader sr1 = new StreamReader(archivoColorSeleccion))
                colorSeleccion = int.Parse(sr1.ReadLine());

            //Opciones del menu
            string[] opciones =
                  { "Agregar Alumno             ",
                    "Buscar Alumno              ",
                    "Modificar Alumno           ",
                    "Eliminar Alumno            ",
                    "Lista de Alumnos           ",
                    "Configuracion              ",
                    "Salir                      " };
            int posicionDeMenuX = 4;
            int posicionDeMenuY = 1;
            int posicionX = Console.WindowWidth / 2;
            int posicionY = Console.WindowHeight / 2;

            //Crear menu
            int repetir = 0;
            while (repetir == 0)
            {
                switch (Menu(opciones, "Inventario de Alumnos", posicionX, posicionY, "Selecciona una opcion:"))
                {
                    case 0: AgregarAlumno(); break;
                    case 1: BuscarAlumno(); break;
                    case 2: ModificarAlumno(); break;
                    case 3: EliminarAlumno(); break;
                    case 4: ListaAlumnos(); break;
                    case 5: Configuracion(); break;
                    default: repetir = 1; break;
                }
            }


        }
        static int Menu(string[] opciones, string titulo, int posicionX, int posicionY, string subTitulo) {

            select = 0;
            Console.CursorVisible = false;
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            //Hago que la posicion reste los caracteres de las opciones
            //para que se posicione correctamente y no inicie desde el centro
            posicionX = posicionX - opciones[1].Length / 2;
            posicionY = posicionY - opciones.Length / 2;

            //Personalizacion del menu
            MenuBackground(opciones, titulo, posicionX, posicionY, subTitulo);
            while (true)
            {
                if (select >= opciones.Length)
                {
                    select = opciones.Length - 1;
                }
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(posicionX, posicionY + i);
                    if (select == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = (ConsoleColor)colorSeleccion;
                    }
                    Console.Write(opciones[i]);
                    Console.ResetColor();
                }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Gray;
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (select > 0)
                            select--;
                        else
                            select = opciones.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:

                        if (select < opciones.Length - 1)
                            select++;
                        else
                            select = 0;
                        break;

                    case ConsoleKey.D1:
                        select = 0;
                        break;

                    case ConsoleKey.D2:
                        select = 1;
                        break;

                    case ConsoleKey.D3:
                        select = 2;
                        break;

                    case ConsoleKey.D4:
                        select = 3;
                        break;

                    case ConsoleKey.D5:
                        select = 4;
                        break;

                    case ConsoleKey.D6:
                        select = 5;
                        break;

                    case ConsoleKey.D7:
                        select = 6;
                        break;

                    case ConsoleKey.D8:
                        select = 7;
                        break;

                    case ConsoleKey.D9:
                        select = 8;
                        break;

                    case ConsoleKey.Enter:
                        Console.ResetColor();
                        Console.Clear();
                        return select;

                    case ConsoleKey.Escape:
                        Console.ResetColor();
                        Console.Clear();
                        return 32;
                }
            }
        }
        static void MenuBackground(string[] opciones, string titulo, int posicionXOriginal, int posicionYOriginal, string subTitulo) {

            //Fondo 
            Console.BackgroundColor = (ConsoleColor)color;
            Console.Clear();

            //Sombra del fondo del Menu
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            int posicionX = posicionXOriginal;
            int posicionY = posicionYOriginal - 3;
            int j = 0;
            for (int i = 0; i < opciones.Length + 7; i++)
            {
                while (j < opciones[0].Length + 3)
                {
                    Console.SetCursorPosition(posicionX, posicionY);
                    Console.Write(" ");
                    posicionX++;
                    j++;
                }
                posicionX = posicionXOriginal;
                j = 0;
                posicionY++;
            }

            //Fondo del Menu
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            posicionX = posicionXOriginal - 2;
            posicionY = posicionYOriginal - 4;
            j = 0;
            for (int i = 0; i < opciones.Length + 6; i++)
            {
                while (j < opciones[0].Length + 3)
                {
                    Console.SetCursorPosition(posicionX, posicionY);
                    Console.Write(" ");
                    posicionX++;
                    j++;
                }
                posicionX = posicionXOriginal - 2;
                j = 0;
                posicionY++;
            }

            //Bordes
            posicionX = posicionXOriginal - 2;
            posicionY = posicionYOriginal - 4;
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("┌");
            posicionX++;
            for (int i = 0; i < opciones[0].Length / 2 - titulo.Length / 2; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY);
                Console.Write("─");
                posicionX++;
            }
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("┤");
            posicionX++;
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write(titulo);
            posicionX += titulo.Length;
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("├");
            posicionX++;
            for (int i = 0; i < opciones[0].Length / 2 - titulo.Length / 2; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY);
                Console.Write("─");
                posicionX++;
            }
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("┐");
            posicionY++;
            for (int i = 0; i < opciones.Length + 5; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY);
                Console.Write("│");
                posicionY++;
            }
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("┘");
            posicionX--;
            for (int i = 0; i < opciones[0].Length + 2; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY);
                Console.Write("─");
                posicionX--;
            }
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write("└");
            posicionY--;
            for (int i = 0; i < opciones.Length + 5; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY);
                Console.Write("│");
                posicionY--;
            }

            posicionY += 2;
            posicionX++;
            Console.SetCursorPosition(posicionX, posicionY);
            Console.Write(subTitulo);
        }
        static void MenuNoOpciones(string[] preguntas, string titulo, int posicionX, int posicionY, string subTitulo) {
            Console.CursorVisible = false;

            //Hago que la posicion reste los caracteres de las opciones
            //para que se posicione correctamente y no inicie desde el centro
            posicionX = posicionX - preguntas[0].Length / 2;
            posicionY = posicionY - preguntas.Length / 2;

            //Personalizacion del menu
            MenuBackground(preguntas, titulo, posicionX, posicionY, subTitulo);

            //Muestra las preguntas
            for (int i = 0; i < preguntas.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(posicionX, posicionY + i);
                Console.Write(preguntas[i]);
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ResetColor();
        }
        static void Configuracion() {
            string[] preguntas = {
                "Cambiar Fondo              ",
                "Cambiar Color de Seleccion ",
                "Regresar                   "
            };
            switch (Menu(preguntas, "C O N F I G", Console.WindowWidth / 2, Console.WindowHeight / 2, "Selecciona un color:"))
            {
                case 0:
                    Fondo();
                    break;
                case 1:
                    ColorSeleccion();
                    break;
                case 2:
                    break;
            }

            
        }
        static void Fondo() {
            string[] preguntas = {
                "Rojo                   ",
                "Verde                  ",
                "Azul                   ",
                "Amarillo               ",
                "Blanco                 ",
                "Regresar"
            };
            switch (Menu(preguntas, "B A C K G R O U N D", Console.WindowWidth / 2, Console.WindowHeight / 2, "Selecciona un color:"))
            {
                case 0:
                    color = 12;
                    break;
                case 1:
                    color = 10;
                    break;
                case 2:
                    color = 9;
                    break;
                case 3:
                    color = 14;
                    break;
                case 4:
                    color = 15;
                    break;
                case 5:
                    Configuracion(); 
                    break;
            }

            //Guardar configuracion
            using (StreamWriter sw2 = new StreamWriter(archivoColorFondo))
                sw2.Write(color);
        }
        static void ColorSeleccion() {
            string[] preguntas = {
                "Rojo                   ",
                "Verde                  ",
                "Azul                   ",
                "Amarillo               ",
                "Blanco                 ",
                "Regresar"
            };
            switch (Menu(preguntas, "S E L E C C I O N", Console.WindowWidth / 2, Console.WindowHeight / 2, "Selecciona un color:"))
            {
                case 0:
                    colorSeleccion = 12;
                    break;
                case 1:
                    colorSeleccion = 10;
                    break;
                case 2:
                    colorSeleccion = 9;
                    break;
                case 3:
                    colorSeleccion = 14;
                    break;
                case 4:
                    colorSeleccion = 15;
                    break;
                case 5:
                    Configuracion();
                    break;
            }

            //Guardar configuracion
            using (StreamWriter sw3 = new StreamWriter(archivoColorSeleccion))
                sw3.Write(colorSeleccion);
        }
        static void AgregarAlumno()
        {
            //Limpiar consola y activo el cursor
            Console.Clear();
            Console.CursorVisible = true;

            //Verifico si existe la carpeta
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            //Verifico si ya existe el archivo txt
            if (!File.Exists(archivo))
            {
                File.Create(archivo).Close();
            }

            //Capturar datos del alumno
            string[] preguntas = {
                "Ingresa la matricula:                       ",
                "Ingresa el nombre:                          ",
                "Ingresa la carrera:                         ",
                "Ingresa el sexo:                            ",
                "Ingresa la edad:                            "
            };

            Boolean datosVacios = true;
            string alumnoDatos = "";
            string[] alumno = new string[5];
            while (datosVacios)
            {
                Boolean errores = false;

                //Mostrar preguntas en el menu
                MenuNoOpciones(preguntas, "Agregar Alumno", Console.WindowWidth / 2, Console.WindowHeight / 2, "Rellene los datos correspondientes.");

                //Guardar los datos en un arreglo
                Console.CursorVisible = true;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;

                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2);
                alumno[0] = Console.ReadLine();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2 - 1);
                alumno[1] = Console.ReadLine();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2);
                alumno[2] = Console.ReadLine();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 1);
                alumno[3] = Console.ReadLine();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
                alumno[4] = Console.ReadLine();

                //Comprobar si no se ingreso ningun caracter vacio
                if ((alumno[0] != "") && (alumno[1] != "") && (alumno[2] != "") && (alumno[3] != "") && (alumno[4] != ""))
                    datosVacios = false;
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 22, Console.WindowHeight / 2 + 3);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("Error:");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(" No puedes dejar campos vacios");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 22, Console.WindowHeight / 2 + 4);
                    Console.Write("Presiona una tecla para continuar...");
                    Console.CursorVisible = true;
                    Console.ReadKey();
                    Console.Clear();
                    errores = true;
                }
                //Comprobar si no se ingreso una matricula ya existente
                if (!errores)
                {
                    alumnoDatos = alumno[0] + "|" + alumno[1] + "|" + alumno[2] + "|" + alumno[3] + "|" + alumno[4];
                    string[] archivito = File.ReadAllLines(archivo);
                    for (int i = 0; i < archivito.Length; i++)
                    {
                        string[] partes = archivito[i].Split('|');
                        if (alumno[0] == partes[0])
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 22, Console.WindowHeight / 2 + 3);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("Error:");
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(" La matricula ya existe");
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 22, Console.WindowHeight / 2 + 4);
                            Console.Write("Presiona una tecla para continuar...");
                            Console.CursorVisible = true;
                            Console.ReadKey();
                            Console.Clear();
                            datosVacios = true;
                            break;
                        }
                    }
                }
            }
            

            //Rescribir el archivo
            string info = File.ReadAllText(archivo);
            StreamWriter sw = new StreamWriter(archivo);
            sw.Write(info);

            //Guardar datos en el archivo
            sw.WriteLine(alumnoDatos);
            sw.Close();
        }
        static void BuscarAlumno()
        {
            Console.Clear();
            string[] preguntas = {
                "Ingresa la matricula del alumno a buscar:  "
            };
            
            MenuNoOpciones(preguntas, "B U S C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 1);
            Console.CursorVisible = true;
            string matricula = Console.ReadLine();

            if (!File.Exists(archivo))
            {
                string[] archoNoExiste = { "Para buscar un alumno tiene que existir previamente", "Actualmente no existe ningun alumno" };
                MenuNoOpciones(archoNoExiste, "E R R O R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Presiona una tecla para continuar...");
                Console.CursorVisible = true;
                Console.ReadKey();
                return;
            }

            StreamReader sr = new StreamReader(archivo);
            int lineasTotal = File.ReadAllLines(archivo).Length;
            int contador = 0;
            int encontrado = 0;
            while (true)
            {
                string linea = sr.ReadLine();
                string[] datos = linea.Split('|');
                for (int i = 0; i < datos.Length; i++)
                {
                    if (datos[i] == matricula)
                    {
                        string[] datosEncontrados =
                        { 
                          "-----------------------------------",
                          "Matricula: " + datos[0],
                          "Nombre: " + datos[1],
                          "Carrera: " + datos[2],
                          "Sexo: " + datos[3],
                          "Edad: " + datos[4],
                          "-----------------------------------",
                          "Presiona una tecla para continuar..."
                        };
                        MenuNoOpciones(datosEncontrados, "B U S C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Datos del alumno encontrado:");
                        Console.CursorVisible = true;
                        Console.ReadKey();
                        encontrado = 1;
                        break;
                    }
                } 

                if (encontrado == 1)
                {
                    sr.Close();
                    break;
                }   

                contador++;
                if (lineasTotal == contador)
                {
                    sr.Close();
                    string[] matNoEncontrada = {
                    "Presiona una tecla para continuar... "
                    };

                    MenuNoOpciones(matNoEncontrada, "B U S C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "No se encontro la matricula");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 19 + matNoEncontrada[0].Length, Console.WindowHeight / 2);
                    Console.CursorVisible = true;
                    Console.ReadKey();
                    break;
                }
            }
        }
        static void ModificarAlumno()
        {
            Console.Clear();
            string[] preguntas = {
                "Ingresa la matricula del alumno a modificar: "
            };

            MenuNoOpciones(preguntas, "M O D I F I C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 1);
            Console.CursorVisible = true;
            string matricula = Console.ReadLine();

            if (!File.Exists(archivo))
            {
                string[] archoNoExiste = { "Para modificar un alumno tiene que existir previamente ", "Actualmente no existe ningun alumno" };
                MenuNoOpciones(archoNoExiste, "E R R O R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Presiona una tecla para continuar...");
                Console.CursorVisible = true;
                Console.ReadKey();
                return;
            }

            string[] lineas = File.ReadAllLines(archivo);
            int encontrado = 0;

            for (int i = 0; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split('|');
                if (datos[0] == matricula)
                {
                    string[] preguntas2 = {
                        "---------------------------------------------------------",
                        "Matricula (no editable): " + datos[0],
                        "Ingresa el nuevo nombre: ",
                        "Ingresa la nueva carrera: ",
                        "Ingresa el nuevo sexo: ",
                        "Ingresa la nueva edad: ",
                        "---------------------------------------------------------"
                    };
                    MenuNoOpciones(preguntas2, "M O D I F I C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Rella los nuevos datos:");

                    Console.CursorVisible = true;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2 - 1);
                    string newNombre = Console.ReadLine();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2);
                    string newCarrera = Console.ReadLine();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 1);
                    string newSexo = Console.ReadLine();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
                    string newEdad = Console.ReadLine();

                    lineas[i] = $"{datos[0]}|{newNombre}|{newCarrera}|{newSexo}|{newEdad}";
                    encontrado = 1;
                    break;
                }
            }

            if (encontrado == 1)
            {
                File.WriteAllLines(archivo, lineas);
                Console.SetCursorPosition(Console.WindowWidth / 2 - 28, Console.WindowHeight / 2 + 4);
                Console.WriteLine("Alumno modificado exitosamente.");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 28, Console.WindowHeight / 2 + 5);
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                string[] matNoEncontrada = {
                "Presiona una tecla para continuar... "
                };

                MenuNoOpciones(matNoEncontrada, "M O D I F I C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "No se encontro la matricula");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 19 + matNoEncontrada[0].Length, Console.WindowHeight / 2 );
                Console.CursorVisible = true;
                Console.ReadKey();
            }

            
        }
        static void EliminarAlumno()
        {
            Console.Clear();
            string[] preguntas = {
                "Ingresa la matricula del alumno a elminar: "
            };

            MenuNoOpciones(preguntas, "E L I M I N A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 1);
            Console.CursorVisible = true;
            string matricula = Console.ReadLine();

            if (!File.Exists(archivo))
            {
                string[] archoNoExiste = { "Para eliminar un alumno tiene que existir previamente", "Actualmente no existe ningun alumno" };
                MenuNoOpciones(archoNoExiste, "E R R O R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Presiona una tecla para continuar...");
                Console.CursorVisible = true;
                Console.ReadKey();
                return;
            }

            string[] lineas = File.ReadAllLines(archivo);

            int encontrado = 0;
            for(int i = 0; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split('|');
                if(datos[0] == matricula)
                {
                    lineas = lineas.Where(val => val != lineas[i]).ToArray();
                    File.WriteAllLines(archivo, lineas);
                    encontrado = 1;

                    string[] elimiAlum = {
                        "Presiona una tecla para continuar... "
                    };
                    MenuNoOpciones(elimiAlum, "E L I M I N A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Alumno eliminado exitosamente.");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 19 + elimiAlum[0].Length, Console.WindowHeight / 2);
                    Console.CursorVisible = true;
                    Console.ReadKey();
                    break;
                }
            }

            if (encontrado == 0)
            {
                string[] matNoEncontrada = {
                "Presiona una tecla para continuar... "
                };

                MenuNoOpciones(matNoEncontrada, "M O D I F I C A R", Console.WindowWidth / 2, Console.WindowHeight / 2, "No se encontro la matricula");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 19 + matNoEncontrada[0].Length, Console.WindowHeight / 2);
                Console.CursorVisible = true;
                Console.ReadKey();
            }
        }
        static void ListaAlumnos() {
            Console.Clear();
            if (!File.Exists(archivo))
            {
                string[] archoNoExiste = { "Para ver la lista de alumnos tiene que existir previamente ", "Actualmente no existe ningun alumno" };
                MenuNoOpciones(archoNoExiste, "E R R O R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Presiona una tecla para continuar...");
                Console.CursorVisible = true;
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < File.ReadAllLines(archivo).Length; i++)
            {
                if (File.ReadAllLines(archivo).Length == 0)
                {
                    string[] archoNoExiste = { "Para ver la lista de alumnos tiene que existir previamente", "Actualmente no existe ningun alumno" };
                    MenuNoOpciones(archoNoExiste, "E R R O R", Console.WindowWidth / 2, Console.WindowHeight / 2, "Presiona una tecla para continuar...");
                    Console.CursorVisible = true;
                    Console.ReadKey();
                    return;
                }
            }

            //Crear los espacios para la lista de alumnos
            int tempNum = 0;
            int lineas = File.ReadAllLines(archivo).Length;
            string[] datosEncontrados = File.ReadAllLines(archivo);
            if (lineas != 1)
            {
                for (int i = 0; i < lineas; i++)
                {
                    if (i == 0)
                    {
                        if (datosEncontrados[i].Length > datosEncontrados[i + 1].Length)
                        {
                            tempNum = datosEncontrados[i].Length;
                        }
                        else
                        {
                            tempNum = datosEncontrados[i + 1].Length;
                        }
                    }

                    if (datosEncontrados[i].Length > tempNum)
                    {
                        tempNum = datosEncontrados[i].Length;
                    }
                }
            }
            else
            {
                tempNum = datosEncontrados[0].Length + 9;
            }

            while (tempNum % 2 != 0)
            {
                tempNum++;
            }

            //Crear el arreglo de todas las matriculas
            string[] matriculas = new string[datosEncontrados.Length];
            for (int i = 0; i < datosEncontrados.Length; i++)
            {
                string[] partes = datosEncontrados[i].Split('|');
                matriculas[i] = partes[0];
            }
            //Buscar cual es la matricula de mayor tamaño
            int mayorMatricula = FraseMayor(matriculas);

            //Crear el arreglo de todos los nombres
            string[] nombres = new string[datosEncontrados.Length];
            for (int i = 0; i < datosEncontrados.Length; i++)
            {
                string[] partes = datosEncontrados[i].Split('|');
                nombres[i] = partes[1];
            }
            //Buscar cual es la matricula de mayor tamaño
            int mayorNombre = FraseMayor(nombres);

            //Crear el arreglo de todos las carreras
            string[] carreras = new string[datosEncontrados.Length];
            for (int i = 0; i < datosEncontrados.Length; i++)
            {
                string[] partes = datosEncontrados[i].Split('|');
                carreras[i] = partes[2];
            }
            //Buscar cual es la matricula de mayor tamaño
            int mayorCarrera = FraseMayor(carreras);

            //Crear arreglo de todos los sexos
            string[] sexos = new string[datosEncontrados.Length];
            for (int i = 0; i < datosEncontrados.Length; i++)
            {
                string[] partes = datosEncontrados[i].Split('|');
                sexos[i] = partes[3];
            }
            //Buscar cual es la matricula de mayor tamaño
            int mayorSexo = FraseMayor(sexos);

            //Crear arreglo de todos los edad
            string[] edades = new string[datosEncontrados.Length];
            for (int i = 0; i < datosEncontrados.Length; i++)
            {
                string[] partes = datosEncontrados[i].Split('|');
                edades[i] = partes[4];
            }
            //Buscar cual es la matricula de mayor tamaño
            int mayorEdad = FraseMayor(edades);

            ///////////////////////////////
            string matriculaText = "Matricula|";
            if (mayorMatricula > 9)
            {
                string vacio = new string(' ', mayorMatricula - 9);
                matriculaText = $"Matricula{vacio}|";
            }

            string nombresText = "Nombre|";
            if (mayorNombre > 6)
            {
                string vacio = new string(' ', mayorNombre - 6);
                nombresText = $"Nombre{vacio}|";
            }

            string carrerasText = "Carrera|";
            if (mayorCarrera > 7)
            {
                string vacio = new string(' ', mayorCarrera - 7);
                carrerasText = $"Carrera{vacio}|";
            }

            string sexosText = "Sexo|";
            if (mayorSexo > 4)
            {
                string vacio = new string(' ', mayorSexo - 4);
                sexosText = $"Sexo{vacio}|";
            }

            string edadesText = "Edad";
            if (mayorEdad > 3)
            {
                string vacio = new string(' ', mayorEdad - 3);
                edadesText = $"Edad{vacio}";
            }

            string[] datos = new string[datosEncontrados.Length + 2];
            datos[0] = $"          {matriculaText}{nombresText}{carrerasText}{sexosText}{edadesText}";
            if (datos[0].Length % 2 == 0)
                datos[0] = $"          {matriculaText}{nombresText}{carrerasText}{sexosText}{edadesText} ";
            string tempString = new string('-', datos[0].Length);
            datos[1] = tempString;
            //int temp = 1;
            for (int i = 2; i < datosEncontrados.Length + 2; i++)
            {
                datos[i] = $"Alumno {i - 1}|{datosEncontrados[i - 2]}";
                //temp++;
            }

            //En datos añadir los espacios en blanco a antes de los pipes
            if (mayorMatricula < "Matricula".Length)
            {
                mayorMatricula = "Matricula".Length;
            }
            if (mayorNombre < "Nombre".Length)
            {
                mayorNombre = "Nombre".Length;
            }
            if (mayorCarrera < "Carrera".Length)
            {
                mayorCarrera = "Carrera".Length;
            }
            if (mayorSexo < "Sexo".Length)
            {
                mayorSexo = "Sexo".Length;
            }
            if (mayorEdad < "Edad".Length)
            {
                mayorEdad = "Edad".Length;
            }
            for (int i = 2; i < datos.Length; i++)
            {
                string[] partes = datosEncontrados[i - 2].Split('|');
                string[] partes2 = datos[i].Split('|');
                string vacioMatricula = new string(' ', Math.Max(0, mayorMatricula - partes[0].Length));
                string vacioNombre = new string(' ', Math.Max(0, mayorNombre - partes[1].Length));
                string vacioCarrera = new string(' ', Math.Max(0, mayorCarrera - partes[2].Length));
                string vacioSexo = new string(' ', Math.Max(0, mayorSexo - partes[3].Length));
                string vacioEdad = new string(' ', Math.Max(0, mayorEdad - partes[4].Length));
                datos[i] = $"{partes2[0]}: {partes[0]}{vacioMatricula}|{partes[1]}{vacioNombre}|{partes[2]}{vacioCarrera}|{partes[3]}{vacioSexo}|{partes[4]}{vacioEdad}";
            }

            MenuNoOpciones(datos, "L I S T A", Console.WindowWidth / 2, Console.WindowHeight / 2, "Lista de alumnos:");
            Console.ReadKey();
            return;
        }

        static int FraseMayor(string[] frases) 
        {
            int mayor = frases[0].Length;

            for (int i = 0; i < frases.Length - 1; i++)
            {
                if (mayor < frases[i + 1].Length)
                {
                    mayor = frases[i + 1].Length;
                }
            }

            if (frases.Length == 1)
            {
                mayor = frases[0].Length;
            }

            return mayor;
        }
    }
}
