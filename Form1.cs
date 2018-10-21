using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Arbol_TreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //funcion recursiva
        private TreeNode crearArbol(DirectoryInfo directoryInfo) {

            TreeNode treeNode = new TreeNode(directoryInfo.Name);

            //este bucle para recorrer las carpetas
            foreach(var item in directoryInfo.GetDirectories())
            {
                treeNode.Nodes.Add(crearArbol(item));
            }
            //recorrer los archivos
            foreach(var item in directoryInfo.GetFiles())
            {
                treeNode.Nodes.Add(new TreeNode(item.Name));

            }

            return treeNode;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string rutaBase = @"D:\WIT solutions practica";

            tvFile.Nodes.Clear();//el clear para limpiar nodos

            DirectoryInfo directoryInfo = new DirectoryInfo(@rutaBase);

            tvFile.Nodes.Add(crearArbol(directoryInfo));
        }

        //evento con double click
        private void tvFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string rutaAbsoluta = @"D:\WIT solutions practica\" + @tvFile.SelectedNode.FullPath;//retorna la ruta absoluta a partir de nuestra raiz, ruta relativa pero absoluta en el arbol

            System.Diagnostics.Process.Start(@rutaAbsoluta);//saltan excepciones


        }
    }
}
