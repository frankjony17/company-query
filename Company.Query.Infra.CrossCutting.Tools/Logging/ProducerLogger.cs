using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Query.Infra.CrossCutting.Tools.Logging
{
    public class ProducerLogger : IProducerLogger
    {
        public void Error(string message)
            => Console.WriteLine(message); // TROCAR PARA UM LOG DECENTE

        public void Information(string message)
            => Console.WriteLine(message); // TROCAR PARA UM LOG DECENTE
    }
}
