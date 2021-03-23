using Database.modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositorios
{
    public class PersonaRepositorio
    {

        private SqlConnection _connection;

        public PersonaRepositorio(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Add(Persona item)
        {
            //prepared statement - Sql injection
            SqlCommand command = new SqlCommand("insert into Personas(Nombre,Apellido,Telefono) values(@name,@lastname,@phone)", _connection);

            command.Parameters.AddWithValue("@name", item.Nombre);
            command.Parameters.AddWithValue("@lastname", item.Apellido);
            command.Parameters.AddWithValue("@phone", item.Telefono);

            return ExecuteDml(command);
        }

        public bool Edit(Persona item)
        {
            //prepared statement - Sql injection
            SqlCommand command = new SqlCommand("update Personas set Nombre=@name,Apellido=@lastname,Telefono=@phone where Id = @id", _connection);

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@name", item.Nombre);
            command.Parameters.AddWithValue("@lastname", item.Apellido);
            command.Parameters.AddWithValue("@phone", item.Telefono);

            return ExecuteDml(command);
        }

        public bool Delete(int Id)
        {
            //prepared statement - Sql injection
            SqlCommand command = new SqlCommand("delete Personas where Id= @id", _connection);

            command.Parameters.AddWithValue("@id", Id);          

            return ExecuteDml(command);
        }

        public DataTable GetAll()
        {            
            SqlDataAdapter query = new SqlDataAdapter("select Id as Codigo,Nombre,Apellido,Telefono from Personas ", _connection);
            return LoadData(query);
        }

        public Persona GetById(int Id)
        {
            _connection.Open();

            SqlCommand command = new SqlCommand("Select Id,Nombre,Apellido,Telefono from Personas where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", Id);

            SqlDataReader reader = command.ExecuteReader();

            Persona retorno = new Persona();

            while (reader.Read())
            {
                retorno.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                retorno.Nombre = reader.IsDBNull(1) ? "" : reader.GetString(1);
                retorno.Apellido = reader.IsDBNull(2) ? "" : reader.GetString(2);
                retorno.Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3);
            }

            reader.Close();
            reader.Dispose();

            _connection.Close();
            return retorno;
        }


        private DataTable LoadData(SqlDataAdapter query)
        {
            try
            {
                DataTable data = new DataTable();
                _connection.Open();

                query.Fill(data);

                _connection.Close();

                return data;

            }
            catch(Exception e)
            {
                return null;
            }

        }

            private bool ExecuteDml(SqlCommand query)
        {
            try
            {
                _connection.Open();

                query.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}
