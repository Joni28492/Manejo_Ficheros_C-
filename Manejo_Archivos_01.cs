using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//importante para streamreader

namespace Manejo_Archivos_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            string nombreArchivo = "\\temporal.txt";
            string rutaArchivo = @txtRutaArchivo.Text + @nombreArchivo;
            //comprobar existencia de archivo
            if (File.Exists(rutaArchivo)){

                nombreArchivo = "\\temporal" + Guid.NewGuid().ToString()+".txt";//Guid.NewGuid().ToString() genera una cadena aleatoria que no se va a repetir
                rutaArchivo = @txtRutaArchivo.Text + @nombreArchivo;
            }

            using (StreamWriter sw = new StreamWriter(@rutaArchivo, true)) //se pasa la ruta de la carpeta en el constructor
            {                                                                         //@ aggrega soporte para caracteres especiales por ejemplo \\ ->\ 
                                                                                      //si es true para continuar escribiendo en el archivo
                                                                                      //false para sobreescribirlo, por defecto va false

            //using funciona como una instancia de la clase, funciona como recolector de basura
            //en el momento que se termina llama a todos los metodos close que tengan que ver con ella 
            //si hay un archivo con el mismo nombre lo sustituye y modifica el contenido 

            sw.WriteLine("Esta es la primera linea en el archivo");
            sw.WriteLine("Esta es la segunda linea en el archivo");
            sw.WriteLine("Esta es la tercera linea en el archivo");
            }

            //IMPORTANTE cerrar siempre
            // sw.Close();//para cerrar el archivo, incluso si no se ve la ventana y esta en memoria
        }

        private void btnLerrArchivo_Click(object sender, EventArgs e)
        {
            txtDatosArchivo.Text = "";//inicializamos como vacio

            using(StreamReader sr =new StreamReader(@txtRutaArchivo.Text)){

                string linea;//variable para leer linea a linea

                while ((linea = sr.ReadLine()) != null)
                {
                    txtDatosArchivo.AppendText(linea+"\n");//para colocarlo al final 
                                                        //hacemos salto de linea para que no vaya todo en una sola

                }



            }



        }

        private void btnCopiarArchivo_Click(object sender, EventArgs e)
        {
            string rutaInicial = @"D:\WIT solutions practica\archivo.txt";// de donde lo voy a copiar 
            string rutaFinal= @"D:\WIT solutions practica\carpeta Temporal\archivoCopiado.txt";//y a donde lo pego

            if(!Directory.Exists(@"D:\WIT solutions practica\carpeta Temporal"))
            {
                Directory.CreateDirectory(@"D:\WIT solutions practica\carpeta Temporal");//creamos el directorio si NO existe
            }

            if (File.Exists(@rutaInicial))
            {
                File.Copy(rutaInicial, rutaFinal, true);//el true es por si existe el archivo lo sobreescriba

            }


        }

        private void btnMoverArchivo_Click(object sender, EventArgs e)
        {

            string posicionInicial = @"D:\WIT solutions practica\archivo1.txt";
            string posicionFinal = @"D:\WIT solutions practica\archivos copiados\archivo1.txt";

            File.Move(posicionInicial, posicionFinal);
            //Directory.Move() para los directorios
        }

        private void btnEliminarArchivo_Click(object sender, EventArgs e)
        {

            string rutaArchivo= @"D:\WIT solutions practica\archivo2.txt";

            if (File.Exists(@rutaArchivo))//comprobar si existe para que no salten excepciones
            {
                File.Delete(@rutaArchivo);

            }

        }

        private void btnObtenerRutaArchivo_Click(object sender, EventArgs e)
        {
            string rutaArchivo = string.Empty;//cadena vacia

            OpenFileDialog openFileDialog = new OpenFileDialog();

            //comprobamos cuando el usuario marca OK
           if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog.FileName;//obtiene la ruta absoluta del archivo
            }

            txtRutaArchivo.Text = rutaArchivo;

        }

        private void btnSeleccionarDirectorio_Click(object sender, EventArgs e)
        {
            string rutaDirectorio = string.Empty;

            FolderBrowserDialog fbd = new FolderBrowserDialog();


            if (fbd.ShowDialog() == DialogResult.OK)
            {
                rutaDirectorio = fbd.SelectedPath;//seleccion de la ruta
            }

            txtRutaDirectorio.Text = rutaDirectorio;

            if(rutaDirectorio.Trim()!=string.Empty)//trim() para borrar los espacios, comprobamos que el directorio no este vacio
            {


                DirectoryInfo di = new DirectoryInfo(@rutaDirectorio);//hay que pasarle ruta

                //se crea la variable item para recorrer el bucle que se recorre mientras haya archivos
                foreach (var item in di.GetFiles())
                {
                    //agregamos cada archivo
                    lbArchivos.Items.Add(item.Name);

                }

            }

        }

    }
}
