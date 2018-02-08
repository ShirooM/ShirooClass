using System;
using System.Collections.Generic;
using System.Text;

namespace ShirooClass
{
    class SConst
    {
        /// <summary>
        /// 从原始服务器，不是从缓存中强制下载请求的文件、 对象或目录列表。
        /// </summary>
        public const uint INTERNET_FLAG_RELOAD = 2147483648;
        /// <summary>
        /// 不自动添加请求的 cookie 标题，并不会自动返回的 cookie 向数据库中添加 cookie。
        /// </summary>
        public const int INTERNET_FLAG_NO_COOKIES = 524288;
        /// <summary>
        /// 不能自动处理中HttpSendRequest的重定向。
        /// </summary>
        public const int INTERNET_FLAG_NO_AUTO_REDIRECT = 2097152;
        /// <summary>
        /// 使用安全的事务语义。这会转换为使用安全套接字层/专用通信技术 （SSL/厘），只是在 HTTP 请求中有意义。
        /// </summary>
        public const int INTERNET_FLAG_SECURE = 8388608;
        /// <summary>
        /// 禁用检测这种特殊类型的重定向。当使用此标志时，WinINet 函数以透明方式允许重定向从 HTTP 到 HTTPS Url。
        /// </summary>
        public const int INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTPS = 16384;
        /// <summary>
        /// HTTP 服务。
        /// </summary>
        public const int INTERNET_SERVICE_HTTP = 3;
        /// <summary>
        /// 接收服务器返回的所有头。每个标题由回车/换行（CR / LF）序列分隔。
        /// </summary>
        public const int HTTP_QUERY_RAW_HEADERS_CRLF = 22;
        /// <summary>
        /// 不使用代理
        /// </summary>
        public const int INTERNET_OPEN_TYPE_DIRECT = 1;
        /// <summary>
        /// 使用代理
        /// </summary>
        public const int INTERNET_OPEN_TYPE_PROXY = 3;
        public const int INTERNET_COOKIE_THIRD_PARTY = 16;

        public const int NULL = 0;
        public const int QS_TIMER = 0x10;

        public const int BUFFER_SIZE = 1024;
        public const int DefaultTimeout = 2 * 60 * 1000; // 2 minutes timeout
    }
}
