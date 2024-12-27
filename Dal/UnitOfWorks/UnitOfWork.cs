using Dal.Context;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IRepository<Ciudad> _ciudadRepository { get; }
        private IRepository<Departamento> _departamentoRepository { get; } 

        private IRepository<Pais> _paisRepository { get; }
        public IRepository<Ciudad> CiudadRepository
        {

            get
            {
                return _ciudadRepository ?? new Repository<Ciudad>(_applicationDbContext);
            }
        }

        public IRepository<Departamento> DepartamentoRepository
        {

            get
            {
                return _departamentoRepository ?? new Repository<Departamento>(_applicationDbContext);
            }
        }

        public IRepository<Pais> PaisRepository
        {

            get
            {
                return _paisRepository ?? new Repository<Pais>(_applicationDbContext);
            }
        }

        public void Save()
        {
            this._applicationDbContext.SaveChanges();
        }

        public Task SaveAsync()
        {
           return this._applicationDbContext.SaveChangesAsync();
        }
    }
}
