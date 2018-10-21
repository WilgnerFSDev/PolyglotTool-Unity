/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using System;

namespace Polyglot
{
	[System.Serializable]
	public class Translation
	{
		public int indexLanguage;
		public string nameID;
		public string translation;
		public int idUniqueElements;
		public Categories categories;


		public Translation(int indexLanguage, string nameID, string translation, int idUniqueElements, Categories categories)
		{
			this.indexLanguage = indexLanguage;
			this.nameID = nameID;
			this.translation = translation;
			this.categories = categories;
			this.idUniqueElements = idUniqueElements;
		}
	}
}