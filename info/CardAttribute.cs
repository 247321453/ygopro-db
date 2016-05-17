/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2016/5/17
 * 时间: 9:09
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace ygopro.info
{
    /// <summary>
    /// 卡片属性
    /// </summary>
    public enum CardAttribute : int
    {
        /// <summary>
        /// 地
        /// </summary>
        ATTRIBUTE_EARTH = 0x01,
        /// <summary>
        /// 水
        /// </summary>
        ATTRIBUTE_WATER = 0x02,
        /// <summary>
        /// 炎
        /// </summary>
        ATTRIBUTE_FIRE = 0x04,
        /// <summary>
        /// 风
        /// </summary>
        ATTRIBUTE_WIND = 0x08,
        /// <summary>
        /// 光
        /// </summary>
        ATTRIBUTE_LIGHT = 0x10,
        /// <summary>
        /// 暗
        /// </summary>
        ATTRIBUTE_DARK = 0x20,
        /// <summary>
        /// 神
        /// </summary>
        ATTRIBUTE_DEVINE = 0x40,
    }
}
