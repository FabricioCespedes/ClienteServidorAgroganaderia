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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ServerTCP.Clases;
using System.Runtime.Serialization.Formatters.Binary;
using ServerTCP.Vistas;
using Libreria;

namespace ServerTCP
{
    public partial class Form1 : Form
    {
        //Instancias
        Metodos Logica = new Metodos();

        //Variables Conexiones (Direccion y puerto)
        private Int32 Port = 8900;
        private string SIPAdress = "192.168.100.77";

        //Variables Cliente y Servidor (El que recibe y al que se envía)
        private Thread thServer;

        private TcpListener Server;

        private TcpClient Client;

        //Estado del servidor
        private bool ON = false;

        //Delegados
        private delegate void dlgEscribir(string msj);

        public Form1()
        {
            InitializeComponent();
        }

        private void Escribir(string msj) // Imprime mensaje en el formulario.
        {
            if (rtb.InvokeRequired)
            {
                this.Invoke(new dlgEscribir(Escribir), msj);
            }
            else
            {
                rtb.Text += msj + "\n";
            }
        }

        private void StartServer() // Inicia servidor.
        {
            ON = true;
            Server = new TcpListener(IPAddress.Parse(SIPAdress), Port);
            Server.Start();
            Escribir("Servidor Iniciado...");
            while (ON)
            {
                Client = Server.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(Metodos));
                thread.Start(Client);
            }
        }

        private void Metodos(object obj) // Por medio del hilo recibe un cliente.
        {
            TcpClient Cliente = (TcpClient)obj; // Estable conexión del cliente por medio del hilo. 
            NetworkStream Stream = Client.GetStream(); // Objeto stream que permite el intercambio de datos.
            BinaryReader Lector = new BinaryReader(Stream); // Lee en binario el objeto que se recibe.
            BinaryWriter Escritor = new BinaryWriter(Stream); // Escribe en binario el objeto de se envía.

            switch (Lector.ReadInt32()) // Según el caso llama al método.
            {
                case 1:
                    Login(Cliente);
                    break;
                case 2:
                    InsertarEmpleado(Cliente);
                    break;
                case 3:
                    Lista(Lector,Escritor);
                    break;
                default:
                    break;
            }

        }

        private void Lista(BinaryReader lector, BinaryWriter escritor)
        {
            List<Finca> lista = new List<Finca>();

            lista = Logica.ObtenerFincas();

            escritor.Write(lista.Count());

            for(int i = 0; i < lista.Count(); i++)
            {
                Finca finca = lista.ElementAt(i);

                escritor.Write(finca.Nombre);
                escritor.Write(finca.NumeroFinca);
            }
        }

        private void Login(object obj)
        {
            // Establece conexión para el inicio del usuario-
            TcpClient Cliente = (TcpClient)obj;

            NetworkStream Stream = Client.GetStream();

            BinaryWriter Escritor = new BinaryWriter(Stream);

            BinaryReader Lector = new BinaryReader(Stream);

            string User = Lector.ReadString();
            string Pass = Lector.ReadString();
            string Repuesta = "";


            Repuesta = Logica.Login(User, Pass);

            Escribir("Cliente " + Repuesta);

            Escritor.Write(Repuesta);

        }

        public void InsertarEmpleado(object obj)
        {
            // Recibe un objeto del cliente para ser insertado.

            TcpClient Cliente = (TcpClient)obj;

            NetworkStream Stream = Client.GetStream();

            BinaryWriter Escritor = new BinaryWriter(Stream);

            BinaryReader Lector = new BinaryReader(Stream);

            int id = Lector.ReadInt32();

            string nombre = Lector.ReadString();

            string apellido = Lector.ReadString();

            string segundoApellido = Lector.ReadString();

            decimal salario = Lector.ReadDecimal();

            string usuario = Lector.ReadString();

            string contrasena = Lector.ReadString();

            int estado = Lector.ReadInt32();

            Empleado empleado = new Empleado(id, nombre, apellido, segundoApellido, salario, usuario, contrasena, estado);

            string Respuesta = "";

            if (Logica.InsertEmpleado(empleado) == true)
            {
                Respuesta = "Se inserto el empleado correctamente";
            }
            else
            {
                Respuesta = "No se agrego el empleado, intente de nuevo";
            }

            Escritor.Write(Respuesta);

            Escribir("Nueva cedula insertada: " + empleado.Identificacion);

        }

        private void btnstart_Click(object sender, EventArgs e)
        {// Inicia el servidor.
            btnstart.Enabled = false;
            rtb.Clear();
            thServer = new Thread(StartServer);
            thServer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnstop_Click(object sender, EventArgs e)
        { // Finaliza el servidor,
            ON = false;
            Server.Stop();
            thServer.Abort();
            btnstart.Enabled = true;
            Escribir("Servidor apagado...");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {  // Opciones del menú.
            VistaValidarUsuario vista = new VistaValidarUsuario();

            vista.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {// Opciones del menú.
            VistaRegistrarDuenio vista = new VistaRegistrarDuenio();

            vista.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {// Opciones del menú.
            VistaRegistrarEmpleado vista = new VistaRegistrarEmpleado();

            vista.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {// Opciones del menú.
            VistaRegistrarFinca vista = new VistaRegistrarFinca();

            vista.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {// Opciones del menú.
            VistaRegistrarRaza vista = new VistaRegistrarRaza();

            vista.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
