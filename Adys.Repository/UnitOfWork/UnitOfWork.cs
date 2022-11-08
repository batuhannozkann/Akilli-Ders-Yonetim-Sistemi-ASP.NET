using Adys.Core.UnitOfWork;
using Adys.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AdysAppContext _adysContext;

        public UnitOfWork(AdysAppContext adysContext)
        {
            _adysContext = adysContext;
        }

        public void Commit()
        {
            _adysContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _adysContext.SaveChangesAsync();
        }
    }
}
