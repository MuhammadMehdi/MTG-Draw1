using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Draw.Model
{
    public enum ItemName
    {
        Ackermann74188A1,
        Ascom_Schwesternruf,
        Ascom_Diagnostikruf,

        Block,

        Deckelschnitt,

        dräger_gasanschluss_sauerstoff,
        dräger_gasanschluss_luft,
        dräger_gasanschluss_vakuum,
        dräger_gasanschluss_lachgas,
        dräger_gasanschluss_kohlenstoffdioxid,

        Einspeisung_4z_seitlich_li,
        Einspeisung_3z_seitlich_li,
        Einspeisung_2z_seitlich_li,
        Einspeisung_1z_seitlich_li,
        Einspeisung_4z_seitlich_re,
        Einspeisung_3z_seitlich_re,
        Einspeisung_2z_seitlich_re,
        Einspeisung_1z_seitlich_re,
        Einspeisung_4z_hinten_li,
        Einspeisung_3z_hinten_li,
        Einspeisung_2z_hinten_li,
        Einspeisung_1z_hinten_li,
        Einspeisung_4z_hinten_re,
        Einspeisung_3z_hinten_re,
        Einspeisung_2z_hinten_re,
        Einspeisung_1z_hinten_re,

        Einspeisekanal_1z,
        Einspeisekanal_2z,

        Endplatte_4z_li,
        Endplatte_3z_li,
        Endplatte_2z_li,
        Endplatte_1z_li,
        Endplatte_4z_re,
        Endplatte_3z_re,
        Endplatte_2z_re,
        Endplatte_1z_re,

        Fertigdecke,

        GS400,
        GS500,

        greggersen_gasanschluss_sauerstoff,
        greggersen_gasanschluss_luft,
        greggersen_gasanschluss_vakuum,
        greggersen_gasanschluss_lachgas,
        greggersen_gasanschluss_kohlenstoffdioxid,

        leerdose_einfach,
        leerdose_einfach_abdeckung,
        leerdose_doppelt,
        leerdose_doppelt_abdeckung,

        Lumos,
        Leerrohr_zur_Leuchte,
        Orientierungslicht,

        peha_steckdose_grün_kontrollleuchte_beschriftungsfeld,
        peha_steckdose_orange_kontrollleuchte_beschriftungsfeld,
        peha_steckdose_weiss_kontrollleuchte_beschriftungsfeld,
        peha_steckdose_grün_kontrollleuchte,
        peha_steckdose_orange_kontrollleuchte,
        peha_steckdose_weiss_kontrollleuchte,
        peha_steckdose_grün,
        peha_steckdose_weiss,
        peha_steckdose_orange,
        peha_doppelschalter_weiss_beschriftungsfeld,
        peha_doppelschalter_weiss,
        peha_schalter_weiss_beschriftungsfeld,
        peha_schalter_weiss,
        peha_rj45_beschriftungsfeld,
        peha_rj45,
        peha_leerfeld_weiss_beschriftungsfeld,
        peha_leerfeld_weiss,

        potentialausgleich_einfach,
        potentialausgleich_doppelt,
        
        Teilung,
        Test,

        undefiniert,

        Wand_Links,
        Wand_Rechts,

        Kantenschutz
    }

    public enum BlockName
    {
        Schiene,
        Block1,
        Block2,
        Block3,
        Block4,
        Block5,
        Block6,
        Block7,
        Block8,
        Block9,
        Block10
    }
        



    public class ItemTyp
    {
        #region Variablen

        private string itemName;

        private double xOffset;

        private double yOffset;

        private double faktor;

        private double abstand;

        private string dxfBlock;

        private string layer;

        private string textLegende;

        #endregion

        public string ItemName { get => itemName; set => itemName = value; }

        public double Xoffset { get => xOffset; set => xOffset= value; }

        public double Yoffset { get => yOffset; set => yOffset = value; }

        public double Faktor { get => faktor; set => faktor = value; }

        public double Abstand { get => abstand; set => abstand = value; }

        public string DxfBlock { get => dxfBlock; set => dxfBlock = value; }

        public string Layer { get => layer; set => layer = value; }

        public string TextLegende { get => textLegende; set => textLegende = value; }

        #region Eigenschaften


        #endregion
    }



    [Serializable]

    public class Itemlist
    {
        protected static int ID = 10;
    }




    public class Item : Itemlist 
    {
        #region Variablen

        private int id = Itemlist.ID+=10 ;

        private int einbauebeneID;

        private ItemName typ;

        private double pos;        

        private double ypos = 0;

        private double laenge = 1;

        private double rotation;   // Drehung in Grad

        private BlockName block;

        private string kreis;

        private int anzahl = 1;

        private string teilenummer;

        private List<Ansicht> listeItemAnsichten = new List<Ansicht>();


        #endregion


        #region Konstruktor

        #endregion


        #region Eigenschaften

        public new int ID { get => id; set => id = value; }

        public int EinbauebeneID { get => einbauebeneID; set => einbauebeneID = value; }

        public ItemName Typ { get => typ; set => typ = value; }

        public double Pos { get => pos; set => pos = value; }

        public double Ypos { get => ypos; set => ypos = value; }

        public double Laenge { get => laenge; set => laenge = value; }

        public double Rotation { get => rotation; set => rotation = value; }

        public string Kreis { get => kreis; set => kreis = value; }

        public BlockName Block { get => block; set => block = value; }

        public int Anzahl { get => anzahl; set => anzahl = value; }

        public string Teilenummer { get => teilenummer; set => teilenummer = value; }

        public List<Ansicht> ListeItemAnsichten { get => listeItemAnsichten; set => listeItemAnsichten = value; }

        #endregion

        #region Methoden

        public Item Clone()
        {
            Item clone = (Item)this.MemberwiseClone();
            return clone;
        }

        #endregion



    }
}
