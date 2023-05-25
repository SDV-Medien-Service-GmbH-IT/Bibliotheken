/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 01.11.2021
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit Datenkonvertierungen
	/// </summary>
	public class Converter
	{
		/// <summary>
		/// erstellt aus einer Tabelle einen JSON String
		/// </summary>
		/// <param name="table">Tabelle mit Daten welche in einen JSON String umgewandelt werden sollen</param>
		/// <returns>JSON String</returns>
		public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
			try 
			{
		        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
	            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
	            Dictionary<string, object> childRow;
	            foreach (DataRow row in table.Rows)
	            {
	                childRow = new Dictionary<string, object>();
	                foreach (DataColumn col in table.Columns)
	                {
	                    childRow.Add(col.ColumnName, row[col]);
	                }
	                parentRow.Add(childRow);
	            }
	           return jsSerializer.Serialize(parentRow);
  			} 
			catch (Exception ex) 
			{
				return(ex.Message + " \n " + ex.InnerException);
			}
      	}
	}
}
