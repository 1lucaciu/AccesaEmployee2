using System;
using System.IO;

namespace AccesaEmployee
{
    internal class DataContractJsonSerializerx
    {
        private Type type;

        public DataContractJsonSerializer(Type type)
        {
            this.type = type;
        }

        internal void WriteObject(MemoryStream stream1, object p)
        {
           
        }

        internal Employee ReadObject(MemoryStream ms)
        {
            throw new NotImplementedException();
        }
    }
}