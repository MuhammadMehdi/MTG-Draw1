using MTG_Draw.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using netDxf.Units;
using System.Data;
using System.Xml;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Markup;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Linq;
using System.ComponentModel;



namespace MTG_Draw
{

    public partial class MainWindow : Window
    {
        #region Variablen
        public static Produkt.HorizontalCura Testschiene = new Produkt.HorizontalCura();
        Produkt ActualProduct = new Produkt();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        DataSet XmlDataSet = new DataSet();
        string XmlFileName;
        string DxfFileName;
        double zoomfactor = 0.05;
        double trans_x = 0;
        double trans_y = 0;

        private System.Windows.Point _pointOnClick; // Click Position for panning
        private ScaleTransform _scaleTransform;
        private TranslateTransform _translateTransform;
        private TransformGroup _transformGroup;







        public class Settings
        {
            private string lastpath = @"C:\users\%UserName%\desktop";

            public string Lastpath { get => lastpath; set => lastpath = value; }
        }

        #endregion

        #region Konstruktor
        public MainWindow()
        {
            InitializeComponent();
            Settings set = new Settings();
            set = ReadConfig();


            _translateTransform = new TranslateTransform();
            _scaleTransform = new ScaleTransform();
            _transformGroup = new TransformGroup();

            fillData();



        }

        #endregion

        #region Menu

        private void MenuItemOeffnen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings set = new Settings();
                set = ReadConfig();
                string pfad = set.Lastpath.ToString();
                openFileDialog.InitialDirectory = pfad;
                openFileDialog.Filter = "XML Files|*.XML";

                if (openFileDialog.ShowDialog() == true)
                {
                    XmlFileName = (openFileDialog.FileName);
                    set.Lastpath = XmlFileName;
                    WriteConfig(set);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Produkt));
                FileStream file = new FileStream(XmlFileName, FileMode.Open);
                //Die Deserialize()-Methode gibt ein Object zurück. => casten!
                Testschiene = serializer.Deserialize(file) as Produkt.HorizontalCura;
                file.Close();




                this.DataContext = Testschiene;
                dgItems.Items.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));

                Testschiene.Draw(Canvas1, /*ScrollViewer1,*/ Testschiene, 1);
                //dgItems.ItemsSource = null;
                //dgItems.ItemsSource = Testschiene.ListeItems;
                //dgBlockItems.ItemsSource = null;
                //dgBlockItems.ItemsSource = Testschiene.ListeBlockItems;
                RenderCanvas();

            }
            catch
            {
                MessageBox.Show("Datei nicht gefundenoder fehlerhaft");
            }

            finally
            {
                //MessageBox.Show("Fehler bei Einlesen der Datei");
            }
        }

        private void MenuItemSpeichern_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = Testschiene;
            saveFileDialog.InitialDirectory = ReadSetting("Pfad");
            saveFileDialog.Filter = "XML Files|*.XML";

            string filename = "Auftrag";
            if (Testschiene.Auftrag != null) filename = filename + "-" + Testschiene.Auftrag; else MessageBox.Show("Bitte Auftragsnummer eingeben");
            if (Testschiene.Position != null) filename = filename + "-Pos." + Testschiene.Position; else MessageBox.Show("Bitte Position eingeben");
            if (Testschiene.Position != null) filename = filename + "-Pos." + Testschiene.BezeichnungAuftrag; else MessageBox.Show("Bitte Bezeichnung Auftrag eingeben");
            if (Testschiene.Version != null) filename = filename + "-" + Testschiene.Version; else MessageBox.Show("Bitte Version eingeben");
            if (Testschiene.Datum != null) filename = filename + "-" + Testschiene.Datum.ToString(); else MessageBox.Show("Bitte Datum eingeben");

            saveFileDialog.FileName = filename;

            if (saveFileDialog.ShowDialog() == true)
            {
                XmlFileName = (saveFileDialog.FileName);

                string directoryPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                //MessageBox.Show(directoryPath);
                AddUpdateAppSettings("Pfad", directoryPath);


                Testschiene.WriteXML(Testschiene, XmlFileName);
            }



        }

        private void MenuItemSchliessen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

// I used this for fill data into MTG printable document

        private void fillData()
        {
           
            XmlDocument docXml = new XmlDocument();
            docXml.Load(@"C:\Users\Malik Mehdi\Desktop\MTG-Draw\MTG-Draw\MTG_Draw\Auftrag-Auftrag18363-Pos.1-Pos.Saale-MED-Asklepios Langen E107 Ruheraum Vorbereitung-V1-21.05.20.xml");
            XmlNodeList lstProduct;

            // here i am getting all file
            lstProduct = docXml.GetElementsByTagName("ID");
            //MessageBox.Show(lstProduct.Item(0).InnerText.ToString());

            headerTxtBox1.Text = "MTG Gmbh Senkrecht 8-10 00280 Sutzac \nTel :23594-3245 \nwww.mtg-gmbh.de";

            lstProduct = docXml.GetElementsByTagName("Auftrag");
            headerTxtBox2.Text +=lstProduct.Item(0).InnerText;
            lstProduct = docXml.GetElementsByTagName("BezeichnungAuftrag");
            headerTxtBox2.Text += " " + lstProduct.Item(0).InnerText;

            lstProduct = docXml.GetElementsByTagName("Position");
            headerTxtBox2.Text += "\nPosition: " + lstProduct.Item(0).InnerText;

            lstProduct = docXml.GetElementsByTagName("Anzahl");
            headerTxtBox2.Text += " " + lstProduct.Item(0).InnerText;

            lstProduct = docXml.GetElementsByTagName("BezeichnungPosition");
            headerTxtBox2.Text += " Stuck " + lstProduct.Item(0).InnerText;

            lstProduct = docXml.GetElementsByTagName("Zeichner");
            headerTxtBox2.Text += "\ngezeichnet:  " + lstProduct.Item(0).InnerText;

            lstProduct = docXml.GetElementsByTagName("Datum");

            headerTxtBox2.Text += " Datum:  " + lstProduct.Item(0).InnerText;

            // here we will add item into grid with relative  positions
            // all operation will be perform


            // fotter 
            lstProduct = docXml.GetElementsByTagName("Bemerkungstext");
            bemerTxtBox.Text =  lstProduct.Item(0).InnerText;

        }

        private void SerializeToXml(Produkt produkt)
        {
            //Erstelle einen XML-Serialisierer für Objekte vom Typ MVE
            XmlSerializer serializer = new XmlSerializer(typeof(Produkt));

            //Erstelle einen FileStream auf die Datei, in die unserer
            //MVE-Objekt in XML-Form gespeichert werden soll.
            FileStream file = new FileStream(Environment.CurrentDirectory + "\\produkt.xml", FileMode.Create);
            //Serialisiere das übergebene MVE-Objekt (blogObj)
            //und schreibe es in den FileStream.
            serializer.Serialize(file, produkt);

            //Schließe die XML-Datei.
            file.Close();
        }

        private Produkt DeserializeToObject()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Produkt));
            FileStream file = new FileStream(Environment.CurrentDirectory + "\\produkt.xml", FileMode.Open);
            //Die Deserialize()-Methode gibt ein Object zurück. => casten!
            Produkt produkt = serializer.Deserialize(file) as Produkt;
            file.Close();
            return produkt;
        }

        private void Anzeige_Click(object sender, RoutedEventArgs e)
        {
            Testschiene.Draw(Canvas1, /*ScrollViewer1,*/ Testschiene, 1);
            dgItems.ItemsSource = null;
            dgItems.ItemsSource = Testschiene.ListeItems;
            dgBlockItems.ItemsSource = null;
            dgBlockItems.ItemsSource = Testschiene.ListeBlockItems;
            RenderCanvas();
        }

        private void DXF_Ausgabe_Click(object sender, RoutedEventArgs e)
        {
            saveFileDialog.InitialDirectory = ReadSetting("Pfad");
            saveFileDialog.Filter = "DXF Files|*.DXF";

            string filename = "Auftrag";
            if (Testschiene.Auftrag != null) filename = filename + "-" + Testschiene.Auftrag; else MessageBox.Show("Bitte Auftragsnummer eingeben");
            if (Testschiene.Position != null) filename = filename + "-Pos." + Testschiene.Position; else MessageBox.Show("Bitte Position eingeben");
            if (Testschiene.Position != null) filename = filename + "-Pos." + Testschiene.BezeichnungAuftrag; else MessageBox.Show("Bitte Bezeichnung Auftrag eingeben");
            if (Testschiene.Version != null) filename = filename + "-" + Testschiene.Version; else MessageBox.Show("Bitte Version eingeben");
            if (Testschiene.Datum != null) filename = filename + "-" + Testschiene.Datum.ToString(); else MessageBox.Show("Bitte Datum eingeben");

            saveFileDialog.FileName = filename;

            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    DxfFileName = (saveFileDialog.FileName);
                    string directoryPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                    AddUpdateAppSettings("Pfad", directoryPath);
                }

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fehler beim Dateiöffnen");
            }

            Testschiene.DxfOut(DxfFileName, Testschiene);
        }




        private void Print_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog prnt = new PrintDialog();


            if (prnt.ShowDialog() == true)
            {

                //Size pageSize = new Size(prnt.PrintableAreaWidth, prnt.PrintableAreaHeight);
                //Canvas1.Measure(pageSize);
                //Canvas1.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));



                if (prnt.ShowDialog() == true)
                {
                //    prnt.PrintVisual(Canvas1, "Printing Canvas");
                //}


                //Size pageSize = new Size(prnt.PrintableAreaWidth, prnt.PrintableAreaHeight);

                //Canvas1.Measure(pageSize);
                //Canvas1.Arrange(new Rect(1, 1, pageSize.Width, pageSize.Height));

                //store original scale
                //Transform originalScale = Canvas1.LayoutTransform;



                //get scale of the print wrt to screen of WPF visual
                //double scale = Math.Min(prnt.PrintableAreaWidth / Canvas1.ActualWidth, prnt.PrintableAreaHeight / Canvas1.ActualHeight);

                    //Console.WriteLine("Scale:" + scale + "    prnt.PrintableAreaWidth: " + prnt.PrintableAreaWidth + "    prnt.PrintableAreaHeight: " + prnt.PrintableAreaHeight);

                //Transform the Visual to scale
                //Canvas1.LayoutTransform = new ScaleTransform(scale,scale);

                prnt.PrintVisual(Canvas1, "Printing Canvas");

                //apply the original transform.
                //Canvas1.LayoutTransform = originalScale;
                }
            }
        }

        private void DXF_Werkstattzeichnung_Click(object sender, RoutedEventArgs e)
        {
            //saveFileDialog.InitialDirectory = "C:\\Users\\sb\\Source\\Repos\\MTG-Draw\\MTG-Draw\\MTG_Draw";
            saveFileDialog.InitialDirectory = ReadSetting("Pfad");
            saveFileDialog.DefaultExt = "dxf";
            //saveFileDialog.FileName = "Testprodukt_Werkstattzeichnung.dxf";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                    DxfFileName = (saveFileDialog.FileName);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fehler beim Dateiöffnen");
            }

            Testschiene.DxfOutWerkstatt(DxfFileName, Testschiene);
        }

        private void Bloecke_aktualisieren_Click(object sender, RoutedEventArgs e)
        {
            this.dgItems.ItemsSource = null;
            Testschiene.Bloecke_aktualisieren(Testschiene);
            this.dgItems.ItemsSource = Testschiene.ListeItems;

            Testschiene.Draw(Canvas1, /*ScrollViewer1,*/ Testschiene, 1);
            RenderCanvas();
        }

        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Capture Mouse
            //Canvas1.CaptureMouse();
            //Store click position relation to Parent of the canvas
            _pointOnClick = e.GetPosition((FrameworkElement)Canvas1.Parent);
        }

        private void Canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pointOnMove = e.GetPosition((FrameworkElement)Canvas1.Parent);
            trans_x = trans_x - (_pointOnClick.X - pointOnMove.X);
            trans_y = trans_y - (_pointOnClick.Y - pointOnMove.Y);

            RenderCanvas();
 
            Mouse.OverrideCursor = null;
        }

        private void MouseWheelHandler(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoomfactor *= 1.05;
            }

            // If the mouse wheel delta is negative, move the box down.
            if (e.Delta < 0)
            {
                zoomfactor *= 0.95;
            }
            RenderCanvas();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            trans_y -= 10;
            RenderCanvas();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            trans_y += 10;
            RenderCanvas();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            trans_x += 10;
            RenderCanvas();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            trans_x -= 10;
            RenderCanvas();
        }
        private void Zoom_In_Click(object sender, RoutedEventArgs e)
        {
            zoomfactor *= 1.1;
            RenderCanvas();
        }

        private void Zoom_Out_Click(object sender, RoutedEventArgs e)
        {
            zoomfactor *= 0.9;
            RenderCanvas();
        }

        private void RenderCanvas()
        {
            var transformGroup = new TransformGroup();
            var scale = new ScaleTransform(zoomfactor, zoomfactor);
            var translate = new TranslateTransform(trans_x, trans_y);
            transformGroup.Children.Add(scale);
            transformGroup.Children.Add(translate);
            Canvas1.RenderTransformOrigin = new System.Windows.Point(0, 0);
            Canvas1.RenderTransform = transformGroup;
            Canvas1.UpdateLayout();
        }

        private void Deckelschnitte_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.Show();
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error reading app settings");
                return null;
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error writing app settings");
            }
        }


        private void WriteConfig(Settings set)
        {
            //try
            //{
                string XmlFileName = ReadSetting("ConfigPfad");

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                FileStream file = new FileStream(XmlFileName, FileMode.Create);
                //Die Deserialize()-Methode gibt ein Object zurück. => casten!


                serializer.Serialize(file,set);
                file.Close();
            //}
            //catch
            //{
            //    MessageBox.Show("Fehler beim Schreiben der Config-Datei");
            //}
        }

        private Settings ReadConfig()
        {
            //try
            //{
                string XmlFileName = ReadSetting("ConfigPfad");

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                FileStream file = new FileStream(XmlFileName, FileMode.Open);
                //Die Deserialize()-Methode gibt ein Object zurück. => casten!
       
                Settings set1 = serializer.Deserialize(file) as Settings;
                file.Close();
                return set1;




            //}
            //catch
            //{
            //    MessageBox.Show("Config-Datei  nicht gefundenoder fehlerhaft");
            //    return null;
            //}
        }
    }
}
