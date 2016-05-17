using System;

namespace ygopro
{
	public struct Card
	{
		/// <summary>卡片密码</summary>
		public Int64 Id;
		/// <summary>卡片规则</summary>
		public Int32 Ot;
		/// <summary>卡片同名卡</summary>
		public Int64 Alias;
		/// <summary>卡片系列号</summary>
		public Int64 SetCode;
		/// <summary>卡片种类</summary>
		public Int64 Type;
		/// <summary>攻击力</summary>
		public Int32 Attack;
		/// <summary>防御力</summary>
		public Int32 Defense;
		/// <summary>卡片等级</summary>
		public Int64 Level;
		/// <summary>卡片种族</summary>
		public Int64 Race;
		/// <summary>卡片属性</summary>
		public Int32 Attribute;
		/// <summary>效果种类</summary>
		public Int64 Category;
		/// <summary>卡片名称</summary>
		public String Name;
		/// <summary>描述文本</summary>
		public String Desc;
		/// <summary>
		/// 
		/// </summary>
		public String[] Str;
	
	}
}