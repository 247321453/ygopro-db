/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2016/5/17
 * 时间: 9:10
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace ygopro.info
{
    public enum CardRule :int
    {
        /// <summary>无</summary>
        NONE = 0,
        /// <summary>OCG</summary>
        OCG =1,
        /// <summary>TCG</summary>
        TCG = 2,
        /// <summary>OT</summary>
        OCGTCG = 3,
        /// <summary>DIY,原创卡</summary>
        DIY = 4,
    }
}
