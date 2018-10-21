/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using System;

namespace Polyglot
{
    [System.Serializable]
    public class IdElements
    {
        public bool inUse;
        public int id;


        public IdElements(bool inUse, int id)
        {
            this.inUse = inUse;
            this.id = id;
        }
    }
}