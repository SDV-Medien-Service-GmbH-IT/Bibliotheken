/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 04.11.2021
 * Time: 07:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Management;
using System.Security.Principal;

namespace Bibliotheken
{
	/// <summary>
	/// Description of Windows.
	/// </summary>
	public class Windows
	{
		
		/// <summary>
		/// Starte einen Windows Dienst neu
		/// </summary>
		/// <param name="ServiceName">Dienstname</param>
		/// <param name="Server">Server auf welchem der Dienst läuft</param>
		/// <returns>True oder False</returns>
		public bool StartService(string ServiceName, string Server)//, string User, string Password)
		{
			try 
			{
				ConnectionOptions connection = new ConnectionOptions();
		        //connection.Username = User;
		        //connection.Password = Password;
			    //connection.EnablePrivileges = true;
	            //connection.Authentication = AuthenticationLevel.Default;
	            //connection.Impersonation = ImpersonationLevel.Impersonate;
	         	ManagementScope scope = new ManagementScope("\\\\"+Server+"\\root\\CIMV2", connection);
	            scope.Connect();
	            ObjectQuery query= new ObjectQuery("SELECT * FROM Win32_Service where name = '" + ServiceName + "'"); 
	            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
	           foreach (ManagementObject queryObj in searcher.Get())
	            {	
	                if (queryObj["State"].Equals("Stopped")) 
	                {
	                    queryObj.InvokeMethod("StartService", null);
	                    return true;
	                }
	                else
	                {
						return false;
	                }
			    }
				return false;
			}
			catch (Exception) 
			{
				return false;
				throw;
			}
			
		}
		
		/// <summary>
		/// Stoppt einen Windows Dienst neu
		/// </summary>
		/// <param name="ServiceName">Dienstname</param>
		/// <param name="Server">Server auf welchem der Dienst läuft</param>
		/// <returns>True oder False</returns>
		public bool StopService(string ServiceName, string Server)//, string User, string Password)
		{
			try 
			{
				ConnectionOptions connection = new ConnectionOptions();
		        //connection.Username = User;
		        //connection.Password = Password;
			    //connection.EnablePrivileges = true;
	            //connection.Authentication = AuthenticationLevel.Default;
	            //connection.Impersonation = ImpersonationLevel.Impersonate;
	         	ManagementScope scope = new ManagementScope("\\\\"+Server+"\\root\\CIMV2", connection);
	            scope.Connect();
	            ObjectQuery query= new ObjectQuery("SELECT * FROM Win32_Service where name = '" + ServiceName + "'"); 
	            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
	           foreach (ManagementObject queryObj in searcher.Get())
	            {	
	                if (queryObj["State"].Equals("Running")) 
	                {
	                    queryObj.InvokeMethod("StopService", null);
	                    return true;
	                }
	                else
	                {
						return false;
	                }
			    }
				return false;
			}
			catch (Exception) 
			{
				return false;
				throw;
			}
			
		}

		/// <summary>
		/// Neustart eines Windows Dienst
		/// </summary>
		/// <param name="ServiceName">Dienstname</param>
		/// <param name="Server">Server auf welchem der Dienst läuft</param>
		/// <returns>True oder False</returns>
		public bool RestartService(string ServiceName, string Server)//, string User, string Password)
		{
			try 
			{
				ConnectionOptions connection = new ConnectionOptions();
		        //connection.Username = User;
		        //connection.Password = Password;
			    //connection.EnablePrivileges = true;
	            //connection.Authentication = AuthenticationLevel.Default;
	            //connection.Impersonation = ImpersonationLevel.Impersonate;
	         	ManagementScope scope = new ManagementScope("\\\\"+Server+"\\root\\CIMV2", connection);
	            scope.Connect();
	            ObjectQuery query= new ObjectQuery("SELECT * FROM Win32_Service where name = '" + ServiceName + "'"); 
	            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
	           foreach (ManagementObject queryObj in searcher.Get())
	            {	
	                if (queryObj["State"].Equals("Running")) 
	                {
	                    queryObj.InvokeMethod("StopService", null);
	                    queryObj.InvokeMethod("StartService", null);  
	                    return true;
	                }
	                else
	                {
						return false;
	                }
			    }
				return false;
			}
			catch (Exception) 
			{
				return false;
				throw;
			}
			
		}		
		
		/// <summary>
		/// Holt alle Dienste einen spezifischen Servers 
		/// </summary>
		/// <param name="Server">Server auf welchem der Dienst läuft</param>
		/// <param name="ds">leeres Dataset</param>
		/// <returns>Liste an Diensten welche auf dem Abgefragten sErver vorhanden sind</returns>
		public DataSet getServices(string Server,DataSet ds)//, string User, string Password, DataSet ds)
		{
			try 
			{
				DataTable dt =new DataTable();
				dt.Columns.Add("ServicesName");
				dt.Columns.Add("DisplayName");
				dt.Columns.Add("ServicesDescription");
				dt.Columns.Add("ServicesStart");
				dt.Columns.Add("ServicesStatus");
				ds.Tables.Add(dt);
				ConnectionOptions connection = new ConnectionOptions();
		        //connection.Username = User;
		        //connection.Password = Password;
			    //connection.EnablePrivileges = true;
                //connection.Authentication = AuthenticationLevel.Default;
                //connection.Impersonation = ImpersonationLevel.Impersonate;
             	ManagementScope scope = new ManagementScope("\\\\"+Server+"\\root\\CIMV2", connection);
	            scope.Connect();
	            ObjectQuery query= new ObjectQuery("SELECT * FROM Win32_Service"); 
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
               foreach (ManagementObject queryObj in searcher.Get())
                {	            
           			dt.Rows.Add(queryObj["Name"],queryObj["DisplayName"],queryObj["Description"],queryObj["StartMode"],queryObj["State"]);
			    }
			    return ds;
			} 
			catch (Exception) 
			{
				return null;
			}
		}
		
		/// <summary>
		/// Prüft ob der Benutzer ein Administrator ist
		/// </summary>
		/// <returns>True oder False</returns>
		public static bool IsAdministrator()
		{
			try 
			{
			    using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
			    {
			        WindowsPrincipal principal = new WindowsPrincipal(identity);
			        return principal.IsInRole(WindowsBuiltInRole.Administrator);
			    }
			} 
			catch (Exception) 
			{
				return false;
			}
		}
	}
}
