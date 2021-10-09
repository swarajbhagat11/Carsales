using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.Core.ChainOfResponsibility
{
    public abstract class ChainHandler : IChainHandler
    {
        private IChainHandler _handler;

        public IChainHandler setNext(IChainHandler handler)
        {
            this._handler = handler;
            return handler;
        }

        public virtual object handle(object request)
        {
            if (this._handler != null)
            {
                return this._handler.handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
