using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DiffieHellman
{
    public class DiffieHellmanEncryption
    {
        private const int BYTES = 32;
        private const int MASK = (-1) >> 1;
        
        private BigInteger privateKey;
        private BigInteger g;
        private BigInteger p;
        
        private byte[] secret;
        public byte[] Secret => secret;

        public Payload GenerateRequest()
        {
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[BYTES];
                random.GetBytes(data);

                privateKey = GetPositiveNumber(data);
                random.GetBytes(data);
                
                p = GetPositiveNumber(data);
                g = 5;

                return new Payload
                {
                    P = p,
                    G = g,
                    SharedSecret = BigInteger.ModPow(g, privateKey, p)
                };
            }
        }
        
        public Payload GenerateResponse(Payload request)
        {
            p = request.P;
            g = request.G;

            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[BYTES];
                random.GetBytes(data);

                privateKey = GetPositiveNumber(data);
            }
            
            secret = BigInteger.ModPow(request.SharedSecret, privateKey, p).ToByteArray();

            return new Payload
            {
                P = p,
                G = g,
                SharedSecret = BigInteger.ModPow(g, privateKey, p)
            };
        }
        
        public void FinalizeKey(Payload response)
        {
            secret = BigInteger.ModPow(response.SharedSecret, privateKey, p).ToByteArray();
        }

        private BigInteger GetPositiveNumber(byte[] data)
        {
            BigInteger result = new BigInteger(data);
            return (result < 0) ? result * (-1) : result;
        }
    }
}
