using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer.PersistentHelper;
using Database.Repositorios;
using Database.modelos;
using System.Data;

namespace BusinessLayer.Servicios
{
    public class ServicioPersona
    {
        private Serializer serializer;
        private string Directory;
        private string FileName;

        private PersonaRepositorio repositorio;
        public ServicioPersona(SqlConnection connection)
        {
            repositorio = new PersonaRepositorio(connection);
                       
        }

        public bool Add(Persona item)
        {
            return repositorio.Add(item);
        }

        public bool Edit(Persona item)
        {
            return repositorio.Edit(item);
        }

        public bool Delete(int id)
        {
            return repositorio.Delete(id);
        }

        public Persona GetById(int id)
        {
            return repositorio.GetById(id);
        }

        public DataTable GetAll()
        {
            return repositorio.GetAll();
        }

    }
}
