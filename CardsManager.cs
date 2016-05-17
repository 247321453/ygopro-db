using System.Collections.Generic;
using System.Text;
using System;
//#if __MonoCS__
using Mono.Data.Sqlite;
using SQLiteConnection = Mono.Data.Sqlite.SqliteConnection;
using SQLiteCommand = Mono.Data.Sqlite.SqliteCommand;
using SQLiteDataReader = Mono.Data.Sqlite.SqliteDataReader;
using SQLiteTransaction = Mono.Data.Sqlite.SqliteTransaction;
//#else
//using System.Data.SQLite;
//#endif

namespace ygopro
{
	public class CardManager
	{
		private SQLiteConnection connection;
		private string dbPath;
		private string error;
		public CardManager(string dbPath="cards.cdb"){
			this.dbPath=dbPath;
		}
		public void SetDbPath(string dbPath){
			this.dbPath=dbPath;
		}
		public bool Open(){
			if(connection!=null)return false;
			bool rs=false;
			try{
				connection = new SQLiteConnection("Data Source=" + dbPath);
				connection.Open();
				rs=true;
			}catch(Exception e){
				error = e.ToString();
			}
			return rs;
		}
		
		public string getLastError(){
			return error;
		}
		
		public void Close(){
			if(connection!=null){
				connection.Close();
			}
		}

		public Card GetById(long id)
		{
			Card card=new Card();
			card.Id = -1;
			using(SQLiteCommand command = new SQLiteCommand("SELECT datas.*, texts.* FROM datas,texts WHERE datas.id=texts.id"
			                                                +" and datas.id="+id+";",
			                                                connection)){
				using(SQLiteDataReader reader = command.ExecuteReader()){
					if (reader.Read())
					{
						try{
							card.Id = reader.GetInt64(0);
							card.Ot = reader.GetInt32(1);
							card.Alias = reader.GetInt64(2);
							card.SetCode = reader.GetInt64(3);
							card.Type = reader.GetInt64(4);
							card.Level = reader.GetInt64(5);
							card.Race = reader.GetInt32(6);
							card.Attribute = reader.GetInt32(7);
							card.Attack = reader.GetInt32(8);
							card.Defense = reader.GetInt32(9);
							card.Name = reader.GetString(12);
							card.Desc = reader.GetString(13);
							card.Str=new string[0x10];
							for(int i=0 ; i < 0x10 ; i++){
								card.Str[i] = reader.GetString(14+i);
							}
						}catch(Exception e){
							error=e.ToString();
							card.Id = -1;
						}
					}
				}
			}
			return card;
		}
		/// <summary>
		/// 执行sql语句
		/// </summary>
		/// <param name="DB">数据库</param>
		/// <param name="SQLs">sql语句</param>
		/// <returns>返回影响行数</returns>
		public int Command(params string[] SQLs)
		{
			int result = 0;
			//
//			using ( SQLiteTransaction trans = connection.BeginTransaction() )
//			{
			try
			{
				using ( SQLiteCommand cmd = new SQLiteCommand(connection) )
				{
					foreach ( string SQLstr in SQLs )
					{
						cmd.CommandText = SQLstr;
						result += cmd.ExecuteNonQuery();
					}
				}
			}
			catch(Exception e)
			{
				error=e.ToString();
//					trans.Rollback();//出错，回滚
				result = -1;
			}
			finally
			{
//					trans.Commit();
			}
//			}
			return result;
		}
		
		/// <summary>
		/// 插入
		/// </summary>
		/// <param name="c">卡片数据</param>
		/// <param name="ignore">重复则忽略</param>
		public int AddCard(Card c, bool ignore=true){
			String sql=GetInsertSQL(c, ignore);
			return Command(sql);
		}
		
		public int UpdateCard(Card c){
			String sql=GetUpdateSQL(c);
			return Command(sql);
		}
		/// <summary>
		/// 转换为插入语句
		/// </summary>
		/// <param name="c">卡片数据</param>
		/// <param name="ignore">重复则忽略</param>
		/// <param name="hex">数字用十六进制表示</param>
		/// <returns>SQL语句</returns>
		private string GetInsertSQL(Card c, bool ignore=true,bool hex= false)
		{
			StringBuilder st = new StringBuilder();
			if(ignore)
				st.Append("INSERT or ignore into datas values(");
			else
				st.Append("INSERT or replace into datas values(");
			st.Append(c.Id.ToString()); st.Append(",");
			st.Append(c.Ot.ToString()); st.Append(",");
			st.Append(c.Alias.ToString()); st.Append(",");
			if(hex){
				st.Append("0x"+c.SetCode.ToString("x")); st.Append(",");
				st.Append("0x"+c.Type.ToString("x")); st.Append(",");
			}else{
				st.Append(c.SetCode.ToString()); st.Append(",");
				st.Append(c.Type.ToString()); st.Append(",");
			}
			st.Append(c.Attack.ToString()); ; st.Append(",");
			st.Append(c.Defense.ToString()); st.Append(",");
			if(hex){
				st.Append("0x"+c.Level.ToString("x")); st.Append(",");
				st.Append("0x"+c.Race.ToString("x")); st.Append(",");
				st.Append("0x"+c.Attribute.ToString("x")); st.Append(",");
				st.Append("0x"+c.Category.ToString("x")); st.Append(")");
			}else{
				st.Append(c.Level.ToString()); st.Append(",");
				st.Append(c.Race.ToString()); st.Append(",");
				st.Append(c.Attribute.ToString()); st.Append(",");
				st.Append(c.Category.ToString()); st.Append(")");
			}
			if(ignore)
				st.Append(";\nINSERT or ignore into texts values(");
			else
				st.Append(";\nINSERT or replace into texts values(");
			st.Append(c.Id.ToString()); st.Append(",'");
			st.Append(c.Name==null?"":c.Name.Replace("'", "''")); st.Append("','");
			st.Append(c.Desc==null?"":c.Desc.Replace("'", "''"));
			if(c.Str==null){
				c.Str=new string[0x10];
			}
			for ( int i = 0; i < 0x10; i++ )
			{
				st.Append("','"); st.Append(c.Str[i]==null?"":c.Str[i].Replace("'", "''"));
			}
			st.Append("');");
			string sql = st.ToString();
			st = null;
			return sql;
		}

		/// <summary>
		/// 转换为更新语句
		/// </summary>
		/// <param name="c">卡片数据</param>
		/// <returns>SQL语句</returns>
		private string GetUpdateSQL(Card c)
		{
			StringBuilder st = new StringBuilder();
			st.Append("update datas set ot="); st.Append(c.Ot.ToString());
			st.Append(",alias="); st.Append(c.Alias.ToString());
			st.Append(",setcode="); st.Append(c.SetCode.ToString());
			st.Append(",type="); st.Append(c.Type.ToString());
			st.Append(",atk="); st.Append(c.Attack.ToString());
			st.Append(",def="); st.Append(c.Defense.ToString());
			st.Append(",level="); st.Append(c.Level.ToString());
			st.Append(",race="); st.Append(c.Race.ToString());
			st.Append(",attribute="); st.Append(c.Attribute.ToString());
			st.Append(",category="); st.Append(c.Category.ToString());
			st.Append(" where id="); st.Append(c.Id.ToString());
			st.Append("; update texts set name='"); st.Append(c.Name==null?"":c.Name.Replace("'", "''"));
			st.Append("',desc='"); st.Append(c.Desc==null?"":c.Desc.Replace("'", "''")); st.Append("', ");
			if(c.Str==null){
				c.Str=new string[0x10];
			}
			for ( int i = 0; i < 0x10; i++ )
			{
				st.Append("str"); st.Append(( i + 1 ).ToString()); st.Append("='");
				st.Append(c.Str[i]==null?"":c.Str[i].Replace("'", "''"));
				if ( i < 15 )
				{
					st.Append("',");
				}
			}
			st.Append("' where id="); st.Append(c.Id.ToString());
			st.Append(";");
			string sql = st.ToString();
			st = null;
			return sql;
		}
	}
}