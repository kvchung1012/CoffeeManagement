using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Constant
{
    public static class Constant
    {
        public static class SaleType
        {
            public static int Money = 1; 
            public static int Persent = 0; 
        }
        public static class DataTypeColumn
        {
            public static int Number = 1;
            public static int String = 2;
            public static int Select = 3;
            public static int SelectMultiple = 4;
            public static int DateTime = 5;
            public static int CheckBox = 6;

        }

        public static class ProductStatus
        {
            public static int Active = 1;
            public static int InActive = 2;
            public static int OutStock = 3;
        }
    }


}
