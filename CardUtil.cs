/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2016/5/17
 * 时间: 8:55
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using ygopro.info;
using System.Globalization;

namespace ygopro
{
	public static class CardUtil
	{
		#region 属性设置
		private const int SETCODE_MAX = 4;
		public static long[] GetSetCode(Card card)
		{
			long[] setcodes = new long[SETCODE_MAX];
			for (int i = 0,k = 0; i < SETCODE_MAX; k += 0x10, i++)
			{
				setcodes[i] = (card.SetCode >> k) & 0xffff;
			}
			return setcodes;
		}
		public static void SetSetCode(Card card,params long[] setcodes)
		{
			int i = 0;
			card.SetCode = 0;
			if (setcodes != null)
			{
				foreach (long sc in setcodes)
				{
					card.SetCode += (sc << i);
					i += 0x10;
				}
			}
		}
		public static void SetSetCode(Card card,params string[] setcodes)
		{
			int i = 0;
			card.SetCode = 0;
			long temp;
			if (setcodes != null)
			{
				foreach (string sc in setcodes)
				{
					long.TryParse(sc, NumberStyles.HexNumber, null, out temp);
					card.SetCode += (temp << i);
					i += 0x10;
				}
			}
		}
		public static long GetLeftScale(Card card)
		{
			return (card.Level >> 0x18) & 0xff;
		}
		public static long GetRightScale(Card card)
		{
			return (card.Level >> 0x10) & 0xff;
		}
		#endregion

		#region 比较、哈希值、操作符
		/// <summary>
		/// 比较
		/// </summary>
		/// <param name="obj">对象</param>
		/// <returns>结果</returns>
		public static bool Equals(Card card,object obj)
		{
			if (obj is Card){
				Card other = (Card)obj;
				bool equalBool=EqualsData(card,other);
				if(!equalBool)
					return false;
				else if (card.Str.Length != other.Str.Length)
					equalBool = false;
				else
				{
					int l = card.Str.Length;
					for (int i = 0; i < l; i++)
					{
						if (!card.Str[i].Equals(other.Str[i]))
						{
							equalBool = false;
							break;
						}
					}
				}
				return equalBool;
			}
			else
				return false;
		}
		/// <summary>
		/// 比较卡片，除脚本提示文本
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool EqualsData(Card card,Card other)
		{
			bool equalBool = true;
			if (card.Id != other.Id)
				equalBool = false;
			else if (card.Ot != other.Ot)
				equalBool = false;
			else if (card.Alias != other.Alias)
				equalBool = false;
			else if (card.SetCode != other.SetCode)
				equalBool = false;
			else if (card.Type != other.Type)
				equalBool = false;
			else if (card.Attack != other.Attack)
				equalBool = false;
			else if (card.Defense != other.Defense)
				equalBool = false;
			else if (card.Level != other.Level)
				equalBool = false;
			else if (card.Race != other.Race)
				equalBool = false;
			else if (card.Attribute != other.Attribute)
				equalBool = false;
			else if (card.Category != other.Category)
				equalBool = false;
			else if (!card.Name.Equals(other.Name))
				equalBool = false;
			else if (!card.Desc.Equals(other.Desc))
				equalBool = false;
			return equalBool;
		}
		/// <summary>
		/// 得到哈希值
		/// </summary>
		public static int GetHashCode(Card card)
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			int hashCode = card.Id.GetHashCode() + ( card.Name==null?0:card.Name.GetHashCode());
			return hashCode;//member.GetHashCode();
		}

		/// <summary>
		/// 是否是某类型
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsType(Card card,CardType type){
			if((card.Type & (long)type) == (long)type)
				return true;
			return false;
		}
		/// <summary>
		/// 是否是某系列
		/// </summary>
		/// <param name="sc"></param>
		/// <returns></returns>
		public static bool IsSetCode(Card card,long sc)
		{
			long settype = sc & 0xfff;
			long setsubtype = sc & 0xf000;
			long setcode = card.SetCode;
			while (setcode != 0)
			{
				if ((setcode & 0xfff) == settype && (setcode & 0xf000 & setsubtype) == setsubtype)
					return true;
				setcode = setcode >> 0x10;
			}
			return false;
		}
		#endregion


		#region 字符串化
		/// <summary>
		/// 密码字符串
		/// </summary>
		public static string GetIdString(Card card)
		{
			return card.Id.ToString("00000000");
		}
		/// <summary>
		/// 字符串化
		/// </summary>
		public static string ToString(Card card)
		{
			return  card.Name+" ["+GetIdString(card)+"]";
		}
		public static string ToShortString(Card card){
			return card.Name+" ["+GetIdString(card)+"]";
		}
		#endregion
	}
}
