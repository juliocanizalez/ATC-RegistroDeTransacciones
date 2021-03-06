﻿using RegistroDeTransacciones.Reportes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaDePagoEmpleados
{
    public partial class Form3 : Form
    {
        CatalogoDeCuentas oCatalogo;
        List<CatalogoDeCuentas> lCatalogo = new List<CatalogoDeCuentas>();

        public Form3()
        {
            InitializeComponent();
            CargarTabla();
            txtCodigo.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                oCatalogo = new CatalogoDeCuentas();
                oCatalogo.EliminarCuenta(DataGridViewUtils.GetValorCelda(dataGridView1, 0), DataGridViewUtils.GetValorCelda(dataGridView1, 1));
                MessageBox.Show("Cuenta Eliminada Con Exito");
                CargarTabla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                oCatalogo = new CatalogoDeCuentas();
                oCatalogo.InsertarCuenta(txtCodigo.Text, txtCuenta.Text, txtNaturaleza.Text);
                CargarTabla();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void CargarTabla()
        {
            oCatalogo = new CatalogoDeCuentas();
            lCatalogo = oCatalogo.CargarCatalogoDeCuentas();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lCatalogo;
            dataGridView1.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogodeCuentaDoc doc = new CatalogodeCuentaDoc();
                doc.PrintDocument(lCatalogo);
                MessageBox.Show("Archivo guardado con éxito en: " + doc.path, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(doc.path);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Ocurrió un problema al intentar guardar el documento:\n" +  ee , "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public class DataGridViewUtils
        {
            public static string GetValorCelda(DataGridView dgv, int num)
            {
                try
                {
                    string valor = "";
                    valor = dgv.Rows[dgv.CurrentRow.Index].Cells[num].Value.ToString();
                    return valor;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
