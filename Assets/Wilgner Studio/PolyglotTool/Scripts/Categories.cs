/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using System;

namespace Polyglot
{
	[System.Serializable]
	public class Categories
	{
		public int index;
		public string name;

		public Categories(int index, string name)
		{
			this.index = index;
			this.name = name;
		}

		public override string ToString ()
		{
			return string.Format ("index: {0} | name: {1}", this.index, this.name);
		}
	}
}

