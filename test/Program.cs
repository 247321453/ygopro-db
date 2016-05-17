/*
 * 由SharpDevelop创建。
 * 用户： Hasee
 * 日期: 2016/5/17
 * 时间: 21:32
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using ygopro;

namespace test
{
	class Program
	{
		public static void Main(string[] args)
		{
			String db="F:\\git\\cards.cdb";
			CardManager manager=new CardManager(db);
			if(manager.Open()){
				Console.WriteLine("open ok");
			}else{
				Console.WriteLine("open fail:"+manager.getLastError());
			}
			Card card=manager.GetById(62121);
			if(card.Id < 0){
				Console.WriteLine("error:"+manager.getLastError());
			}
			
			Console.WriteLine("card str:"+card.Name);
			Console.WriteLine("card str:"+card.Str[0]);
			card.Name=card.Name+"_test";
			Card tmp=new Card();
			tmp.Id=1;
			tmp.Name="test";
			manager.AddCard(tmp);
			manager.Close();
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}