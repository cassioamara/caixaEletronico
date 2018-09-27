using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace caixaEletronico
{
    public partial class Form1 : Form
    {
        int flag;
        string id;

        public Form1()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);            
        }       

        private void button2_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {                   

            if (textBox4.Text == textBox3.Text)
            {

                MySqlConnection conn = new MySqlConnection("User Id=root; Host=localhost; Database=banco");

                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
                MySqlCommand insert = conn.CreateCommand();

                string numero = textBox1.Text;
                string cpf = textBox2.Text;
                string senha = textBox4.Text;
                string limite = textBox6.Text;
                string saldo = textBox5.Text;

                insert.CommandText = "INSERT INTO clientes (cpf,numero,senha,saldo,limite) VALUES ('" + cpf + "', '" + numero + "', '" + senha + "', '" + limite + "', '" + saldo + "');";

                try
                {
                    int result = insert.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Conta criada!");
                    }
                    else
                    {
                        MessageBox.Show(result.ToString());
                        MessageBox.Show("Erro!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                conn.Close();
                tabControl1.SelectTab(0);
            }
            else 
            {
                MessageBox.Show("As senhas não coincidem!");
            }
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("User Id=root; Host=localhost; Database=banco");

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MySqlCommand command = conn.CreateCommand();
            MySqlCommand saldo = conn.CreateCommand();

            int numero = int.Parse(textBox7.Text);
            int senha = int.Parse(textBox8.Text);            

            command.CommandText = "SELECT * FROM clientes WHERE numero="+numero+" AND senha="+senha+" ;";            

            MySqlDataReader reader = command.ExecuteReader();    
        

            if (reader.Read())
            {
                label19.Text = reader["saldo"].ToString();
                tabControl1.SelectTab(3);
                id = reader["id"].ToString();
            }
            else
            {
                MessageBox.Show("Para de zoeira!");
            }

            conn.Close();            
        }

        private void button17_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection("User Id=root; Host=localhost; Database=banco");

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            double valor = double.Parse(textBox9.Text);
            double saldoFinal;

            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "SELECT saldo FROM clientes WHERE id= '" + id + "';";

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

                double saldo = double.Parse(reader["saldo"].ToString());
                reader.Close();

                if (flag == 0) //saque
                {
                    saldoFinal = saldo - valor;
                    MySqlCommand update = conn.CreateCommand();
                    update.CommandText = "UPDATE clientes SET saldo='" + saldoFinal + "' WHERE id= '" + id + "'";
                    update.ExecuteNonQuery();
                    label19.Text = saldoFinal.ToString();
                }
                else if (flag == 1) //depósito
                {
                    saldoFinal = saldo + valor;
                    MySqlCommand update = conn.CreateCommand();
                    update.CommandText = "UPDATE clientes SET saldo='" + saldoFinal + "' WHERE id= '" + id + "'";
                    update.ExecuteNonQuery();
                    label19.Text = saldoFinal.ToString();
                }
                tabControl1.SelectTab(5);
            }
        

        private void button30_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            label9.Text = "Valor saque: ";
            flag = 0;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            label9.Text = "Valor depósito: ";
            flag = 1;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            textBox1.Text = rnd.Next(11111, 99999).ToString();
        }     

        
    }
}
