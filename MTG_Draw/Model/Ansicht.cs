using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    public enum Darstellung {oben, unten, links, rechts, vorne, hinten}

    public enum AnsichtTyp {statisch, variabel}

    [Serializable]
    public class Ansicht
    {
        #region Variablen

        private AnsichtTyp typ;

        private string bildPfad;

        private Darstellung darstellung;

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften
    
        public AnsichtTyp Typ { get => typ; set => typ = value; }
        public string BildPfad { get => bildPfad; set => bildPfad = value; }
        public Darstellung Darstellung { get => darstellung; set => darstellung = value; }

        #endregion
    }
 }
