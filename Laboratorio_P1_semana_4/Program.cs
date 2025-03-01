using System;
using System.IO;

namespace LaboratorioAvengers
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivo = "inventos.txt"; // Ruta del archivo de inventos
            string rutaBackup = Path.Combine("Backup", "inventos.txt"); // Ruta de la copia de seguridad
            string rutaClasificados = Path.Combine("ArchivosClasificados", "inventos.txt"); // Ruta de archivos clasificados
            string rutaLaboratorio = "LaboratorioAvengers"; // Ruta de la carpeta del laboratorio

            while (true)
            {
                Console.WriteLine("=== Menú de Operaciones ===");
                Console.WriteLine("1. Crear archivo 'inventos.txt'");
                Console.WriteLine("2. Agregar un invento");
                Console.WriteLine("3. Leer archivo línea por línea");
                Console.WriteLine("4. Leer todo el contenido del archivo");
                Console.WriteLine("5. Copiar archivo a 'Backup'");
                Console.WriteLine("6. Mover archivo a 'ArchivosClasificados'");
                Console.WriteLine("7. Crear carpeta 'ProyectosSecretos'");
                Console.WriteLine("8. Listar archivos en 'LaboratorioAvengers'");
                Console.WriteLine("9. Eliminar archivo 'inventos.txt'");
                Console.WriteLine("10. Salir");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearArchivo(rutaArchivo);
                        break;
                    case "2":
                        Console.Write("Ingresa el nombre del invento: ");
                        string invento = Console.ReadLine();
                        AgregarInvento(rutaArchivo, invento);
                        break;
                    case "3":
                        LeerLineaPorLinea(rutaArchivo);
                        break;
                    case "4":
                        LeerTodoElTexto(rutaArchivo);
                        break;
                    case "5":
                        CopiarArchivo(rutaArchivo, rutaBackup);
                        break;
                    case "6":
                        MoverArchivo(rutaArchivo, rutaClasificados);
                        break;
                    case "7":
                        CrearCarpeta("ProyectosSecretos");
                        break;
                    case "8":
                        ListarArchivos(rutaLaboratorio);
                        break;
                    case "9":
                        EliminarArchivo(rutaArchivo);
                        break;
                    case "10":
                        Console.WriteLine("Saliendo del programa...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intenta de nuevo.");
                        break;
                }

                Console.WriteLine(); // Espacio para mejor legibilidad
            }
        }

        // Función para crear un archivo
        static void CrearArchivo(string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    File.Create(ruta).Close(); // Crear el archivo y cerrarlo
                    Console.WriteLine("Archivo 'inventos.txt' creado exitosamente.");
                }
                else
                {
                    Console.WriteLine("El archivo 'inventos.txt' ya existe.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para crear el archivo en esta ubicación.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Algo salió mal al intentar crear el archivo. Verifica la ruta o los permisos.");
            }
        }

        // Función para agregar un invento al archivo
        static void AgregarInvento(string ruta, string invento)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(ruta))
                {
                    sw.WriteLine(invento);
                }
                Console.WriteLine($"Invento '{invento}' agregado exitosamente.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para modificar el archivo.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo agregar el invento. Verifica la ruta o los permisos.");
            }
        }

        // Función para leer el archivo línea por línea
        static void LeerLineaPorLinea(string ruta)
        {
            try
            {
                if (File.Exists(ruta))
                {
                    using (StreamReader sr = new StreamReader(ruta))
                    {
                        string linea;
                        while ((linea = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(linea);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para leer el archivo.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo leer el archivo. Verifica la ruta o los permisos.");
            }
        }

        // Función para leer todo el contenido del archivo
        static void LeerTodoElTexto(string ruta)
        {
            try
            {
                if (File.Exists(ruta))
                {
                    string contenido = File.ReadAllText(ruta);
                    Console.WriteLine(contenido);
                }
                else
                {
                    Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para leer el archivo.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo leer el archivo. Verifica la ruta o los permisos.");
            }
        }

        // Función para copiar un archivo
        static void CopiarArchivo(string origen, string destino)
        {
            try
            {
                if (File.Exists(origen))
                {
                    string directorioBackup = Path.GetDirectoryName(destino);
                    if (!Directory.Exists(directorioBackup))
                    {
                        Directory.CreateDirectory(directorioBackup);
                    }
                    File.Copy(origen, destino, true); // Sobrescribir si existe
                    Console.WriteLine("Archivo copiado a 'Backup' exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para copiar el archivo.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo copiar el archivo. Verifica la ruta o los permisos.");
            }
        }

        // Función para mover un archivo
        static void MoverArchivo(string origen, string destino)
        {
            try
            {
                if (File.Exists(origen))
                {
                    string directorioClasificados = Path.GetDirectoryName(destino);
                    if (!Directory.Exists(directorioClasificados))
                    {
                        Directory.CreateDirectory(directorioClasificados);
                    }
                    File.Move(origen, destino);
                    Console.WriteLine("Archivo movido a 'ArchivosClasificados' exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para mover el archivo.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo mover el archivo. Verifica la ruta o los permisos.");
            }
        }

        // Función para crear una carpeta
        static void CrearCarpeta(string nombreCarpeta)
        {
            try
            {
                if (!Directory.Exists(nombreCarpeta))
                {
                    Directory.CreateDirectory(nombreCarpeta);
                    Console.WriteLine($"Carpeta '{nombreCarpeta}' creada exitosamente.");
                }
                else
                {
                    Console.WriteLine($"La carpeta '{nombreCarpeta}' ya existe.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para crear la carpeta.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo crear la carpeta. Verifica la ruta o los permisos.");
            }
        }

        // Función para listar archivos en una carpeta
        static void ListarArchivos(string ruta)
        {
            try
            {
                if (Directory.Exists(ruta))
                {
                    string[] archivos = Directory.GetFiles(ruta);
                    Console.WriteLine($"Archivos en '{ruta}':");
                    foreach (string archivo in archivos)
                    {
                        Console.WriteLine(Path.GetFileName(archivo));
                    }
                }
                else
                {
                    Console.WriteLine($"Error: La carpeta '{ruta}' no existe.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para listar los archivos.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo listar los archivos. Verifica la ruta o los permisos.");
            }
        }

        // Función para eliminar un archivo
        static void EliminarArchivo(string ruta)
        {
            try
            {
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                    Console.WriteLine("Archivo 'inventos.txt' eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error: El archivo 'inventos.txt' no existe. ¡Thanos debió borrarlos de la existencia!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para eliminar el archivo.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: No se pudo eliminar el archivo. Verifica la ruta o los permisos.");
            }
        }
    }
}