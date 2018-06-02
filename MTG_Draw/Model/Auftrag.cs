using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    public class Auftrag
    {
        #region Variablen

        private int id;

        private List<Position> listePositionen = new List<Position>();

        private List<Zeichnung> listeZeichnungen = new List<Zeichnung>();

        #endregion


        #region Konstruktor

        public int ID { get => id; set => id = value; }

        internal List<Zeichnung> ListeZeichnungen { get => listeZeichnungen; set => listeZeichnungen = value; }

        internal List<Position> ListePositionen { get => listePositionen; set => listePositionen = value; }

        #endregion


        #region Eigenschaften

        #endregion


        #region Unterklassen
        public class Position
        {
            #region Variablen 

            private string name;

            private int anzahl;

            private Produkt produkt;

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            public string Name { get => name; set => name = value; }

            public int Anzahl { get => anzahl; set => anzahl = value; }

            public Produkt Produkt { get => produkt; set => produkt = value; }

            #endregion
        }
        #endregion
    }
}
