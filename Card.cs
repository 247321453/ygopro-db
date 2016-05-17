using System;

namespace ygopro
{
	public struct Card
	{
		/// <summary>卡片密码</summary>
		public Int64 id;
		/// <summary>卡片规则</summary>
		public Int32 ot;
		/// <summary>卡片同名卡</summary>
		public Int64 alias;
		/// <summary>卡片系列号</summary>
		public Int64 setcode;
		/// <summary>卡片种类</summary>
		public Int64 type;
		/// <summary>攻击力</summary>
		public Int32 Attack;
		/// <summary>防御力</summary>
		public Int32 Defense;
		/// <summary>卡片等级</summary>
		public Int64 level;
		/// <summary>卡片种族</summary>
		public Int64 race;
		/// <summary>卡片属性</summary>
		public Int32 attribute;
		/// <summary>效果种类</summary>
		public Int64 category;
		/// <summary>卡片名称</summary>
		public String name;
		/// <summary>描述文本</summary>
		public String desc;
		/// <summary>
		/// 
		/// </summary>
		public String[] str;
	
	}
}