using MTG_Draw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    public class Zeichnung
    {
        #region Variablen

        private int id;

        private string format;

        private List<ZeichnungAnsicht> listeAnsicht = new List<ZeichnungAnsicht>();

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften

        public int ID { get => id; set => id = value; }

        public string Format { get => format; set => format = value; }

        internal List<ZeichnungAnsicht> ListeAnsicht { get => listeAnsicht; set => listeAnsicht = value; }

        #endregion


    }
}
