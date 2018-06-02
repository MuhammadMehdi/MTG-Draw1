using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    [Serializable]
    public class Profil
    {
        #region Variablen

        private int id;

        private string farbe;

        private List<Rechteck> listeRechtecke = new List<Rechteck>();

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften

        public int ID { get => id; set => id = value; }

        public string Farbe { get => farbe; set => farbe = value; }

        public List<Rechteck> ListeRechtecke { get => listeRechtecke; set => listeRechtecke = value; }

        #endregion

        #region Unterklassen
        [Serializable]
        public class Rechteck
        {
            #region Variablen

            private double k1;

            private double k2;

            private Darstellung darstellung;

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            public double K1 { get => k1; set => k1 = value; }

            public double K2 { get => k2; set => k2 = value; }

            public Darstellung Darstellung { get => darstellung; set => darstellung = value; }

            #endregion
        }
        #endregion
    }
}
