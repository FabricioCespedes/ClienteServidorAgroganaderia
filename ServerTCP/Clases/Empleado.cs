using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP.Clases
{
    class Empleado : Persona
    {
        // Definición de variable.
        private decimal salario;

        private string usuario;

        private string contraseña;

        private int estado;

        public Empleado()
        {
        }


        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Empleado(int identificacion, string nombre, string primerApellido, string segundaApellido, decimal salario, string usuario, string contraseña = null, int estado = 0)
           : base(identificacion, nombre, primerApellido, segundaApellido)
        {
            this.Salario = salario;
            this.Usuario = usuario;
            this.contraseña = contraseña;
            this.estado = estado;
        }

        // Métodos establecer y obtener.
        public decimal Salario { get => salario; set => salario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Usuario { get => usuario; set => usuario = value; }
    }
}
