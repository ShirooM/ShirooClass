using System;
using System.Collections.Generic;
using System.Text;

namespace ShirooClass
{
    class SString
    {
        /// <summary>
        /// 取文本中间，如果头文本找不到则从起始位置开始，尾文本找不到则默认文本结尾。
        /// </summary>
        /// <param name="str">待处理的文本</param>
        /// <param name="startStr">头文本</param>
        /// <param name="endStr">尾文本</param>
        /// <param name="startIndex">起始位置，从0开始。</param>
        /// <param name="ignoreCase">区分大小写，默认为false不区分。</param>
        /// <param name="IncludeHeadStr">是否包含头文本，默认为false不包含。</param>
        /// <returns></returns>
        public static string GetSubString(string str,string startStr,string endStr,int startIndex=0,bool ignoreCase=false,bool IncludeHeadStr=false)
        {
            StringComparison ignore_case;
            if (ignoreCase)
            {
                //区分大小写
                ignore_case = StringComparison.Ordinal;
            }
            else
            {
                //不区分大小写
                ignore_case = StringComparison.OrdinalIgnoreCase;
            }

            int start_index = str.IndexOf(startStr, startIndex, str.Length - startIndex, ignore_case);
            if (start_index != -1)
            {
                //说明找到的头文本。
                if (!IncludeHeadStr)
                {
                    //如果为假，则不包含头文本。
                    start_index += startStr.Length;
                }
            }
            else
            {
                start_index = 0;//没有找到头文本，则默认为起始为值 0
            }
            int end_index = str.IndexOf(endStr,start_index,str.Length-start_index,ignore_case);
            if (end_index == -1)
            {
                end_index = str.Length;
            }
            return str.Substring(start_index, end_index - start_index);
        }
        /// <summary>
        /// 子文本替换
        /// </summary>
        /// <param name="str">欲被替换的文本</param>
        /// <param name="oldStr">欲被替换的子文本</param>
        /// <param name="newStr">用作替换的子文本</param>
        /// <param name="startIndex">进行替换的起始位置，默认从0开始</param>
        /// <param name="replaceNum">替换进行的次数，默认0，替换所有</param>
        /// <param name="ignoreCase">是否区分大小写，为真区分大小写，为假不区分。</param>
        /// <returns></returns>
        public static string ReplaceStr(string str,string oldStr, string newStr,int startIndex=0,int replaceNum=0,bool ignoreCase=false)
        {
            if(startIndex<=0 && replaceNum<=0 && ignoreCase)
            {
                //区分大小写，起始位置0，全部替换，可以直接使用下面方法。
                return str.Replace(oldStr, newStr);
            }
            StringComparison ignore_case;
            if (ignoreCase)
            {
                ignore_case = StringComparison.Ordinal;//区分大小写
            }
            else
            {
                ignore_case = StringComparison.OrdinalIgnoreCase;//不区分
            }

            int strIndex=str.IndexOf(oldStr, startIndex, str.Length - startIndex, ignore_case);//从指定起始位置寻找被替换的文本。
            if (replaceNum <= 0)
            {
                //替换全部
                while (strIndex != -1)//寻找到结果的话
                {
                    //先把找到的字符删除掉。
                    str = str.Remove(strIndex,oldStr.Length);
                    //然后再插入新的字符。
                    str = str.Insert(strIndex, newStr);
                    //然后继续寻找下一个位置。
                    strIndex= str.IndexOf(oldStr, strIndex, str.Length - strIndex, ignore_case);
                }
            }
            else
            {
                //替换指定的次数
                for (int i=0;i<replaceNum;i++)
                {
                    if (strIndex != -1)
                    {
                        //先把找到的字符删除掉。
                        str = str.Remove(strIndex, oldStr.Length);
                        //然后再插入新的字符。
                        str = str.Insert(strIndex, newStr);
                        //然后继续寻找下一个位置。
                        strIndex = str.IndexOf(oldStr, strIndex, str.Length - strIndex, ignore_case);
                    }
                }
            }
            return str;
        }
    }
}
