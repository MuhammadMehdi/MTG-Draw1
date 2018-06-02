using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    [Serializable]
    public class Einbauebene
    {
        #region Variablen

        private int id;

        private double x;

        private double y;

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften

        public int ID { get => id; set => id = value; }

        public double X { get => x; set => x = value; }

        public double Y { get => y; set => y = value; }

        #endregion
    }
}
