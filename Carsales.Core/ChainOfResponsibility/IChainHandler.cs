using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.Core.ChainOfResponsibility
{
    public interface IChainHandler
    {
        IChainHandler setNext(IChainHandler handler);

        object handle(object request);
    }
}
