/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 01.11.2021
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit Drawing
	/// </summary>
	public class Color
	{
		public static readonly Random rand = new Random();

		/// <summary>
		/// generiert eine zufällige Farbe
		/// </summary>
		/// <returns>Farbe im RGB Format als String</returns>
		public string GetRandomColour()
		{
			try 
			{
				return System.Drawing.Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)).ToString() ;
  			} 
			catch (Exception) 
			{
				throw;
			}
			
		}
	}
}
