using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using ServerTCP.Clases;
using Libreria;

namespace ServerTCP
{
    class Metodos
    {
        private ConexionBD Conexion = new ConexionBD(); // Llama a la instancia que establece conexión.

        private SqlCommand cmd; // Comando para escribir sentencias en la base de datos.

        private SqlDataReader Lector; // Lee de la base de datos.

        public string Login(string user, string pass) // Función que revisa si la contraseña y el usuario que se recibe del cliente es correcto.
        {
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Empleado where Usuario=@user and Contrasena=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();

            if (Existe == true)
            {
                if (RevisarEstado(user) == true)
                {

                    return "Admitido";
                }
                else
                {
                    return "Usuario inactivo";
                }
            }
            else
            {
                return "usuario no encontrado";
            }
        }

        public void ActivarEmpleado(int numero) // Función que activa empleado.
        {
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "UPDATE dbo.Empleado SET Estado = 1  WHERE Identificacion =@id ";
            cmd.Parameters.AddWithValue("@id", numero);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            Lector.Close();
            Conexion.Close();

        }

        public List<Finca> ObtenerFincas() // Obtiene lista de tipo finca extraidas de la base de datos.
        {
            List<Finca> lista = new List<Finca>();
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select Id_Finca,Nombre,Tamanno, Direccion,Telefono From dbo.Finca";
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            while (Lector.Read())
            {
                Finca finca = new Finca();
                finca.NumeroFinca = Convert.ToInt32(Lector[0]);
                finca.Nombre = Lector[1].ToString();
                finca.TamanioFinca = Convert.ToInt32(Lector[2]);
                finca.DirreccionFinca = Lector[3].ToString();
                finca.Telefono = Convert.ToInt32(Lector[4]);

                lista.Add(finca);
            }
            Lector.Close();
            Conexion.Close();
            return lista;
        }
        public List<Raza> ObtenerRaza() // Obtiene lista de tipo raza extraidas de la base de datos.
        {
            List<Raza> lista = new List<Raza>();
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select Id_Raza,Descripcion From dbo.Raza";
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            while (Lector.Read())
            {
                Raza raza = new Raza();
                raza.Codigo = Convert.ToInt32(Lector[0]);
                raza.DescripcionRaza = Lector[1].ToString();


                lista.Add(raza);
            }
            Lector.Close();
            Conexion.Close();
            return lista;
        }


        public List<Animal> ObtenerAnimal() // Obtiene una lista de empleados obtenidos de la base de datos.
        {
            List<Animal> lista = new List<Animal>();
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * From dbo.Animales";
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            while (Lector.Read())
            {
                Animal animal = new Animal();
                Animal animalMadre = new Animal();
                Animal animalPadre = new Animal();
                animal.Identificacion = Convert.ToInt32(Lector[0]);
                animal.Nombre = Lector[1].ToString();
                animal.FincaDelAnimal.NumeroFinca = Convert.ToInt32(Lector[2]);
                animal.RazaAnimal.Codigo = Convert.ToInt32(Lector[3]);
                animal.FechaNacimiento = Lector.GetDateTime(4);
                animal.Sexo = Convert.ToInt32(Lector[5]);
                if(Lector[6] == DBNull.Value)
                {
                    animal.Madre.Identificacion = null;
                }
                else
                {
                    animal.Madre.Identificacion = Convert.ToInt32(Lector[6]);
                }
                if (Lector[7] == DBNull.Value)
                {
                    animal.Padre.Identificacion = null;
                }
                else
                {
                    animal.Padre.Identificacion = Convert.ToInt32(Lector[7]);
                }
                
                lista.Add(animal);
            }
            Lector.Close();
            Conexion.Close();
            return lista;
        }




        public List<Empleado> ObtenerEmpleados() // Obtiene una lista de empleados obtenidos de la base de datos.
        {
            List<Empleado> lista = new List<Empleado>();
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select Identificacion,Nombre,Primer_Apellido,Segundo_Apellido,Salario,Usuario,Contrasena, Estado From dbo.Empleado";
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            while (Lector.Read())
            {
                Empleado empleado = new Empleado();
                empleado.Identificacion = Convert.ToInt32(Lector[0]);
                empleado.Nombre = Lector[1].ToString();
                empleado.PrimerApellido = Lector[2].ToString();
                empleado.SegundaApellido = Lector[3].ToString();
                empleado.Usuario = Lector[4].ToString();
                empleado.Contraseña = Lector[5].ToString();
                empleado.Estado = Convert.ToInt32(Lector[6]);

                lista.Add(empleado);
            }
            Lector.Close();
            Conexion.Close();
            return lista;
        }

        public List<Duenio> ObtenerDuenios() // Obtiene una lista de dueños obtenidos de la base de datos.
        {
            List<Duenio> lista = new List<Duenio>();

            Metodos c = new Metodos();

            List<Finca> lista2 = new List<Finca>();

            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select Identificacion,Nombre,Primer_Apellido, Segundo_Apellido,Correo_Electronico,Telefono_Celular,Id_Finca From dbo.Duenno";
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            while (Lector.Read())
            {
                Duenio duenio = new Duenio();
                duenio.Identificacion = Convert.ToInt32(Lector[0]);
                duenio.Nombre = Lector[1].ToString();
                duenio.PrimerApellido = Lector[2].ToString();
                duenio.SegundaApellido = Lector[3].ToString();
                duenio.CorreoElectronico = Lector[4].ToString();
                duenio.NumeroCelular = Convert.ToInt32(Lector[5]);
                lista.Add(duenio);
            }
            Lector.Close();
            Conexion.Close();
            return lista;
        }



        public bool ExisteEmpleado(int Cedula)  // Verifica un empleado con determinada identificación exista.
        {

            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Empleado where Identificacion=@cedu";
            cmd.Parameters.AddWithValue("@cedu", Cedula);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();
            return Existe;
        }

        public bool ExisteFinca(int numeroFinca)  // Verifica una finca con determinada identificación exista.
        {

            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Finca where Id_Finca=@numberFinca";
            cmd.Parameters.AddWithValue("@numberFinca", numeroFinca);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();
            return Existe;
        }

        public bool ExisteRaza(int numeroRaza)  // Verifica una raza con determinada identificación exista.
        {

            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Finca where Id_Rza=@numberRaza";
            cmd.Parameters.AddWithValue("@numberRaza", numeroRaza);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();
            return Existe;
        }

        public bool ExisteDuenio(int numero)  // Verifica un dueño con determinada identificación exista.
        {

            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Duenno where Identificacion=@numeroId";
            cmd.Parameters.AddWithValue("@numeroId", numero);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();
            return Existe;
        }

        public bool RevisarEstado(string id)  // Revisa el estado del empleado.
        {
            cmd = new SqlCommand();
            cmd.Connection = Conexion.Open();
            cmd.CommandText = "Select * from dbo.Empleado where Usuario=@id and Estado=1";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;
            Lector = cmd.ExecuteReader();
            bool Existe = Lector.Read();
            Lector.Close();
            Conexion.Close();
            return Existe;
        }

        public bool InsertEmpleado(Empleado empleado) // Inserta el empleado a la base de datos.
        {
            if (ExisteEmpleado(empleado.Identificacion))
            {
                return false;
            }
            else
            {
                cmd = new SqlCommand();
                cmd.Connection = Conexion.Open();
                cmd.CommandText = "INSERT INTO dbo.Empleado (Identificacion,Nombre,Primer_Apellido,Segundo_Apellido,Salario,Usuario,Contrasena, Estado) Values (@cedu ,@nombre, @primerApellido, @segundoApellido, @salario, @usuario, @contrasena, @estado)";
                cmd.Parameters.AddWithValue("@cedu", empleado.Identificacion);
                cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@primerApellido", empleado.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", empleado.SegundaApellido);
                cmd.Parameters.AddWithValue("@salario", empleado.Salario);
                cmd.Parameters.AddWithValue("@usuario", empleado.Usuario);
                cmd.Parameters.AddWithValue("@contrasena", empleado.Contraseña);
                cmd.Parameters.AddWithValue("@estado", empleado.Estado);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Lector.Close();
                Conexion.Close();
                return true;
            }
        }


        public bool InsertDuenio(Duenio dueno) // Inserta el dueño a la base de datos.
        {
            if (ExisteDuenio(dueno.Identificacion))
            {
                return false;
            }
            else
            {
                cmd = new SqlCommand();
                cmd.Connection = Conexion.Open();
                cmd.CommandText = "INSERT INTO dbo.Duenno (Identificacion,Nombre,Primer_Apellido,Segundo_Apellido,Correo_Electronico,Telefono_Celular, Id_Finca) Values (@cedu ,@nombre, @primerApellido, @segundoApellido, @salario, @usuario, @contrasena)";
                cmd.Parameters.AddWithValue("@cedu", dueno.Identificacion);
                cmd.Parameters.AddWithValue("@nombre", dueno.Nombre);
                cmd.Parameters.AddWithValue("@primerApellido", dueno.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", dueno.SegundaApellido);
                cmd.Parameters.AddWithValue("@salario", dueno.CorreoElectronico);
                cmd.Parameters.AddWithValue("@usuario", dueno.NumeroCelular);
                cmd.Parameters.AddWithValue("@contrasena", dueno.FincaDuenio.NumeroFinca);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Lector.Close();
                Conexion.Close();
                return true;
            }
        }


        public bool InsertRaza(Raza raza) // Inserta raza a la base de datos.
        {
            if (ExisteEmpleado(raza.Codigo))
            {
                return false;
            }
            else
            {
                cmd = new SqlCommand();
                cmd.Connection = Conexion.Open();
                cmd.CommandText = "INSERT INTO dbo.Raza (Id_Raza,Descripcion) Values (@idRaza ,@descripcion)";
                cmd.Parameters.AddWithValue("@idRaza", raza.Codigo);
                cmd.Parameters.AddWithValue("@descripcion", raza.DescripcionRaza);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Lector.Close();
                Conexion.Close();
                return true;
            }
        }

        public bool InsertAnimal(Animal animal) // Inserta raza a la base de datos.
        {

            try
            { // Intenta guardar los datos que ingresa el usuario desde la interfaz.

                cmd = new SqlCommand();
                cmd.Connection = Conexion.Open();
                cmd.CommandText = "INSERT INTO dbo.Animales (Id_animal,Nombre,Id_Finca,Id_Raza,Fecha_Nacimiento,Sexo,Id_Madre,Id_Padre) Values (@idRaza ,@descripcion,@a,@b,@c,@d,@e,@f,@g)";
                cmd.Parameters.AddWithValue("@idRaza", animal.Identificacion);
                cmd.Parameters.AddWithValue("@descripcion", animal.Nombre);
                cmd.Parameters.AddWithValue("@a", animal.Nombre);
                cmd.Parameters.AddWithValue("@b", animal.FincaDelAnimal.NumeroFinca);
                cmd.Parameters.AddWithValue("@c", animal.RazaAnimal.Codigo);
                cmd.Parameters.AddWithValue("@d", animal.FechaNacimiento);
                cmd.Parameters.AddWithValue("@e", animal.Sexo);
                if(animal.Madre == null)
                {
                    cmd.Parameters.AddWithValue("@f", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@f", animal.Madre.Identificacion);
                }
                if (animal.Padre == null)
                {
                    cmd.Parameters.AddWithValue("@g", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@g", animal.Padre.Identificacion);
                }

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Lector.Close();
                Conexion.Close();
                return true;


            }
            catch (Exception)
            {
                return false;
            }



        }

        public bool InsertFinca(Finca finca) // Inserta el finca a la base de datos.
        {
            if (ExisteEmpleado(finca.NumeroFinca))
            {
                return false;
            }
            else
            {
                cmd = new SqlCommand();
                cmd.Connection = Conexion.Open();
                cmd.CommandText = "INSERT INTO dbo.Finca (Id_Finca,Nombre,Tamanno,Direccion,Telefono) Values (@numeroFinca ,@nombre, @tamanio, @direccion, @telefono)";
                cmd.Parameters.AddWithValue("@numeroFinca", finca.NumeroFinca);
                cmd.Parameters.AddWithValue("@nombre", finca.Nombre);
                cmd.Parameters.AddWithValue("@tamanio", finca.TamanioFinca);
                cmd.Parameters.AddWithValue("@direccion", finca.DirreccionFinca);
                cmd.Parameters.AddWithValue("@telefono", finca.Telefono);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Lector.Close();
                Conexion.Close();
                return true;
            }
        }



    }


}
