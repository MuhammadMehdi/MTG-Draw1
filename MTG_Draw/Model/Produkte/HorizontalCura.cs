using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Windows;
using System.IO;
using System.Windows.Markup;
using System.Data.SqlClient;
using netDxf;
using netDxf.Entities;
using netDxf.Units;
using System.Linq;
using netDxf.Tables;
using netDxf.Blocks;
using System.Drawing;

namespace MTG_Draw.Model
{
    public enum ProfilName
    {
        IV2_3z_2GS, IV2_3z_GSo, IV2_3z_GSu, IV2_3z,
        IV2_2z_2GS, IV2_2z_GSo, IV2_2z_GSu, IV2_2z,
        IV2_1z_2GS, IV2_1z_GSo, IV2_1z_GSu, IV2_1z,
        IV2_4z_2GS
    }

    [Serializable, XmlInclude(typeof(HorizontalCura))]
    public partial class Produkt
    {
        #region Unterklassen

        [Serializable]
        public class HorizontalCura : Produkt
        {
            #region Variablen

            private string auftrag="";

            private string bezeichnungAuftrag = "";

            private string position = "";

            private string bezeichnungPosition = "";

            private string bemerkungstext = "";

            private int anzahl = 1;

            private double laenge;

            private double montageHoehe;

            private string version;

            private string zeichner;

            private string datum;

            //private double wandabstandLinks;

            //private double wandabstandRechts;

            private ProfilName profilTyp;

            private List<Profil> listeProfile = new List<Profil>();

            private List<Ansicht> listeCuraAnsichten = new List<Ansicht>();

            private List<Einbauebene> listeEinbauebenen = new List<Einbauebene>();

            private List<Item> listeItems = new List<Item>();

            private List<Item> listeBlockItems = new List<Item>();


            #endregion


            #region Konstruktor

            #endregion


            #region Eigenschaften

            public string Auftrag { get => auftrag; set => auftrag = value; }

            public string BezeichnungAuftrag { get => bezeichnungAuftrag; set => bezeichnungAuftrag = value; }

            public string Position { get => position; set => position = value; }

            public int Anzahl { get => anzahl; set => anzahl = value; }

            public string BezeichnungPosition { get => bezeichnungPosition; set => bezeichnungPosition = value; }

            public double Laenge { get => laenge; set => laenge = value; }

            public double MontageHoehe { get => montageHoehe; set => montageHoehe = value; }

            //public double WandabstandLinks { get => wandabstandLinks; set => wandabstandLinks = value; }

            //public double WandabstandRechts { get => wandabstandRechts; set => wandabstandRechts = value; }

            public ProfilName ProfilTyp { get => profilTyp; set => profilTyp = value; }

            public List<Profil> ListeProfile { get => listeProfile; set => listeProfile = value; }

            public List<Ansicht> ListeCuraAnsichten { get => listeCuraAnsichten; set => listeCuraAnsichten = value; }

            public List<Einbauebene> ListeEinbauebenen { get => listeEinbauebenen; set => listeEinbauebenen = value; }

            public List<Item> ListeItems { get => listeItems; set => listeItems = value; }

            public List<Item> ListeBlockItems { get => listeBlockItems; set => listeBlockItems = value; }

            public string Version { get => version; set => version = value; }

            public string Zeichner { get => zeichner; set => zeichner = value; }

            public string Datum { get => datum; set => datum = value; }

            public string Bemerkungstext { get => bemerkungstext; set => bemerkungstext = value; }

            #endregion


            #region Methoden

            public void DeckelschnitteErzeugen(double xpos)
            {
                foreach (Einbauebene myEinbauebene in listeEinbauebenen) if (myEinbauebene.ID != 0)
                {
                    Item myItem = new Item();
                    myItem.Typ = ItemName.Deckelschnitt;
                    myItem.EinbauebeneID = myEinbauebene.ID;
                    myItem.Pos = xpos;
                    myItem.Block = BlockName.Schiene;

                    ListeItems.Add(myItem);

                }
            }


            //public void Draw(Canvas myCanvas, /*ScrollViewer myScrollViewer,*/ HorizontalCura myProduct, double zoomfactor)
            //{
            //    double np_x = 200;   // 200
            //    double np_y = 2200;   // 600
            //    //double zoomfactor = 1;

            //    //if (laenge > 1600) zoomfactor = 1600 / Laenge;
            //    double strokethickness = 0.8 * zoomfactor;

            //    myCanvas.Children.Clear();

            //    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));

            //    double cw = (Laenge + 1000) /** zoomfactor*/;
            //    double CanvasWidth;

            //    if (cw > 7800) CanvasWidth = cw;
            //    else CanvasWidth = 7800 ;

            //    double CanvasHeight = 5500;

            //    myCanvas.Height = CanvasHeight;
            //    myCanvas.Width = CanvasWidth;



            //    double aw = myCanvas.ActualWidth;
            //    double ah = myCanvas.ActualHeight;
            //    //Rand
            //    Rectangle rect = new System.Windows.Shapes.Rectangle()
            //    {
            //        Stroke = new SolidColorBrush(Colors.Black),
            //        StrokeThickness = 2,
            //        Fill = brush,
            //        Width = CanvasWidth,
            //        Height = CanvasHeight
            //    };
            //    Canvas.SetLeft(rect, 0);
            //    Canvas.SetTop(rect, 0);
            //    myCanvas.Children.Add(rect);

            //    //Schrift
            //    double logoPosX = 10;
            //    double logoPosY = 10;

            //    TextBlock textBlock = new TextBlock();
            //    textBlock.Text = myProduct.Auftrag + "  "
            //                    + myProduct.BezeichnungAuftrag.ToString() + "\r"
            //                    + "Position:" + myProduct.Position.ToString() + "  "
            //                    + myProduct.Anzahl.ToString() + " Stück     "
            //                    + "  gezeichnet:   " + myProduct.Zeichner
            //                    + "  Datum:    " + myProduct.Datum;
            //    textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            //    textBlock.FontFamily = new FontFamily("Arial");
            //    textBlock.FontSize = 100;
            //    textBlock.TextWrapping = TextWrapping.Wrap;
            //    //textBlock.FontStretch = FontStretches.UltraExpanded;
            //    //textBlock.FontStyle = FontStyles.Italic;
            //    //textBlock.FontWeight = FontWeights.UltraBold;
            //    textBlock.Width = 2000;
            //    textBlock.Height = 600;
            //    Canvas.SetLeft(textBlock, 10);
            //    Canvas.SetTop(textBlock, 10);
            //    myCanvas.Children.Add(textBlock);

            //    TextBlock textBlock1 = new TextBlock();
            //    textBlock1.Text = myProduct.Bemerkungstext;
            //    textBlock1.Width = 2000;
            //    textBlock1.Height = 1000;
            //    textBlock1.Background = Brushes.White;
            //    textBlock1.Foreground = Brushes.Black;
            //    textBlock1.FontFamily = new FontFamily("Arial");
            //    textBlock1.FontSize = 80;
            //    textBlock1.FontStretch = FontStretches.UltraExpanded;
            //    //textBlock1.FontStyle = FontStyles.Italic;
            //    textBlock1.FontWeight = FontWeights.Normal;
            //    //textBlock1.LineHeight = Double.NaN;
            //    textBlock1.Padding = new Thickness(5, 10, 5, 10);
            //    textBlock1.TextAlignment = System.Windows.TextAlignment.Left;
            //    textBlock1.TextWrapping = TextWrapping.Wrap;
            //    //textBlock1.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
            //    //textBlock1.Typography.SlashedZero = true;
            //    Canvas.SetLeft(textBlock1, 2000);
            //    Canvas.SetTop(textBlock1, 3500);
            //    myCanvas.Children.Add(textBlock1);





            //    ProfileDraw(myCanvas, strokethickness, np_x, np_y, zoomfactor, myProduct);

            //    ItemTyp myItemProperties = new ItemTyp();
            //    Einbauebene myEinbauebene = new Einbauebene();

            //    // Items ohne Block
            //    foreach (Item myItem in myProduct.ListeItems) //if (myItem.Block.ToString() == "Schiene")
            //        {
            //            try
            //            {
            //            if (myItem.Typ.ToString() == "GS400")
            //            {
            //                GSDraw(np_x + myItem.Pos, np_y-myItem.Ypos, 400, myItem.Laenge, myCanvas);
            //            }
            //            else if (myItem.Typ.ToString() == "GS500")
            //            {
            //                GSDraw(myItem.Pos, myItem.Ypos, 500, myItem.Laenge, myCanvas);
            //            }
            //            else
            //            {
            //                myItemProperties = ItemProperties(myItem.Typ.ToString());
            //                //myEinbauebene = EinbauebeneProperties(myItem.EinbauebeneID);
            //                myEinbauebene = new Einbauebene();
            //                myEinbauebene = myProduct.ListeEinbauebenen.Find(x => x.ID == myItem.EinbauebeneID);
            //                for (int i = 1; i <= myItem.Anzahl; i++)
            //                {
            //                    double xpos = np_x + myItem.Pos + (i - 1) * myItemProperties.Abstand + myItemProperties.Xoffset;
            //                    //double ypos = np_y + myItemProperties.Yoffset - GetEinbauebeneYoffset(myItem.EinbauebeneID);
            //                    double ypos = np_y - myItem.Ypos + myItemProperties.Yoffset - myEinbauebene.Y;
            //                    ItemDraw(myCanvas, myItem.Typ.ToString(), xpos, ypos + 2, myItem.Rotation, zoomfactor, myItemProperties.Faktor);

            //                    if (myItem.Kreis != null)
            //                    {
            //                        TextDraw(myItem.Kreis.ToString(), xpos, ypos, zoomfactor, (Color)ColorConverter.ConvertFromString("Black"), myCanvas);
            //                    }
            //                }
            //            }

            //            }
            //            catch
            //            {
            //                MessageBox.Show("ItemProperties Error (Items ohne Block)" + myItem.Typ.ToString());
            //            }
            //        }


            //    //System.Windows.Point realSize = new System.Windows.Point(myCanvas.ActualHeight, myCanvas.ActualWidth);
            //    //System.Windows.Point sizeAvailable = new System.Windows.Point(myScrollViewer.ActualHeight, myScrollViewer.ActualWidth);

            //    //double scaleX = sizeAvailable.X / realSize.X;
            //    //double scaleY = sizeAvailable.Y / realSize.Y;

            //    //double newScale = Math.Round(Math.Min(scaleX, scaleY), 2);

            //    //ScaleTransform scaletransform = new ScaleTransform();
            //    //scaletransform.ScaleX = newScale;

            //    //myCanvas.RenderTransform = scaletransform;


            //}
            public void Draw(Canvas myCanvas, HorizontalCura myProduct, double zoomfactor)
            {
                var LeftItem = myProduct.ListeItems.OrderBy(x => x.Pos).First();
                var RightItem = myProduct.ListeItems.OrderBy(x => x.Pos).Last();

                double np_x = 200 - LeftItem.Pos;
                double np_y = 2200;   // 600

                double strokethickness = 1;

                double cw = (Laenge + 1000 - LeftItem.Pos);
                double CanvasWidth;

                if (cw > 7800) CanvasWidth = cw;
                else CanvasWidth = 7800;

                double CanvasHeight = 5500;

                myCanvas.Height = CanvasHeight;
                myCanvas.Width = CanvasWidth;

                double aw = myCanvas.ActualWidth;
                double ah = myCanvas.ActualHeight;

                //Clear Canvas
                myCanvas.Children.Clear();

                //Rand
                Rectangle rect = new System.Windows.Shapes.Rectangle()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = Brushes.White,
                    Width = CanvasWidth,
                    Height = CanvasHeight
                };
                Canvas.SetLeft(rect, 0);
                Canvas.SetTop(rect, 0);
                myCanvas.Children.Add(rect);

                // Logo, Adresse
                ItemDraw(myCanvas, "MTG", 850, 225, 0, 1, 15);


                // Text Auftrag
                TextBlock textBlock = new TextBlock();
                textBlock.Text = myProduct.Auftrag + "  "
                                + myProduct.BezeichnungAuftrag.ToString() + "\r"
                                + "Position:" + myProduct.Position.ToString() + "  "
                                + myProduct.Anzahl.ToString() + " Stück     " + myProduct.bezeichnungPosition.ToString() + "     "
                                + "  gezeichnet:   " + myProduct.Zeichner
                                + "  Datum:    " + myProduct.Datum;
                textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                textBlock.FontFamily = new FontFamily("Arial");
                textBlock.FontSize = 100;
                textBlock.TextWrapping = TextWrapping.Wrap;
                //textBlock.FontStretch = FontStretches.UltraExpanded;
                //textBlock.FontStyle = FontStyles.Italic;
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.Width = 2000;
                textBlock.Height = 600;
                Canvas.SetLeft(textBlock, 2200);
                Canvas.SetTop(textBlock, 50);
                myCanvas.Children.Add(textBlock);

                //Legende
                double legende_Y = 3500;
                TextlineDraw("Legende", 100, legende_Y, 2000, 120, 80, FontWeights.Bold, Brushes.Black, myCanvas);

                var DistinctItems = myProduct.ListeItems.GroupBy(x => x.Typ).Select(y => y.First()).OrderBy(z => z.EinbauebeneID);  //Jedes Element der Liste nur einmal
                ItemTyp myItemProperties = new ItemTyp();
                //Für jeden Block
                foreach (Item myItem in DistinctItems) //if ((myItem.Block.ToString() != "Schiene") && (myItem.Typ.ToString() != "Block"))
                {
                    myItemProperties = ItemProperties(myItem.Typ.ToString());
                    string legende = myItemProperties.TextLegende.Trim();

                    if (legende.Length > 5)
                    {
                        legende_Y += 150;
                        ItemDraw(myCanvas, myItem.Typ.ToString(), 140, legende_Y + 60, myItem.Rotation, zoomfactor, myItemProperties.Faktor);
                        TextlineDraw(legende, 200, legende_Y, 2500, 100, 80, FontWeights.Normal, Brushes.Black, myCanvas);
                    }
                }

                // Bemerkungen
                TextlineDraw("Bemerkungen", 2750, 3500, 2000, 120, 80, FontWeights.Bold, Brushes.Black, myCanvas);

                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = myProduct.Bemerkungstext;
                textBlock1.Width = 3000;
                textBlock1.Height = 1800;
                textBlock1.Background = Brushes.White;
                textBlock1.Foreground = Brushes.Black;
                textBlock1.FontFamily = new FontFamily("Arial");
                textBlock1.FontSize = 80;
                textBlock1.FontStretch = FontStretches.UltraExpanded;
                //textBlock1.FontStyle = FontStyles.Italic;
                textBlock1.FontWeight = FontWeights.Normal;
                //textBlock1.LineHeight = Double.NaN;
                textBlock1.Padding = new Thickness(5, 10, 5, 10);
                textBlock1.TextAlignment = System.Windows.TextAlignment.Left;
                textBlock1.TextWrapping = TextWrapping.Wrap;
                //textBlock1.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
                //textBlock1.Typography.SlashedZero = true;
                Canvas.SetLeft(textBlock1, 2750);
                Canvas.SetTop(textBlock1, 3600);
                myCanvas.Children.Add(textBlock1);

                // Freigabestempel
                ItemDraw(myCanvas, "Freigabe", 6700, 4000, 0, 1, 3);

                //Fussbodenlinie
                System.Windows.Shapes.Line fussboden = new System.Windows.Shapes.Line();
                fussboden.X1 = np_x;
                fussboden.X2 = np_y + myProduct.MontageHoehe;
                fussboden.Y1 = np_x + myProduct.Laenge;
                fussboden.Y2 = np_y + myProduct.MontageHoehe;

                myCanvas.Children.Add(fussboden);

                int sh = 50;  // Höhe der Schraffur
                double xf = fussboden.X1 + sh / 2; ; double yf = fussboden.X2 + sh / 2;
                for (int i = 0; i <= (int)(myProduct.Laenge / sh - 1); i++)
                {
                    ItemDraw(myCanvas, "Schraffur", xf, yf + sh / 2, 0, 1, 1);
                    xf += sh;
                }
                //ItemDraw(myCanvas, "Nullpunkt", x, y, 0, 1, 1);




                ProfileDraw(myCanvas, strokethickness, np_x, np_y, zoomfactor, myProduct);

                //ItemDraw(myCanvas, "IV2_2z_2GS_Seite", 6000, np_y, 0, zoomfactor, 1);


                //ItemTyp myItemProperties = new ItemTyp();
                Einbauebene myEinbauebene = new Einbauebene();

                // Items ohne Block
                foreach (Item myItem in myProduct.ListeItems) //if (myItem.Block.ToString() == "Schiene")
                {
                    try
                    {
                        if (myItem.Typ.ToString() == "GS400")
                        {
                            GSDraw(np_x + myItem.Pos, np_y - myItem.Ypos, 400, myItem.Laenge, myCanvas);
                        }
                        else if (myItem.Typ.ToString() == "GS500")
                        {
                            GSDraw(myItem.Pos, myItem.Ypos, 500, myItem.Laenge, myCanvas);
                        }
                        else
                        {
                            myItemProperties = ItemProperties(myItem.Typ.ToString());
                            //myEinbauebene = EinbauebeneProperties(myItem.EinbauebeneID);
                            myEinbauebene = new Einbauebene();
                            myEinbauebene = myProduct.ListeEinbauebenen.Find(x => x.ID == myItem.EinbauebeneID);
                            for (int i = 1; i <= myItem.Anzahl; i++)
                            {
                                double xpos = np_x + myItem.Pos + (i - 1) * myItemProperties.Abstand + myItemProperties.Xoffset;
                                //double ypos = np_y + myItemProperties.Yoffset - GetEinbauebeneYoffset(myItem.EinbauebeneID);
                                double ypos = np_y - myItem.Ypos + myItemProperties.Yoffset - myEinbauebene.Y;
                                ItemDraw(myCanvas, myItem.Typ.ToString(), xpos, ypos + 2, myItem.Rotation, zoomfactor, myItemProperties.Faktor);

                                if (myItem.Kreis != null)
                                {
                                    TextDraw(myItem.Kreis.ToString(), xpos, ypos, zoomfactor, (Color)ColorConverter.ConvertFromString("Black"), myCanvas);
                                }
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("ItemProperties Error (Items ohne Block)" + myItem.Typ.ToString());
                    }
                }
            }






            public void ProfileDraw(Canvas myCanvas, double strokethickness, double np_x, double np_y, double zoomfactor, HorizontalCura myProduct)
            {
                if (myProduct.ListeProfile != null)
                {
                    foreach (Profil p in myProduct.ListeProfile)
                    {
                        foreach (Profil.Rechteck r in p.ListeRechtecke)
                        {
                            if (true)   // f (r.K1 < r.K2) 
                            {
                                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(p.Farbe));
                                Rectangle rect = new System.Windows.Shapes.Rectangle()
                                {
                                    Stroke = new SolidColorBrush(Colors.Black),
                                    StrokeThickness = strokethickness,
                                    Fill = brush,
                                    Width = Laenge * zoomfactor,
                                    Height = Math.Abs((r.K2 - r.K1)) * zoomfactor
                                };
                                Canvas.SetLeft(rect, np_x * zoomfactor);
                                Canvas.SetTop(rect, np_y * zoomfactor - r.K1 * zoomfactor);
                                myCanvas.Children.Add(rect);
                            }
                        }
                    }
                }
            }


            private void TextDraw(string text, double x, double y, double zoomfactor, Color color, Canvas myCanvas)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = System.Windows.TextAlignment.Center;
                textBlock.Text = text;
                textBlock.Height = 40 * zoomfactor;
                textBlock.Width = 40 * zoomfactor;
                textBlock.FontSize = 35 * zoomfactor;
                textBlock.Foreground = new SolidColorBrush(color);
                Canvas.SetLeft(textBlock, (x-20) * zoomfactor);
                Canvas.SetTop(textBlock, (y-70) * zoomfactor);
                myCanvas.Children.Add(textBlock);
            }

            private void TextlineDraw(string text, double x, double y, double width, double height, double fontsize, FontWeight fontweight, Brush color, Canvas myCanvas)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = System.Windows.TextAlignment.Left;
                textBlock.FontFamily = new FontFamily("Arial");
                textBlock.Text = text;
                textBlock.Width = width;
                textBlock.Height = height;
                textBlock.FontSize = fontsize;
                textBlock.FontWeight = fontweight;
                textBlock.Foreground = color;
                Canvas.SetLeft(textBlock, x);
                Canvas.SetTop(textBlock, y);
                myCanvas.Children.Add(textBlock);
            }

            


            void GSDraw(double xpos, double ypos, double GSHalterAbstand, double GSLaenge, Canvas myCanvas)
            {
                //Halter
                if (GSLaenge <= 120) GSLaenge = 120;

                int AnzahlHalter = (int)((GSLaenge - 60) / GSHalterAbstand + 1);

                if (AnzahlHalter < 2)
                {
                    AnzahlHalter = 2;
                    GSHalterAbstand = GSLaenge - 60;
                }
                double Halter1Xpos = (GSLaenge - (AnzahlHalter - 1) * GSHalterAbstand) / 2;
                //double HalterXpos = Halter1Xpos;

                for (int i = 0; i < AnzahlHalter; i++)
                {
                        double x = xpos + Halter1Xpos + (i * GSHalterAbstand);
                        ItemDraw(myCanvas, "GSHalter", x, ypos,0,1,1);
                }

                //PA
                ItemDraw(myCanvas, "GSErde", xpos + Halter1Xpos, ypos - 90, 0, 1, 1);

                //Schiene
                Rectangle rect = new System.Windows.Shapes.Rectangle()
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(Colors.White),
                    Width = GSLaenge,
                    Height = 25
                };
                Canvas.SetLeft(rect, xpos);
                Canvas.SetTop(rect, ypos - 12.5);
                myCanvas.Children.Add(rect);

            }



            public void ItemDraw(Canvas myCanvas, string ItemString, double xpos, double ypos, double rotate, double zoomfactor, double ItemZoomFaktor)
            {

                UIElement element;
                ItemString = "XAML\\" + ItemString + ".xaml";

                try
                {
                    using (var stream = new FileStream(ItemString, FileMode.Open, FileAccess.Read))
                    {
                        //element1 = XamlReader.Load(stream) as DrawingImage;
                        element = (UIElement)XamlReader.Load(stream);
                    }

                    FrameworkElement fe = element as FrameworkElement;
                    //MessageBox.Show(" Breite: " + fe.Width.ToString() + " Höhe: " + fe.Height.ToString());

                    Canvas myCanvas1 = new Canvas();
                    //myCanvas1.Width = 22;
                    //myCanvas1.Height = 27;

                    ScaleTransform myScaleTransform = new ScaleTransform();

                    myScaleTransform.ScaleY = zoomfactor * ItemZoomFaktor;
                    myScaleTransform.ScaleX = zoomfactor * ItemZoomFaktor;

                    RotateTransform myRotateTransform = new RotateTransform(rotate);

                    TransformGroup myTransformGroup = new TransformGroup();
                    myTransformGroup.Children.Add(myScaleTransform);
                    myTransformGroup.Children.Add(myRotateTransform);

                    //element.RenderTransformOrigin = new System.Windows.Point(fe.Width / 2, fe.Height / 2);
                    //element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

                    element.RenderTransform = myTransformGroup;
                    //MessageBox.Show(" Breite: " + fe.Width.ToString() + " Höhe: " + fe.Height.ToString());

                    xpos = xpos - fe.Width / 2 * ItemZoomFaktor;
                    ypos = ypos - fe.Height / 2 * ItemZoomFaktor;

                    Canvas.SetLeft(element, xpos * zoomfactor);
                    Canvas.SetTop(element, ypos * zoomfactor);

                    myCanvas1.Children.Add(element);
                    myCanvas.Children.Add(myCanvas1);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Item draw Error: " + ItemString);
                }

            }




            public ItemTyp ItemProperties(string myItemTyp)
            {
                ItemTyp ReturnItemTyp = new ItemTyp();
                SqlConnection con = new SqlConnection
                {
                    //ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    // @"AttachDbFilename=C:\Users\sb\Source\Repos\MTG-Draw\MTG-Draw\MTG_Draw\LocalDB.mdf;" +
                    // @"Integrated Security=True;"

                    ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                     @"AttachDbFilename=C:\Users\sb\MTG_Draw\LocalDB.mdf;" +
                     @"Integrated Security=True;"


                };
                con.Open();

                try
                {
                    string SqlString = "SELECT * FROM Items WHERE Bezeichnung = '" + myItemTyp + "'";
                    //MessageBox.Show(SqlString);
                    SqlCommand cmd = new SqlCommand(SqlString, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ReturnItemTyp.Xoffset = (double)reader["Xoffset"];
                            ReturnItemTyp.Yoffset = (double)reader["Yoffset"];
                            ReturnItemTyp.Faktor = (double)reader["Faktor"];
                            ReturnItemTyp.Abstand = (double)reader["Abstand"];
                            ReturnItemTyp.DxfBlock = (string)reader["DxfBlock"];
                            ReturnItemTyp.Layer = (string)reader["Layer"];
                            ReturnItemTyp.TextLegende = (string)reader["TextLegende"];
                            //MessageBox.Show(ReturnItemTyp.Xoffset.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("No rows found.");
                        ReturnItemTyp = null;
                    }
                    reader.Close();
                    con.Close();
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Database Error (ItemProperties)");
                    ReturnItemTyp = null;
                }
                return ReturnItemTyp;
            }

            void DrawBlock(string blockname, double posx1, double posy1, double rotation, Layer layer, DxfDocument mydoc)
            {
                netDxf.Blocks.Block B1 = new netDxf.Blocks.Block(blockname);
                B1 = mydoc.Blocks[blockname];
                if (B1 != null)
                {
                    Insert i1 = new Insert(B1, new Vector2(posx1, posy1));
                    i1.Rotation = rotation;
                    i1.Layer = layer;
                    mydoc.AddEntity(i1);
                }

            }

            void AddBlockToBlock(string fromblock, netDxf.Blocks.Block toblock, double posx1, double posy1, DxfDocument mydoc)
            {
                netDxf.Blocks.Block Bl = new netDxf.Blocks.Block(fromblock);
                Bl = mydoc.Blocks[fromblock];
                if (Bl != null)
                {
                    Insert ins = new Insert(Bl, new Vector2(posx1, posy1));
                    toblock.Entities.Add(ins);
                }
            }

            void DrawDimension(double posx1, double posy1, double posx2, double posy2, double masspos, DxfDocument mydoc)
            {
                netDxf.Entities.AlignedDimension Dim = new netDxf.Entities.AlignedDimension(new Vector2(posx1, posy1), new Vector2(posx2, posy2), masspos);
                Dim.Layer = new Layer("Masse");
                Dim.Style = new DimensionStyle("1XP1");
                mydoc.AddEntity(Dim);
            }

            void DrawDimensionToBlock(Block myBlock, double posx1, double posy1, double posx2, double posy2, double masspos)
            {
                netDxf.Entities.AlignedDimension Dim = new netDxf.Entities.AlignedDimension(new Vector2(posx1, posy1), new Vector2(posx2, posy2), masspos);
                Dim.Layer = new Layer("Masse");
                Dim.Style = new DimensionStyle("1XP1");
                myBlock.Entities.Add(Dim);
            }

            void DrawText(string TextString, double posx1, double posy1, double height, string layer, string style, double rotation, netDxf.Entities.TextAlignment textalignment ,DxfDocument mydoc)
            {
                netDxf.Entities.Text myText = new Text(TextString, new Vector3(posx1, posy1, 0), height);
                myText.Layer = new Layer(layer);
                myText.Style = new TextStyle(style);
                myText.Rotation = rotation;
                myText.Alignment = textalignment;                     //TextAlignment.BaselineCenter;
                mydoc.AddEntity(myText);
            }

            void CreateGSBlock(double GSHalterAbstand, double GSLaenge, DxfDocument mydoc, HorizontalCura mycura)
            {
                // GS zeichnen
                try
                {
                    netDxf.Blocks.Block GS = new netDxf.Blocks.Block("GS");   //Schiene
                    GS = mydoc.Blocks["GS"];
                    netDxf.Blocks.Block GSHalter = new netDxf.Blocks.Block("GSHalter");   //Halter
                    GSHalter = mydoc.Blocks["GSHalter"];
                    netDxf.Blocks.Block ErdSymbol = new netDxf.Blocks.Block("ErdSymbol");   //Erdsymbol
                    ErdSymbol = mydoc.Blocks["ErdSymbol"];

                    string BlockName = "GS" + GSHalterAbstand.ToString() + "_" + GSLaenge.ToString();
                    netDxf.Blocks.Block GSneu = new netDxf.Blocks.Block(BlockName);   //neue GS

                    if (GSLaenge <= 120) GSLaenge = 120;

                    Insert InsertGS = new Insert(GS, new Vector2(0, 0));
                    InsertGS.Scale = new Vector3((GSLaenge / 1000.0), 0.0, 0.0);
                    GSneu.Entities.Add(InsertGS);

                    int AnzahlHalter = (int)((GSLaenge - 60) / GSHalterAbstand + 1);

                    //MessageBox.Show("AnzahlHalter:" + AnzahlHalter);

                    if (AnzahlHalter < 2)
                    {
                        AnzahlHalter = 2;
                        GSHalterAbstand = GSLaenge - 60;
                    }

                    //MessageBox.Show("AnzahlHalter:" + AnzahlHalter);


                    double Halter1Xpos = (GSLaenge - (AnzahlHalter - 1) * GSHalterAbstand) / 2;
                    double HalterXpos = Halter1Xpos;

                    Insert InsertErdsymbol = new Insert(ErdSymbol, new Vector2(Halter1Xpos, 80));
                    GSneu.Entities.Add(InsertErdsymbol);

                    for (int i = 0; i < AnzahlHalter; i++)
                    {
                        Insert InsertGSHalter = new Insert(GSHalter, new Vector2(HalterXpos, 0));
                        GSneu.Entities.Add(InsertGSHalter);
                        HalterXpos += GSHalterAbstand;
                    }

                    //Bemassung GS als Block zum GS Block 
                    double massab2 = -100;

                    netDxf.Blocks.Block dimBlock = new netDxf.Blocks.Block(BlockName + "_Dim");

                    DrawDimensionToBlock(dimBlock, 0, 0, Halter1Xpos, 0, massab2);
                    DrawDimensionToBlock(dimBlock, 0 + Halter1Xpos, 0, Halter1Xpos + GSHalterAbstand, 0, massab2);
                    if (mycura.Laenge != GSLaenge)
                    {
                        DrawDimensionToBlock(dimBlock, 0, 0, GSLaenge, 0, 2 * massab2);
                    }
                    //drawDimensionToBlock(dimBlock, 0, 0, 0,??? , -massab2);

                    //Zeichnen Ohne Zeichnen wird keinn Block in der DXF Datei angelegt
                    Insert InsertGSneu = new Insert(GSneu, new Vector2(-10000, 0));
                    mydoc.AddEntity(InsertGSneu);

                    Insert InsertDim = new Insert(dimBlock, new Vector2(-10000, 0));
                    mydoc.AddEntity(InsertDim);

                    AddBlockToBlock(BlockName + "_Dim", GSneu, 0, 0, mydoc);
                }
                catch
                {
                    MessageBox.Show("Geräteschienenblock nicht gefunden");
                }
            }
            

            public void DxfOut(string Filename, HorizontalCura myProduct)
            {
                DxfDocument doc = DxfDocument.Load("Start.dxf");
                doc.DrawingVariables.InsUnits = DrawingUnits.Millimeters;

                // Text, Logo etc.
                Layer docLayer = doc.Layers["0"];

                double legende_Y = -1 * myProduct.MontageHoehe - 600;

                try
                {
                    double logoPosX = -500;
                    double logoPosY = 2100;
                    double textPosX = 600;
                    double textPosY = 2100;
                    double TextHeight = 70;
                    DrawBlock("MTGlogo", logoPosX, logoPosY, 0, docLayer, doc);
                    DrawText("Auftrag:" , textPosX, textPosY -= 1.0 * TextHeight, TextHeight, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                    DrawText("Position:" , textPosX, textPosY-= 1.5 * TextHeight, TextHeight,"Text","Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                    DrawText("Version:  ", textPosX, textPosY -= 1.5 * TextHeight, TextHeight, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                    textPosY = 2100;
                    textPosX = 1100;
                    DrawText(myProduct.BezeichnungAuftrag.ToString(), textPosX, textPosY -= 1.0 * TextHeight, TextHeight, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                    DrawText(myProduct.Position.ToString() + "    " + myProduct.Anzahl.ToString() + " Stück     " + myProduct.BezeichnungPosition, textPosX, textPosY -= 1.5 * TextHeight, TextHeight, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                    DrawText(myProduct.Version + "  gezeichnet:   "  + myProduct.Zeichner + "      Datum:    " + myProduct.Datum, textPosX, textPosY -= 1.5 * TextHeight, TextHeight, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);

                    logoPosX = 5000;
                    logoPosY = legende_Y;
                    DrawBlock("Freigabe", logoPosX, logoPosY, 0, docLayer, doc);
                    
                    textPosY = legende_Y;
                    textPosX = 2500;

                    netDxf.Entities.MText myMText = new MText();
                    myMText.Position = new Vector3(textPosX, textPosY, 0.0);
                    string replacement = myProduct.Bemerkungstext.Replace("\n\r","\\P");
                    replacement = replacement.Replace("\n", "\\P");
                    replacement = replacement.Replace("\r", "\\P");
                    myMText.Value = replacement;
                    myMText.Height = 50;
                    //myMText.RectangleWidth = 1000;
                    myMText.Layer = new Layer("0");
                    myMText.Style = new TextStyle("RomansF1");                        
                    myMText.Rotation = 0;
                    myMText.LineSpacingFactor = 1.5;
                    
                    doc.AddEntity(myMText);

                }

                catch
                {

                }

                // Schiene zeichnen
                netDxf.Blocks.Block BlockSchiene = new netDxf.Blocks.Block("BlockSchiene");   //Schiene
                BlockSchiene = doc.Blocks[myProduct.ProfilTyp.ToString()];  // Blockname muss noch vom Element kommen
                try
                {
                    Insert InsertSchiene = new Insert(BlockSchiene, new Vector2(0, 0));
                    InsertSchiene.Scale = new Vector3((myProduct.Laenge / 1000.0), 0.0, 0.0);
                    doc.AddEntity(InsertSchiene);
                }
                catch
                {
                    MessageBox.Show("Schienenblock nicht gefunden");
                }

                // aus Datenbank
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                @"AttachDbFilename=C:\Users\sb\Source\Repos\MTG-Draw\MTG-Draw\MTG_Draw\LocalDB.mdf;" +
                @"Integrated Security=True;";

                ItemTyp myItemProperties = new ItemTyp();
                Einbauebene myEinbauebene = new Einbauebene();
                //Blöcke zusammenbauen
                var DistinctItems = myProduct.ListeItems.GroupBy(x => x.Typ).Select(y => y.First()).OrderBy( z => z.EinbauebeneID);  //Jedes Element der Liste nur einmal

                // Startpunkt Legende


                DrawText("LEGENDE", 0, legende_Y, 75, docLayer.ToString(), "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                legende_Y -= 200;

                //Für jeden Block
                foreach (Item myItem in DistinctItems) //if ((myItem.Block.ToString() != "Schiene") && (myItem.Typ.ToString() != "Block"))
                    {
                        myItemProperties = ItemProperties(myItem.Typ.ToString());
                        string legende = myItemProperties.TextLegende.Trim();

                        if ( legende.Length > 5)
                        {

                        //Console.WriteLine(myItem.Typ.ToString());
                        //Console.WriteLine(legende);
                        DrawBlock(myItem.Typ.ToString(), 0, legende_Y, 0, docLayer, doc);
                        DrawText(legende, 100, legende_Y -25, 50,  docLayer.ToString(), "Romans", 0,netDxf.Entities.TextAlignment.BaselineLeft, doc);
                        legende_Y -= 150;
                        }
                    }

                //Items Zeichnen
                foreach (Item myItem in myProduct.ListeItems) // if (myItem.Block.ToString() == "Schiene")
                    {
                        try
                        {
                            if (myItem.Typ.ToString() == "GS400")
                            {
                                Layer myLayer = doc.Layers["GS"];
                                CreateGSBlock(400, myItem.Laenge, doc, myProduct);
                                DrawBlock("GS400_" + myItem.Laenge.ToString(), myItem.Pos, myItem.Ypos, myItem.Rotation, myLayer, doc);
                                //drawDimension(myItem.Pos, myItem.Ypos - myProduct.MontageHoehe, myItem.Pos, 0 , 200);
                            }
                            else if (myItem.Typ.ToString() == "GS500")
                            {
                                Layer myLayer = doc.Layers["GS"];
                                CreateGSBlock(500, myItem.Laenge, doc, myProduct);
                                DrawBlock("GS500_" + myItem.Laenge.ToString(), myItem.Pos, myItem.Ypos, myItem.Rotation, myLayer, doc);
                                //drawDimension(myItem.Pos, myItem.Ypos - myProduct.MontageHoehe, myItem.Pos, 0 , 200);
                            }
                            else if (myItem.Typ.ToString() == "Einspeisekanal_2z")
                            {
                            netDxf.Blocks.Block Einspeisekanal = new netDxf.Blocks.Block("Einspeisekanal_2z");   //Schiene
                            Insert Insert_Einspeisekanal = new Insert(Einspeisekanal, new Vector2(myItem.Pos,myItem.Ypos));
                            Insert_Einspeisekanal.Scale = new Vector3(0.0, myItem.Laenge, 0.0);
                            doc.AddEntity(Insert_Einspeisekanal);
                            }
                            else
                            {
                                myItemProperties = ItemProperties(myItem.Typ.ToString());
                                Layer myLayer = doc.Layers[myItemProperties.Layer];
                                myEinbauebene = new Einbauebene();
                                myEinbauebene = myProduct.ListeEinbauebenen.Find(x => x.ID == myItem.EinbauebeneID);

                                double xpos = myItem.Pos;
                                double ypos = myEinbauebene.Y + myItem.Ypos;
                                //Console.WriteLine(myItem.Typ.ToString());
                                DrawBlock(myItem.Typ.ToString(), xpos, ypos, myItem.Rotation, myLayer, doc); 
                        
                            if (myItem.Kreis != null)
                            {
                                DrawText(myItem.Kreis.ToString(), xpos, ypos + 30, 30,"0", "Romans", 0, netDxf.Entities.TextAlignment.BaselineCenter, doc);
                            }
                               

                                //for (int i = 1; i <= myItem.Anzahl; i++)
                                //{
                                //    double xpos = myItem.Pos + (i - 1) * myItemProperties.Abstand;
                                //    double ypos = myEinbauebene.Y + myItem.Ypos;
                                //    drawBlock(myItem.Typ.ToString(), xpos, ypos, myItem.Rotation, doc);
                                //}
                            }

                        }
                        catch
                        {
                            MessageBox.Show("ItemProperties Error ItemID: " + myItem.ID + "myItem.Typ" + myItem.Typ.ToString());
                        }
                    }

                // Blöcke zeichnen
                foreach (Item myItem in myProduct.ListeItems) if (myItem.Typ.ToString() == "Block")
                    {
                        try
                        {
                            myItemProperties = ItemProperties(myItem.Typ.ToString());
                            myEinbauebene = new Einbauebene();
                            myEinbauebene = myProduct.ListeEinbauebenen.Find(x => x.ID == myItem.EinbauebeneID);
                            for (int i = 1; i <= myItem.Anzahl; i++)
                            {
                                double xpos = myItem.Pos + (i - 1) * myItemProperties.Abstand;
                                double ypos = myEinbauebene.Y;
                                DrawBlock(myItem.Block.ToString(), xpos, 0, myItem.Rotation, doc.Layers["0"], doc);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ItemProperties Error ItemID: " + myItem.ID + "myItem.Typ" + myItem.Typ.ToString());
                        }
                    }

                // Bemassung zeichnen
                double masspos1 = -300;    //Maßlinie Bettachsen unter Mitte Schiene
                double massab = -200;      //Abstand Masslinie
                double lastx = 0;

                myProduct.ListeItems = myProduct.ListeItems.OrderBy(f => f.Pos).ToList();

                foreach (Item myItem in myProduct.ListeItems) if (myItem.Typ.ToString() == "Block")
                    {
                        DrawDimension(lastx, masspos1, myItem.Pos, masspos1, massab, doc);
                        lastx = myItem.Pos;
                    }
                DrawDimension(lastx, masspos1, myProduct.Laenge, masspos1, massab, doc);
                massab -= 100;
                DrawDimension(0, masspos1, myProduct.Laenge, masspos1, massab, doc);


                if (myProduct.MontageHoehe != 0)
                {
                    // Diese Bemassungen nur wenn Montagehöhe angegeben
                    // Bemassung vertikal
                    System.Collections.Generic.List<Double> yDimList = new List<Double>();
                    var DistinctItems2 = myProduct.ListeItems.GroupBy(y => y.Ypos).Select(y => y.First());                  //.Where(x => x.Block == BlockName.Schiene);
                    foreach (var myItem in DistinctItems2) yDimList.Add((double)myItem.Ypos);

                    massab = 100;
                    Double lastY = -3000;
                    foreach (double myYpos in yDimList.OrderBy(d => d))
                    {
                        //Console.WriteLine(myYpos.ToString());

                        DrawBlock("Koordinate", -600, myYpos, 90, doc.Layers["0"], doc);
                        if (myYpos - lastY < 60) DrawText((myYpos + MontageHoehe).ToString(), -500, myYpos + 60, 50, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                        else DrawText((myYpos + MontageHoehe).ToString(), -500, myYpos, 50, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                        lastY = myYpos;
                    }
                    // Null
                    DrawBlock("Koordinate", -600, -MontageHoehe, 90, doc.Layers["0"], doc);
                    DrawText("0", -500, -MontageHoehe, 50, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);

                    // Position der Schiene
                    DrawBlock("Koordinate", -600, 0, 90, doc.Layers["0"], doc);
                    DrawText(MontageHoehe.ToString(), -500, 0, 50, "Text", "Romans", 0, netDxf.Entities.TextAlignment.BaselineLeft, doc);

                    /////x => x.Block == BlockName.Schiene

                    //Bemassung horizontal
                    System.Collections.Generic.List<Double> xDimList = new List<Double>();
                    var DistinctItems3 = myProduct.ListeItems.Where(x => (x.Typ == ItemName.Block) || (x.Block == BlockName.Schiene) || x.Typ == ItemName.GS400 || x.Typ == ItemName.GS500).GroupBy(x => x.Pos).Select(x => x.First());

                    //var DistinctItems3 = myProduct.ListeItems.GroupBy(x => x.Pos).Select(x => x.First()).Where(x => x.Typ == ItemName.Block).Where(y =>y.Block == BlockName.Schiene);


                    foreach (var myItem in DistinctItems3) xDimList.Add((double)myItem.Pos);

                    double MassUnten = -myProduct.MontageHoehe - 200;
                    massab = -100;
                    Double lastX = -3000;
                    foreach (double myXpos in xDimList.OrderBy(d => d))
                    {
                        //Console.WriteLine(myXpos.ToString());
                        //drawDimension(0, MassUnten, myXpos, MassUnten, massab);
                        //MassUnten -= 100;

                        DrawBlock("Koordinate", myXpos, -MontageHoehe - 300, 0, doc.Layers["0"], doc);
                        if (myXpos - lastX < 60) DrawText(myXpos.ToString(), myXpos + 60, -MontageHoehe - 250, 50, "Text", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                        else DrawText(myXpos.ToString(), myXpos, -MontageHoehe - 250, 50, "Text", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                        //MessageBox.Show(lastX.ToString() + "    " + myXpos.ToString());
                        lastX = myXpos;
                    }


                    //Fussbodenlinie
                    netDxf.Blocks.Block B2 = new netDxf.Blocks.Block("Fussboden");
                    netDxf.Entities.Line line = new netDxf.Entities.Line(new Vector2(0, -1 * myProduct.MontageHoehe), new Vector2(myProduct.Laenge, -1 * myProduct.MontageHoehe));
                    line.Layer = new Layer("0");
                    line.Layer.Color.Index = 255;
                    //doc.AddEntity(line);
                    B2.Entities.Add(line);
                    //Console.WriteLine(myProduct.Laenge);

                    //drawDimension(0, 0, 0, -myProduct.MontageHoehe, -2 * massab);

                    int sh = 50;  // Höhe der Schraffur
                    for (int i = 0; i <= (int)(myProduct.Laenge / sh - 1); i++)
                    {
                        netDxf.Entities.Line line1 = new netDxf.Entities.Line(new Vector2(i * sh, -myProduct.MontageHoehe - sh), new Vector2((i + 1) * sh, -myProduct.MontageHoehe));
                        line1.Layer = new Layer("Schraffur");
                        line1.Layer.Color.Index = 1;     // 1 = rot
                        //doc.AddEntity(line1);
                        B2.Entities.Add(line1);
                    }

                    netDxf.Entities.Text myText = new Text("Fertigfussboden", new Vector3(50, -MontageHoehe + 10, 0), 30);
                    myText.Layer = new Layer("Text");
                    myText.Style = new TextStyle("Romans");
                    B2.Entities.Add(myText);

                    netDxf.Blocks.Block B3 = new netDxf.Blocks.Block("Ursprung");
                    B3 = doc.Blocks["Ursprung"];
                    Insert InsertUrsprung = new Insert(B3, new Vector2(0, -MontageHoehe));
                    B2.Entities.Add(InsertUrsprung);

                    Insert InsertFussbodenBlock = new Insert(B2, new Vector2(0, 0));
                    doc.AddEntity(InsertFussbodenBlock);





                }

                //Seitenansicht  Bemassungen in Blöcken müssen aufgelöst werden
                //Der Name des Blocks der Seitenansicht ist der Name des Profiltyps + _Seite
                double abstandseitenansicht = 500;
                double maxPos = myProduct.ListeItems.Max(t => t.Pos);
                double posx = 0;

                if (maxPos > myProduct.Laenge) posx = maxPos + abstandseitenansicht;
                else posx = myProduct.Laenge + abstandseitenansicht;

                double posy = 0;
                string myblockname = myProduct.ProfilTyp + "_Seite";
                //MessageBox.Show("Seitenansicht " + myblockname);
                try
                {
                    DrawBlock(myblockname, posx, posy, 0, doc.Layers["0"], doc);
                }
                catch
                {
                    MessageBox.Show("Seitenansicht " + myblockname + " nicht gefunden");
                }

                //Werkstattbemassung
                
                var DistinctItems1 = myProduct.ListeItems.GroupBy(x => x.EinbauebeneID).Select(x => x.Max());

                var item = myProduct.ListeItems.OrderByDescending(i => i.EinbauebeneID).FirstOrDefault();
                int AnzahlEinbauebenen = item.EinbauebeneID;

                Double ypos1 = 500;
                for (int einbauebene = 1; (einbauebene <= AnzahlEinbauebenen); einbauebene++)
                {


                    try
                    {
                        var DimItems = myProduct.ListeItems.Where(x => (x.EinbauebeneID == einbauebene)).GroupBy(x => x.Pos).Select(x => x.Max());
                        
                        Double wert = 0;
                        Double lastX = 0;
                        Double deckelbeginn = 0;
                        Double xpos = 0;

                    
                        foreach (var myItem in DimItems)
                        {
                            xpos = myItem.Pos;
                            if (myItem.Typ == ItemName.Deckelschnitt) deckelbeginn = xpos;
                            wert = xpos - deckelbeginn;
                            DrawBlock("Koordinate", xpos, ypos1, 0, doc.Layers["Werkstatt"], doc);
                            if (xpos - lastX < 40) DrawText(wert.ToString() + "-" + xpos, xpos, ypos1 + 200, 30, "Werkstatt", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                            else DrawText(wert.ToString() + "-" + xpos, xpos, ypos1, 30, "Werkstatt", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc);
                            lastX = xpos;
                            //Console.WriteLine(myItem.Pos.ToString() + " " + myItem.ID);
                        }
                        lastX = 0;
                        ypos1 += 300;

                    }
                    catch
                    {
                        MessageBox.Show("Fehler in Einbauebene" + einbauebene.ToString() + " Komponenten mit gleicher Position? ");
                    }


                }
                    







                //Speichern
                try
                {
                    doc.Save(Filename);
                }
                catch
                {
                    MessageBox.Show("Datei kann nicht gespeichert werden, ist eventuell in anderem Programm geöffnet");
                }
            }

            public void DxfOutWerkstatt(string Filename, HorizontalCura myProduct)
            {
                DxfDocument doc1 = DxfDocument.Load("Start.dxf");
                doc1.DrawingVariables.InsUnits = DrawingUnits.Millimeters;



                System.Collections.Generic.List<Double> xDimList = new List<Double>();
                var DistinctItems1 = myProduct.ListeItems.GroupBy(x => x.EinbauebeneID).Select(x => x.Max());

                var item = myProduct.ListeItems.OrderByDescending(i => i.EinbauebeneID).FirstOrDefault();
                int AnzahlEinbauebenen = item.EinbauebeneID;

                Double lastX = 0;
                double posx = 0;
                double posy = 0;

                for (int einbauebene = 1; (einbauebene <= AnzahlEinbauebenen); einbauebene++)
                {
                    var DistinctItems2 = myProduct.ListeItems.Where(x => (x.Typ == ItemName.Deckelschnitt) && (x.EinbauebeneID == einbauebene)).GroupBy(x => x.Pos).Select(x => x.Max());
                    foreach (var myItem in DistinctItems2) xDimList.Add((double)myItem.Pos);
                    xDimList.Add(myProduct.Laenge);

                    double MassUnten = -myProduct.MontageHoehe - 200;


                    foreach (double myXpos in xDimList.OrderBy(d => d))
                    {
                        //Console.WriteLine(myXpos.ToString());

                        netDxf.Blocks.Block B1 = new netDxf.Blocks.Block("Deckel_136");
                        B1 = doc1.Blocks["Deckel_136"];

                        if (B1 != null)
                        {
                            Insert i1 = new Insert(B1, new Vector2(posx, posy));
                            double laenge = myXpos - lastX;
                            i1.Scale = new Vector3((laenge / 1000.0), 0.0, 0.0);
                            doc1.AddEntity(i1);
                            DrawDimension(posx, posy - 100, posx + laenge, posy - 100, -200, doc1);
                            posx += laenge;
                            posx += 500;
                        }

                        DrawBlock("Koordinate", myXpos, -MontageHoehe - 300, 0, doc1.Layers["0"], doc1);
                        if (myXpos - lastX < 60) DrawText(myXpos.ToString(), myXpos + 60, -MontageHoehe - 250, 50, "Text", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc1);
                        else DrawText(myXpos.ToString(), myXpos, -MontageHoehe - 250, 50, "Text", "Romans", 90, netDxf.Entities.TextAlignment.BaselineLeft, doc1);
                        //MessageBox.Show(lastX.ToString() + "    " + myXpos.ToString());
                        lastX = myXpos;
                    }
                    xDimList.Clear();
                    lastX = 0;
                    posx = 0;
                    posy -= 1000;
                }
                //Speichern
                try
                {
                    doc1.Save(Filename);
                }
                catch
                {
                    MessageBox.Show("Datei kann nicht gespeichert werden, ist eventuell in anderem Programm geöffnet");
                }
            }

            public void Bloecke_aktualisieren(HorizontalCura myProduct)
            {
                List<Item> myListeItems = new List<Item>();
                ItemTyp myItemProperties = new ItemTyp();

                myProduct.ListeItems.RemoveAll(x => x.Block != BlockName.Schiene  &&  x.Typ.ToString() != "Block");                     

                int myID = 1000;
                foreach (Item myItem in myProduct.ListeItems) if (myItem.Typ.ToString() == "Block")
                {
                    foreach (Item myItem1 in myProduct.ListeBlockItems) if (myItem1.Block == myItem.Block)
                    {
                        try
                        {
                            myItemProperties = ItemProperties(myItem1.Typ.ToString());
                            for (int i = 1; i <= myItem1.Anzahl; i++)
                            {
                                double xpos = myItem.Pos + myItem1.Pos + (i - 1) * myItemProperties.Abstand;
                                Item myItem3 = new Item();
                                myItem3 = myItem1.Clone();
                                myItem3.Pos = xpos;
                                myItem3.Anzahl = 1;
                                myItem3.ID = myID;
                                myID += 10;        

                                myListeItems.Add(myItem3);

                            }
                        }
                        catch
                        {
                            MessageBox.Show("ItemProperties Error ItemID: " + myItem.ID + "myItem.Typ" + myItem.Typ.ToString());
                        }
                    }
                }
                try
                {
                    foreach (Item myItem in myListeItems) myProduct.ListeItems.Add(myItem);
                    myListeItems.Clear();
                }
                catch
                {
                    MessageBox.Show("Fehler bei der Blockverarbeitung");
                }
            }
            #endregion
        }
        #endregion
    }
}

