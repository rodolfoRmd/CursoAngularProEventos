using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersist<T> where T: class
    {
        //Geral
         void Add(T entity) ;
         void Update(T entity);
         void Delete(T entity) ;
         void DeleteRange(T[] entityArr) ;
         Task<bool> SaveChangesAsync();


    }
}