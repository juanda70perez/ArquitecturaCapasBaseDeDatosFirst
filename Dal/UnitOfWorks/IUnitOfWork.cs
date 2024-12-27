using Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IRepository<Ciudad> CiudadRepository { get; }
        public IRepository<Departamento> DepartamentoRepository { get; }
        public IRepository<Pais> PaisRepository { get; }

        public void Save();

        public Task SaveAsync();

    }
}
