using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Query.Infra.CrossCutting.Tools.Logging
{
    public interface IProducerLogger
    {
        public void Error(string message);
        public void Information(string message);
    }
}
