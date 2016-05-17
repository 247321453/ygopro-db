using System.Collections.Generic;
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
	public class CardsManager
	{
		private SQLiteConnection connection;
		private string dbPath;
		public CardsManager(string dbPath="cards.cdb"){
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
			}catch{}
			return rs;
		}
		
		public void Close(){
			if(connection!=null){
				connection.Close();
			}
		}

		public Card GetById(long id)
		{
			
			using(SQLiteCommand command = new SQLiteCommand("SELECT datas.id, ot, alias, "
			                                                +"setcode, type, level, race, attribute, atk, def ,"
			                                                +" name , desc, texts.* FROM datas,texts WHERE datas.id=texts.id",
			                                                connection)){
				using(SQLiteDataReader reader = command.ExecuteReader()){
					if (reader.Read())
					{
						Card card=new Card();
						card.Id = reader.GetInt64(0);
						card.Ot = reader.GetInt32(1);
						card.Alias=reader.GetInt64(2);
						card.SetCode = reader.GetInt64(3);
						card.Type = reader.GetInt64(4);
						card.Level = reader.GetInt64(5);
						card.Race = reader.GetInt32(6);
						card.Attribute = reader.GetInt32(7);
						card.Attack = reader.GetInt32(8);
						card.Defense = reader.GetInt32(9);
						card.Name=reader.GetString(10);
						card.Desc=reader.GetString(11);
						return card;
					}
				}
			}
			return null;
		}
	}
}