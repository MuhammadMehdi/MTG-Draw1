using MTG_Draw.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace MTG_Draw.Model
{
    public partial class Produkt
    {

        #region Variablen

        private int id;

        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften


        public int ID { get => id; set => id = value; }

        #endregion


        #region Unterklassen
        
        public class VertikalCura : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion

        }

        public class HorizontalLigno : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion
        }

        public class VertikalLigno : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion
        }

        public class Lumos : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion
        }

        public class GS2 : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion
        }

        public class MS : Produkt
        {
            #region Variablen

            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            #endregion
        }

        #endregion

        #region Methoden

        public void WriteXML(Produkt ActualProduct, string xmlFilename)
        {
            if (xmlFilename == null)
            {
                throw new ArgumentNullException(nameof(xmlFilename));
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Produkt));
                FileStream fileout = new FileStream(xmlFilename, FileMode.Create);
                serializer.Serialize(fileout, ActualProduct);
                fileout.Close();
            }
            catch
            {
                MessageBox.Show("Fehler beim Speichern der XML - Datei");
            }



        }




        #endregion





    }
}
