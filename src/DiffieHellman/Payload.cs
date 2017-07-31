using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DiffieHellman
{
    public struct Payload
    {
        public BigInteger P { get; set; }
        public BigInteger G { get; set; }
        public BigInteger SharedSecret { get; set; }
    }
}
