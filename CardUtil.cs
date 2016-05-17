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
				setcodes[i] = (card.setcode >> k) & 0xffff;
			}
			return setcodes;
		}
		public static void SetSetCode(Card card,params long[] setcodes)
		{
			int i = 0;
			card.setcode = 0;
			if (setcodes != null)
			{
				foreach (long sc in setcodes)
				{
					card.setcode += (sc << i);
					i += 0x10;
				}
			}
		}
		public static void SetSetCode(Card card,params string[] setcodes)
		{
			int i = 0;
			card.setcode = 0;
			long temp;
			if (setcodes != null)
			{
				foreach (string sc in setcodes)
				{
					long.TryParse(sc, NumberStyles.HexNumber, null, out temp);
					card.setcode += (temp << i);
					i += 0x10;
				}
			}
		}
		public static long GetLeftScale(Card card)
		{
			return (card.level >> 0x18) & 0xff;
		}
		public static long GetRightScale(Card card)
		{
			return (card.level >> 0x10) & 0xff;
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
			if (obj is Card)
				return card.Equals((Card)obj); // use Equals method below
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
			if (card.id != other.id)
				equalBool = false;
			else if (card.ot != other.ot)
				equalBool = false;
			else if (card.alias != other.alias)
				equalBool = false;
			else if (card.setcode != other.setcode)
				equalBool = false;
			else if (card.type != other.type)
				equalBool = false;
			else if (card.Attack != other.Attack)
				equalBool = false;
			else if (card.Defense != other.Defense)
				equalBool = false;
			else if (card.level != other.level)
				equalBool = false;
			else if (card.race != other.race)
				equalBool = false;
			else if (card.attribute != other.attribute)
				equalBool = false;
			else if (card.category != other.category)
				equalBool = false;
			else if (!card.name.Equals(other.name))
				equalBool = false;
			else if (!card.desc.Equals(other.desc))
				equalBool = false;
			return equalBool;
		}
		/// <summary>
		/// 比较卡片是否一致？
		/// </summary>
		/// <param name="other">比较的卡片</param>
		/// <returns>结果</returns>
		public static bool Equals(Card card,Card other)
		{
			bool equalBool=EqualsData(card,other);
			if(!equalBool)
				return false;
			else if (card.str.Length != other.str.Length)
				equalBool = false;
			else
			{
				int l = card.str.Length;
				for (int i = 0; i < l; i++)
				{
					if (!card.str[i].Equals(other.str[i]))
					{
						equalBool = false;
						break;
					}
				}
			}
			return equalBool;

		}
		/// <summary>
		/// 得到哈希值
		/// </summary>
		public static int GetHashCode(Card card)
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			int hashCode = card.id.GetHashCode() + ( card.name==null?0:card.name.GetHashCode());
			return hashCode;//member.GetHashCode();
		}

		/// <summary>
		/// 是否是某类型
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsType(Card card,CardType type){
			if((card.type & (long)type) == (long)type)
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
			long setcode = card.setcode;
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
			return card.id.ToString("00000000");
		}
		/// <summary>
		/// 字符串化
		/// </summary>
		public static string ToString(Card card)
		{
			return  card.name+" ["+GetIdString(card)+"]";
		}
		public static string ToShortString(Card card){
			return card.name+" ["+GetIdString(card)+"]";
		}
		#endregion
	}
}
