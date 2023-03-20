using FinalXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalXamarin.Repository
{
    public class ApiRepository
    {
        readonly SQLiteAsyncConnection database;

        public ApiRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            //Creando La tabla tipo Persona
            database.CreateTableAsync<PersonaModel>().Wait();
           
        }
        ///Metodo Para solicitar los datos para llenar la lista de Personas
        public Task<List<PersonaModel>> GetPersonasAsync()
        {
            return database.Table<PersonaModel>().ToListAsync();
        }


        //Metodo Para solicitar los datos por ID y poderlos modificar Persona
        public Task<PersonaModel> GetPersonasAsync(int id)
        {
            return database.Table<PersonaModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        //Metodo para guardar el listado o registro de Personas en la base de datos
        public Task<int> SavePersonasAsync(PersonaModel good)
        {
            if (good.ID != 0)
            {
                return database.UpdateAsync(good);
            }
            else
            {
                return database.InsertAsync(good);
            }
        }

        //Para borrar un registro del listado de Persona
        public Task<int> DeletePersonasAsync(PersonaModel good)
        {
            return database.DeleteAsync(good);
        }
    }
}
