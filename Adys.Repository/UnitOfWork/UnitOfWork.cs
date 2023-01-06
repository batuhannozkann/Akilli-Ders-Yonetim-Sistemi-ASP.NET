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
        protected readonly IdentityContext _identityContext;

        public UnitOfWork(AdysAppContext adysContext, IdentityContext identityContext)
        {
            _adysContext = adysContext;
            _identityContext = identityContext;
        }
  
        public void Commit()
        {
            _adysContext.SaveChanges();
            _identityContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _adysContext.SaveChangesAsync();
            await _identityContext.SaveChangesAsync();
        }
    }
}
