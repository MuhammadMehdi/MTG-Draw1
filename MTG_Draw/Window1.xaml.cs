using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MTG_Draw.Model
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //public class Startwert
        //{
        //    string  xpos = "500";
        //} 

        public Window1()
        {
            string xpos = "500";
            InitializeComponent();
            textbox1.Text = xpos;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Testschiene.DeckelschnitteErzeugen( Convert.ToDouble(textbox1.Text.ToString()));
        }
    }
}
