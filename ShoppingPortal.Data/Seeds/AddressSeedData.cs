using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Seeds
{
    public static class AddressSeedData
    {
        public static Address[] GetSeedAddress()
        {
            return new Address[]
            {
                new Address
                {
                    PostalCode = "393120",
                    City = "Bharuch",
                    State = "Gujarat"
                },
                new Address
                {
                    PostalCode = "380051",
                    City = "Ahmedabad",
                    State = "Gujarat"
                },
                new Address
                {
                    PostalCode = "382481",
                    City = "Ahmedabad",
                    State = "Gujarat"
                }
            };
        }
    }
}
