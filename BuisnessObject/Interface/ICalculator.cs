using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObject.Interface
{
    public interface ICalculator
    {
        Task<int> add(int a, int b);
        int divide(int a, int b);
        int multiple(int a, int b);

    }
}
