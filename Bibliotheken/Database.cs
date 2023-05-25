/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 01.11.2021
 * Time: 14:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit Datenbanken
	/// </summary>
	public class Database
	{
		/// <summary>
		/// holt Daten aus einer MS SQL Datenbank
		/// </summary>
		/// <param name="ds">Dataset welches befüllt wird</param>
		/// <param name="sql">SQL Befehl zur Selektion der Daten aus der Datenbank</param>
		/// <param name="constr">Connectionstring für die Verbindung zur Datenbank</param>
		/// <returns>Dataset mit den aus der SQL Datenbank abgerufenen Daten</returns>
		public DataSet GetDataFromMSSQLDatabase(DataSet ds,string sql, string constr)
		{
			try 
			{
				SqlConnection Conn = new SqlConnection(constr);  
				SqlCommand selectCMD = new SqlCommand(sql, Conn);  
				selectCMD.CommandTimeout = 30;  
				SqlDataAdapter da = new SqlDataAdapter();  
				da.SelectCommand = selectCMD;  
				Conn.Open();  
				da.Fill(ds, ds.Tables[0].ToString());
				Conn.Close(); 
				return ds;
			}
			catch (Exception)
			{
				return null;
			}
		}
		
		/// <summary>
		/// schreibt Daten in eine MS SQL Datenbank
		/// </summary>
		/// <param name="sql">SQL Befehl zum schreiben der Daten (INSERT, UPDATE, DELETE)</param>
		/// <param name="constr">Connectionstring für die Verbindung zur Datenbank</param>
		/// <returns>anzahl der in dem Zuge geschriebenen Datensätze als INT</returns>
		public int WroteDataToMSSQLDatabase(string sql, string constr)
		{
			try 
			{
				using (SqlConnection connection = new SqlConnection(constr))
			    {
			        SqlCommand command = new SqlCommand(sql, connection);
			        command.Connection.Open();
			        var ret=command.ExecuteNonQuery().ToString();
			        command.Connection.Close();
			        return int.Parse(ret);
				}
			} 
			catch (Exception) 
			{
				throw;
			}
		}		
	}
}