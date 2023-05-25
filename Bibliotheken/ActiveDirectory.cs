/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 02.11.2021
 * Time: 16:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.DirectoryServices.AccountManagement;

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit der Active Directory
	/// </summary>
	public class ActiveDirectory
	{
		/// <summary>
		/// Fragt die Active Directory nach Rechnern ab.
		/// </summary>
		/// <param name="ds">leeres Dataset</param>
		/// <param name="Domain">Domaine die abgefragt werden soll</param>
		/// <param name="LDAPPfad">LDAP Pfad welcher abgefragt werden soll</param>
		/// <returns>Dataset mit Computernamen</returns>
		public DataSet GetComputerFromAD(DataSet ds, string Domain, string LDAPPfad)
		{
			try 
			{
				string [] container= new string[]{LDAPPfad};
				DataTable dt =new DataTable();
				dt.Columns.Add("Computer");
				ds.Tables.Add(dt);
				foreach (string cont in container)
				{
		            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Domain, cont);
		            ComputerPrincipal cP = new ComputerPrincipal(ctx);
		            cP.Name = "*";
		            PrincipalSearcher ps = new PrincipalSearcher();
		            ps.QueryFilter = cP;
		            PrincipalSearchResult<Principal> result = ps.FindAll();
		            foreach (ComputerPrincipal p in result)
		            {
	            		dt.Rows.Add(p.Name);
		            }
				}
				return ds;
			} 
			catch (Exception) 
			{
				return null;
			}
		}
	}
}
