using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Repository es un patron que te permite tener el código desacoplado.
    /// Es un error de diseño el tener el código fuertemente coplado.
    /// Con el Repository, tu capa de datos es agnóstico del motor, puedes cambiar de MSSQL a MySQL, Postgre, 
    /// SqLite, etcetera
    /// </summary>
    public interface IRepository : IDisposable
    {
        // Cuando veas esos <Type> en C#, estas usando genericos. Eso significa que puedes usar cualquier objeto.
        // Si en tu proyecto tienes el objeto Persona, Compra, Venta, etc. Al usar un genérico, el método puede recibir
        // cualquiera de ellos. Por ejemplo, en lugar de usar
        // MiMetodo(string miString) y luego tener que usar otro para MiMetodo(int miEntero)
        // vas a poder usar MiMetodo(T t), y asi te ahorras tener que escribir dos metodos con diferentes parametros 
        // pero que hacen lo mismo
        TEntity Create<TEntity>(TEntity newEntity) where TEntity : class;

        bool Update<TEntity>(TEntity updateEntity) where TEntity : class;

        bool Delete<TEntity>(TEntity deleteEntity) where TEntity : class;

        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        IEnumerable<TEntity> FindEntitySet<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        IPagedList<TEntity> FindEntitySetPage<TEntity>(Expression<Func<TEntity, bool>> criteria, 
            Expression<Func<TEntity, string>> order, int page, int pageSize) where TEntity : class;
    }
}
