using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    public class ZeichnungAnsicht
    {
        #region Variablen

            private int id;

            private int produktID;

            private int darstellung;

            double xPos;

            double yPos;

            int massstab;

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften

        public int ProduktID { get => produktID; set => produktID = value; }

        public int Darstellung { get => darstellung; set => darstellung = value; }

        public int ID { get => id; set => id = value; }

        public double XPos { get => xPos; set => xPos = value; }

        public double YPos { get => yPos; set => yPos = value; }

        public int Massstab { get => massstab; set => massstab = value; }

        #endregion
    }
}
