using EDDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;


//using GraphVizWrapper;
//using GraphVizWrapper.Queries;
//using GraphVizWrapper.Commands;

//using csdot;
//using csdot.Attributes.DataTypes;

namespace EDDemo.Estructuras_No_Lineales
{
    namespace EDDemo.Estructuras_No_Lineales
    {
        public partial class frmArboles : Form
        {
            ArbolBusqueda miArbol;
            NodoBinario miRaiz;

            public frmArboles()
            {
                InitializeComponent();
                miArbol = new ArbolBusqueda();
                miRaiz = null;
            }

            private void btnInsertar_Click(object sender, EventArgs e)
            {
                miRaiz = miArbol.RegresaRaiz();
                miArbol.strArbol = "";

                miArbol.InsertaNodo(int.Parse(txtDato.Text), ref miRaiz);
                miArbol.MuestraArbolAcostado(1, miRaiz);
                txtArbol.Text = miArbol.strArbol;

                txtDato.Text = "";
            }

            private void btnLimpiar_Click(object sender, EventArgs e)
            {
                miArbol = null;
                miRaiz = null;
                miArbol = new ArbolBusqueda();
                txtArbol.Text = "";
                txtDato.Text = "";
                lblRecorridoPreOrden.Text = "";
                lblRecorridoInOrden.Text = "";
                lblRecorridoPostOrden.Text = "";
                lblRecorridoPorNiveles.Text = ""; // Limpiamos el label de recorrido por niveles
            }

            private void btnGrafica_Click(object sender, EventArgs e)
            {
                String graphVizString;

                miRaiz = miArbol.RegresaRaiz();
                if (miRaiz == null)
                {
                    MessageBox.Show("El árbol está vacío");
                    return;
                }

                StringBuilder b = new StringBuilder();
                b.Append("digraph G { node [shape=\"circle\"]; " + Environment.NewLine);
                b.Append(miArbol.ToDot(miRaiz));
                b.Append("}");
                graphVizString = b.ToString();

                Bitmap bm = FileDotEngine.Run(graphVizString);

                frmGrafica graf = new frmGrafica();
                graf.ActualizaGrafica(bm);
                graf.MdiParent = this.MdiParent;
                graf.Show();
            }

            private void btnRecorrer_Click(object sender, EventArgs e)
            {
                // Recorrido en PreOrden
                miRaiz = miArbol.RegresaRaiz();
                miArbol.strRecorrido = "";

                if (miRaiz == null)
                {
                    lblRecorridoPreOrden.Text = "El árbol está vacío";
                    return;
                }
                lblRecorridoPreOrden.Text = "";
                miArbol.PreOrden(miRaiz);
                lblRecorridoPreOrden.Text = miArbol.strRecorrido;

                // Recorrido en InOrden
                miRaiz = miArbol.RegresaRaiz();
                miArbol.strRecorrido = "";

                if (miRaiz == null)
                {
                    lblRecorridoInOrden.Text = "El árbol está vacío";
                    return;
                }
                lblRecorridoInOrden.Text = "";
                miArbol.InOrden(miRaiz);
                lblRecorridoInOrden.Text = miArbol.strRecorrido;

                // Recorrido en PostOrden
                miRaiz = miArbol.RegresaRaiz();
                miArbol.strRecorrido = "";

                if (miRaiz == null)
                {
                    lblRecorridoPostOrden.Text = "El árbol está vacío";
                    return;
                }
                lblRecorridoPostOrden.Text = "";
                miArbol.PostOrden(miRaiz);
                lblRecorridoPostOrden.Text = miArbol.strRecorrido;

                // Recorrido por Niveles
                miRaiz = miArbol.RegresaRaiz();
                miArbol.strRecorrido = "";

                if (miRaiz == null)
                {
                    lblRecorridoPorNiveles.Text = "El árbol está vacío";
                    return;
                }
                lblRecorridoPorNiveles.Text = "";
                miArbol.RecorrerPorNiveles(miRaiz);
                lblRecorridoPorNiveles.Text = miArbol.strRecorrido;
            }

            private void btnCrearArbol_Click(object sender, EventArgs e)
            {
                // Limpiamos los objetos y clases del anterior árbol
                miArbol = null;
                miRaiz = null;
                miArbol = new ArbolBusqueda();
                txtArbol.Text = "";
                txtDato.Text = "";

                miArbol.strArbol = "";

                Random rnd = new Random();

                for (int nNodos = 1; nNodos <= txtNodos.Value; nNodos++)
                {
                    int Dato = rnd.Next(1, 100);
                    miRaiz = miArbol.RegresaRaiz();
                    miArbol.InsertaNodo(Dato, ref miRaiz);
                }

                miArbol.MuestraArbolAcostado(1, miRaiz);
                txtArbol.Text = miArbol.strArbol;

                txtDato.Text = "";
            }

            private void btnBuscar_Click(object sender, EventArgs e)
            {
                // Verificamos si el árbol tiene una raíz
                miRaiz = miArbol.RegresaRaiz();

                if (miRaiz == null)
                {
                    MessageBox.Show("El árbol está vacío.");
                    return;
                }

                // Obtenemos el valor a buscar
                int valor = int.Parse(txtBuscar.Text);

                // Llamamos al método BuscarNodo
                bool encontrado = miArbol.BuscarNodo(valor, miRaiz);

                if (encontrado)
                {
                    MessageBox.Show($"El nodo con valor {valor} fue encontrado.");
                }
                else
                {
                    MessageBox.Show($"El nodo con valor {valor} no existe en el árbol.");
                }

                // Limpiamos la caja de texto
                txtBuscar.Text = "";
            }

            private void btnEliminarPredecesor_Click(object sender, EventArgs e)
            {
                int valor = int.Parse(txtBuscar.Text);

                miRaiz = miArbol.EliminarNodoPredecesor(valor, ref miRaiz);

                // Actualizamos el árbol en la interfaz
                miArbol.MuestraArbolAcostado(1, miRaiz);
                txtArbol.Text = miArbol.strArbol;
            }

            private void btnEliminarSucesor_Click(object sender, EventArgs e)
            {
                int valor = int.Parse(txtBuscar.Text);

                miRaiz = miArbol.EliminarNodoSucesor(valor, ref miRaiz);

                // Actualizamos el árbol en la interfaz
                miArbol.MuestraArbolAcostado(1, miRaiz);
                txtArbol.Text = miArbol.strArbol;
            }
        }
    }
}