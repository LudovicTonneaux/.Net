using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSec
{
    public class Persoon
    {
        public string naam;
        public string publicKey;
        public string privateKey;

        public Persoon(string naam)
        {
            this.naam = naam;
            this.publicKey = null;
            this.privateKey = null;
        }

        public override string ToString()
        {
            return this.naam;
        }
    }
}
