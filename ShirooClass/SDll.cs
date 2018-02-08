using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ShirooClass
{
    class SDll
    {
        /// <summary>
        /// 初始化 WinINet 函数的应用程序的使用
        /// </summary>
        /// <param name="Agent">User-AgentR的值</param>
        /// <param name="AccessType">所需的访问类型。1：直接连接，3：代理连接</param>
        /// <param name="ProxyName">代理IP地址</param>
        /// <param name="ProxyBypass">如果使用代理，此参数不要设置NULL,设置为""</param>
        /// <param name="dwFlags">默认为0</param>
        /// <returns>返回句柄</returns>
        [DllImport("Wininet.dll")]
        public static extern int InternetOpenA(string Agent, int AccessType, string ProxyName, string ProxyBypass, int dwFlags = 0);
        /// <summary>
        /// 关闭互联网句柄
        /// </summary>
        /// <param name="hInternet">句柄</param>
        /// <returns>如果成功返回true，否则false。</returns>
        [DllImport("Wininet.dll")]
        public static extern bool InternetCloseHandle(int hInternet);
        /// <summary>
        /// 打开给定站点文件传输协议 (FTP) 或 HTTP 会话。
        /// </summary>
        /// <param name="Internet">由InternetOpen返回的句柄。</param>
        /// <param name="ServerName">主机域名，可用Uri.Host获取</param>
        /// <param name="ServerPort">主机端口，可用Uri.Port获取</param>
        /// <param name="Username">指定要登录的用户的名称</param>
        /// <param name="Password">要用来登录的密码</param>
        /// <param name="service">要访问的服务的类型，如FTP，HTTP</param>
        /// <param name="Flags">FTP类型时，使用被动。</param>
        /// <param name="Context">包含一个应用程序定义的值，用于标识在回调中返回的句柄的应用程序上下文的变量的指针</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern int InternetConnectA(int Internet, string ServerName, int ServerPort, string Username, string Password, int service, int Flags, int Context);
        /// <summary>
        /// 创建一个 HTTP 请求句柄。
        /// </summary>
        /// <param name="Connect">由InternetConnect返回 HTTP 会话句柄。</param>
        /// <param name="Verb">访问方式，"GET"，"POST"</param>
        /// <param name="ObjectName">页面地址，不包含域名，可用Uri.PathAndQuery获取</param>
        /// <param name="Version">请求中使用的 HTTP 版本，"HTTP/1.1"</param>
        /// <param name="Referer">指定从中获取 URL 请求 (lpszObjectName) 中的文档的 URL</param>
        /// <param name="AcceptTypes">指示媒体类型的客户所接受</param>
        /// <param name="Flags"></param>
        /// <param name="Context"></param>
        /// <returns>返回 HTTP 请求句柄，如果成功返回句柄</returns>
        [DllImport("Wininet.dll")]
        public static extern int HttpOpenRequestA(int Connect, string Verb, string ObjectName, string Version, string Referer, string AcceptTypes, uint Flags, int Context);
        /// <summary>
        /// 将指定的请求发送到 HTTP 服务器，发送超出正常额外数据使用HttpSendRequestEx。
        /// </summary>
        /// <param name="Request">通过调用HttpOpenRequest函数返回的句柄</param>
        /// <param name="Headers">附加的协议头</param>
        /// <param name="HeadersLength">协议头长度</param>
        /// <param name="Optional">此参数用于POST或PUT的数据提交。即POST数据。</param>
        /// <param name="OptionalLength">POST数据的长度</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool HttpSendRequestA(int Request, string Headers, int HeadersLength, byte[] Optional, int OptionalLength);
        /// <summary>
        /// 从打开的InternetOpenUrl、 FtpOpenFile或HttpOpenRequest函数句柄读取数据。
        /// </summary>
        /// <param name="hFile">从InternetOpenUrl、 FtpOpenFile或HttpOpenRequest返回的句柄。</param>
        /// <param name="Buffer">指向接收的数据的缓冲区</param>
        /// <param name="NumberOfBytesToRead">要读取的字节数</param>
        /// <param name="NumberOfBytesRead">指向一个变量来接收读取的字节数</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool InternetReadFile(int hFile, byte[] Buffer, int NumberOfBytesToRead, out int NumberOfBytesRead);
        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();
        /// <summary>
        /// 检索与 HTTP 请求关联的标头信息。
        /// </summary>
        /// <param name="Request">通过对HttpOpenRequest或InternetOpenUrl函数的调用返回句柄。</param>
        /// <param name="InfoLevel">要检索的属性组合和修改请求的标志。</param>
        /// <param name="Buffer">指向接收请求的信息的缓冲区的指针。此参数不能为空。</param>
        /// <param name="BufferLength">缓冲区的大小</param>
        /// <param name="Index">用于枚举具有相同名称的多个标头从零开始标头索引指向的指针。</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool HttpQueryInfoA(int Request, int InfoLevel, byte[] Buffer, out int BufferLength, out int Index);
        /// <summary>
        /// 检索指定网址的Cookie。
        /// </summary>
        /// <param name="_url">要检索Cookie的URL。</param>
        /// <param name="_cookie_name">要取的Cookie名称</param>
        /// <param name="_cookie_data">指向接收Cookie数据的缓冲区的指针。</param>
        /// <param name="_size">缓冲区的大小 </param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool InternetGetCookieA(string _url, string _cookie_name, byte[] _cookie_data, out int _size);

        [DllImport("Wininet.dll")]
        public static extern bool InternetGetCookieExA(string URL, string CookieName, out string CookieData, int Size, int Flags, int Reserved);
        /// <summary>
        /// 创建与指定的URL相关联的cookie
        /// </summary>
        /// <param name="Url">设置Cookie的URL。</param>
        /// <param name="CookieName">Cookie数据相关联的名称</param>
        /// <param name="CookieData">指向与URL相关联的实际数据</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool InternetSetCookieA(string Url, string CookieName, string CookieData);

        [DllImport("Wininet.dll")]
        public static extern bool InternetGetCookieExA(string URL, string CookieName, byte[] CookieData, out int Size, int Flags, int Reserved);

        [DllImport("Kernel32.dll")]
        public static extern int CreateWaitableTimerA(int lpTimerAttributes, bool bManualReset, int lpTimerName);
        [DllImport("Kernel32.dll")]
        public static extern int SetWaitableTimer(int hTimer, ref long pDueTime, int lPeriod, int pfnCompletionRoutine, int lpArgToCompletionRoutine, bool fResume);
        [DllImport("User32.dll")]
        public static extern int MsgWaitForMultipleObjects(uint nCount, ref int pHandles, bool bWaitAll, int dwMilliseconds, uint dwWakeMask);
        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(int hObject);
    }
}
