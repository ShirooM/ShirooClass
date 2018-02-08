using System;
using System.Collections.Generic;
using System.Text;

namespace ShirooClass
{
    class Class1
    {
        static void Main()
        {
            SHttp http = new SHttp("http://www.baidu.com");
            byte[] httpCode = http.WinInetHttp();
            httpCode=Encoding.Convert(Encoding.GetEncoding("utf-8"), Encoding.GetEncoding("gb2312"), httpCode);
            Console.WriteLine(Encoding.Default.GetString(httpCode));
        }
    }
}
