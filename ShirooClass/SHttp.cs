using System;
using System.Collections.Generic;
using System.Text;

namespace ShirooClass
{
    class SHttp
    {
        public string url="";//访问的网址，必须包含"http://"或"https://"
        public string method = "GET";//访问方式（"GET","POST"）
        public string postData = "";//POST方式，数据提交。格式：“name=value&name1=value1”，如为JSON对象数据，格式：“{"BaseRequest":{"Uin":"xxx1","Sid":"xxx2","Skey":"xxx3","DeviceID":"xxx4"}}”
        public string pushCookie = "";//提交的COOKIE
        public string pushHeaders = "";//附加的协议头，一行一个，用换行符隔开
        public bool redirect = false;//重定向网页，默认：false
        public string proxyIp = "";//代理IP地址。格式"127.0.0.1:1080"

        private string pullCookie = "";//访问后返回的COOKIE
        private string pullHeaders = "";//访问后返回的协议头

        public SHttp(string url="")
        {
            this.url = url;
        }
        public byte[] WinInetHttp()
        {
            if (string.IsNullOrEmpty(this.url))
            {
                return null;
            }
            this.method = this.method.ToUpper();//method自动转换为大写
            string httpStr = this.url.Substring(0, 5).ToLower();//获取url前五个字符，并转换为小写
            bool isHttps = false;
            if (httpStr == "https") {
                isHttps = true;//判断五个字符是否为https，如果是则为https协议
            }
            if (string.IsNullOrEmpty(this.pushHeaders))
            {
                //判断参数协议头是否填写，如没有填写使用下面默认值
                this.pushHeaders = "Accept: */*";
                this.pushHeaders += "\r\nReferer: " + this.url;
                this.pushHeaders += "\r\nAccept-Language: zh-cn";
                this.pushHeaders += "\r\nContent-Type: application/x-www-form-urlencoded";
            }
            else
            {
                //处理掉参数双引号与单引号
                this.pushHeaders = this.pushHeaders.Replace("\"", "");
                this.pushHeaders = this.pushHeaders.Replace("'", "");
            }
            if (!string.IsNullOrEmpty(this.pushCookie))
            {
                //判断参数cookie是否填写，不为空的话。与协议头合并
                this.pushHeaders += "\r\nCookie: " + this.pushCookie;
            }
            string user_agent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0";
            if (pushHeaders.IndexOf("User-Agent") != -1)
            {
                //如果设置得有User-Agent，首先删除首尾空格符号，
                user_agent = pushHeaders.Trim();
                user_agent = SString.GetSubString(user_agent, "User-Agent:", "\r\n");
            }
            int internetHandle=0;
            if (string.IsNullOrEmpty(proxyIp))
            {
                //没有设置代理
                internetHandle=SDll.InternetOpenA(user_agent,SConst.INTERNET_OPEN_TYPE_DIRECT,null,null,0);
            }
            else
            {
                //设置了代理
                if (isHttps)
                {
                    internetHandle = SDll.InternetOpenA(user_agent,SConst.INTERNET_OPEN_TYPE_PROXY,proxyIp,"",0);
                }
                else
                {
                    internetHandle = SDll.InternetOpenA(user_agent,SConst.INTERNET_OPEN_TYPE_PROXY,"http="+proxyIp,"",0);
                }
            }
            if (internetHandle == 0) { return null; }
            Uri uri = new Uri(url);
            int internetConnectHandle = SDll.InternetConnectA(internetHandle, uri.Host, uri.Port, null, null, SConst.INTERNET_SERVICE_HTTP, 0, 0);
            if (internetConnectHandle == 0)
            {
                SDll.InternetCloseHandle(internetHandle);
                return null;
            }
            uint requestTag = SConst.INTERNET_FLAG_RELOAD | SConst.INTERNET_COOKIE_THIRD_PARTY;
            if (!string.IsNullOrEmpty(pushCookie))
            {
                requestTag = requestTag | SConst.INTERNET_FLAG_NO_COOKIES;
            }
            if (redirect)
            {
                requestTag = requestTag | SConst.INTERNET_FLAG_NO_AUTO_REDIRECT;
            }
            if (isHttps)
            {
                requestTag = requestTag | SConst.INTERNET_FLAG_SECURE;
            }
            else
            {
                requestTag = requestTag | SConst.INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTPS;
            }
            int httpRequestHandle = SDll.HttpOpenRequestA(internetConnectHandle,method,uri.PathAndQuery,"HTTP/1.1",null,null,requestTag,0);
            if (httpRequestHandle == 0)
            {
                SDll.InternetCloseHandle(internetConnectHandle);
                SDll.InternetCloseHandle(internetHandle);
                return null;
            }
            bool isTrue = false;
            if (method == "GET")
            {
                isTrue = SDll.HttpSendRequestA(httpRequestHandle, pushHeaders, pushHeaders.Length, null, 0);
            }
            else
            {
                byte[] postDataByte = Encoding.Default.GetBytes(postData);
                isTrue = SDll.HttpSendRequestA(httpRequestHandle, pushHeaders, pushHeaders.Length, postDataByte, postDataByte.Length);
            }
            if (!isTrue)
            {
                SDll.InternetCloseHandle(httpRequestHandle);
                SDll.InternetCloseHandle(internetConnectHandle);
                SDll.InternetCloseHandle(internetHandle);
                return null;
            }
            byte[] pageBody = new byte[0];
            byte[] readByte= new byte[1024];
            int i=0;
            while (true)
            {
                SDll.InternetReadFile(httpRequestHandle, readByte, 1024, out i);
                if (i == 0)
                {
                    break;
                }
                else
                {
                    byte[] tempByte = new byte[i];
                    Array.Copy(readByte, tempByte, i);//这里取出读取到的数据，并填充到tempByte变量里
                    //取到的数据在tempByte里。这是一个数组数据。
                    //把tempByte加进pageBody里面去。
                    byte[] pageBodyTemp = new byte[pageBody.Length + tempByte.Length];//临时变量的长度等于两个的长度。
                    pageBody.CopyTo(pageBodyTemp, 0);//把pageBody复制进临时变量里，起始位置0
                    tempByte.CopyTo(pageBodyTemp, pageBody.Length);//把tempByte复制进临时变量里，起始位置为pageBody的长度。
                    pageBody = pageBodyTemp;
                }
            }
            //第一次是把数据长度取出来。
            SDll.HttpQueryInfoA(httpRequestHandle, SConst.HTTP_QUERY_RAW_HEADERS_CRLF, null, out int queryInfoLength, out int queryInfoIndex);
            //然后告诉下面这货，你有多长，
            byte[] queryInfo = new byte[queryInfoLength];
            //最后再来取数据，放进去
            isTrue = SDll.HttpQueryInfoA(httpRequestHandle, SConst.HTTP_QUERY_RAW_HEADERS_CRLF, queryInfo, out queryInfoLength, out queryInfoIndex);
            //以防万一取回来的里面有set-cookie所以替换成Set-Cookie，
            this.pullHeaders = SString.ReplaceStr(Encoding.Default.GetString(queryInfo), "set-Cookie", "Set-Cookie", 0, 0, true);
            //关闭相应的句柄。
            SDll.InternetCloseHandle(httpRequestHandle);
            SDll.InternetCloseHandle(internetConnectHandle);
            SDll.InternetCloseHandle(internetHandle);

            string[] pullHeadersA = this.pullHeaders.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for(int j = 0; j < pullHeadersA.Length; j++)
            {
                if (pullHeadersA[j].IndexOf("Set-Cookie", 0, pullHeadersA[j].Length, StringComparison.Ordinal)!=-1)
                {
                    //在取回的协议头里寻找是否有Cookie,如果有。
                    pullCookie += SString.GetSubString(pullHeadersA[j].Trim(),"Set-Cookie:",";",0,false,false);
                }
            }
            this.pullCookie = this.pullCookie.Trim();
            return pageBody;
        }

    }
}
