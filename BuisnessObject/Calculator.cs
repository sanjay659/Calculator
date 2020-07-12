using BuisnessObject.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObject
{
    public class Calculator : ICalculator
    {
        public object JsonConvert { get; private set; }

        public async Task<int> add(int a, int b)
        {

            var v = await Add(a, b);
            return v;
        }

        async Task<int> Add(int a, int b)
        {

            int idList = a + b;
            return idList;

        }

        public int divide(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int multiple(int a, int b)
        {
            throw new NotImplementedException();
        }
    }
}
