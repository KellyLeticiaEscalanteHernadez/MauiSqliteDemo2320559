using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqliteDemo2320559
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            //Le indica al sistema que crea la tabla de nuestro contexto
            _connection.CreateTableAsync<Cliente>();
        }
        public async Task<List<Cliente>> GetClientes()
        {
            return await _connection.Table <Cliente>().ToListAsync();   
        }
        // este metodo es parea listar los registros del id
        public async Task<Cliente> GetById(int id) 
        {
            return await _connection.Table<Cliente>().Where(x=> x.Id == id).FirstOrDefaultAsync();  
        }

        //metodo pra crear registros
        public async Task Create(Cliente cliente)
        {
            await _connection.InsertAsync(cliente);  
        }
        //metodo para actualizar 
        public async Task Update(Cliente cliente)
        {
            await _connection.UpdateAsync(cliente);
        }
        //metodo para eliminar
        public async Task Delete(Cliente cliente)
        {
            await _connection.DeleteAsync(cliente); 
        }
    }
}


