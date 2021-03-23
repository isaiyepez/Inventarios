using System;
using System.Collections.Generic;
using System.Linq;
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
        // cualquiera de ellos.
        TEntity Create<TEntity>(TEntity newEntity) where TEntity : class;
    }
}
