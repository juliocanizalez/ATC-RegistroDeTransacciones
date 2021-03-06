﻿using RegistroDeTransacciones;
using RegistroDeTransacciones.Formularios;
using RegistroDeTransacciones.Reportes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaDePagoEmpleados
{
    public partial class Form1 : Form
    {
        LibroDiario Transaccion;
        List<LibroDiario> libroDiario = new List<LibroDiario>();
        public static Form1 f1;

        public Form1()
        {
            InitializeComponent();
            Form1.f1 = this;
            CargarTabla();

        }

        public void CrearAsiento()
        {
            try
            {
                Transaccion = new LibroDiario();
                Transaccion.Codigo = txtCod.Text;
                Transaccion.Cuenta = txtCuenta.Text;
                Transaccion.Concepto = txtConcepto.Text;
                Transaccion.Fecha = txtFecha.Text;
                Transaccion.Asiento = Convert.ToString(txtAsiento.Value);
                Transaccion.Orden = Convert.ToString(txtOrden.Value);
                if (txtNaturaleza.SelectedIndex == 0)
                {
                    Transaccion.Parcial = Convert.ToDouble(txtValor.Text);
                }
                else if (txtNaturaleza.SelectedIndex == 1)
                {
                    Transaccion.Debe = Convert.ToDouble(txtValor.Text);
                }
                else if (txtNaturaleza.SelectedIndex == 2)
                {
                    Transaccion.Haber = Convert.ToDouble(txtValor.Text);
                }
                //Agregar Transaccion a la Lista
                libroDiario.Add(Transaccion);

                CargarTabla();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Limpiar()
        {
            txtCod.Text = "";
            txtCuenta.Text = "";
            txtConcepto.Text = "";
            txtFecha.Text = "";
            txtValor.Text = "";
        }

        public void CargarTabla()
        {
            Transaccion = new LibroDiario();
            libroDiario= Transaccion.CargarLibroDiario();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = libroDiario;
            dataGridView1.Visible = true;
        }


        private void btnCalcularSalario_Click_1(object sender, EventArgs e)
        {
            try
            {
                Transaccion = new LibroDiario();
                Transaccion.InsertarAsiento(txtCod.Text, txtCuenta.Text, txtConcepto.Text, txtFecha.Value.ToShortDateString(), Convert.ToString(txtAsiento.Value), Convert.ToString(txtOrden.Value), txtNaturaleza.SelectedItem.ToString(), Convert.ToDouble(txtValor.Text));
                CrearAsiento();
                Limpiar();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FrmCargarCatalogoDeCuenta F = new FrmCargarCatalogoDeCuenta();
            F.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBalanceDeComprobacion F = new FrmBalanceDeComprobacion();
            F.Show();
        }

        private void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            FrmMayorizacion F = new FrmMayorizacion();
            F.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                LibroDiarioDoc doc = new LibroDiarioDoc();
                doc.PrintDocument(libroDiario);
                MessageBox.Show("Archivo guardado con éxito en: " + doc.path, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(doc.path);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Ocurrió un problema al intentar guardar el documento" + ee, "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Transaccion = new LibroDiario();
                Transaccion.EliminarAsiento(DataGridViewUtils.GetValorCelda(dataGridView1, 1), DataGridViewUtils.GetValorCelda(dataGridView1, 2));
                MessageBox.Show("Asiento Contable Eliminado Con Exito");
                CargarTabla();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Ocurrió un erro al intentar eliminar el registro:\n" + ee.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmBalanceGeneral F = new FrmBalanceGeneral();
            F.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Transaccion = new LibroDiario();
                Transaccion.EliminarTabla();
                MessageBox.Show("Base de Datos Limpia");
                CargarTabla();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    }

