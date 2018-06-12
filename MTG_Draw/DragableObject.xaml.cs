using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Dragable1
{
    /// <summary>
    /// Interaktionslogik für DragableObject.xaml
    /// </summary>
    public partial class DragableObject : UserControl
    {

            public DragableObject()
            {
                InitializeComponent();

                this.MouseLeftButtonDown += new MouseButtonEventHandler(DragableObject_MouseLeftButtonDown);
                this.MouseLeftButtonUp += new MouseButtonEventHandler(DragableObject_MouseLeftButtonUp);
                this.MouseMove += new MouseEventHandler(DragableObject_MouseMove);

            var draggableControl = this as UserControl;

            var transform = draggableControl.RenderTransform as TranslateTransform;
            transform = new TranslateTransform();
            transform.X = XPos;
            transform.Y = YPos;
            draggableControl.RenderTransform = transform;
            }

            public double XPos = 100;
            public double YPos = 100;
            protected bool isDragging;
            private Point clickPosition;


            private void DragableObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                isDragging = true;
                var draggableControl = sender as UserControl;
                clickPosition = e.GetPosition(this as UIElement);
                draggableControl.CaptureMouse();
            }

            private void DragableObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                isDragging = false;
                var draggable = sender as UserControl;
            Point currentPosition = e.GetPosition(this.Parent as UIElement);

            XPos = currentPosition.X;
            YPos = currentPosition.Y;



            draggable.ReleaseMouseCapture();



            }

            private void DragableObject_MouseMove(object sender, MouseEventArgs e)
            {
                var draggableControl = sender as UserControl;

                if (isDragging && draggableControl != null)
                {
                    Point currentPosition = e.GetPosition(this.Parent as UIElement);

                    var transform = draggableControl.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        draggableControl.RenderTransform = transform;
                    }

                    transform.X = SnapPosition(currentPosition.X - clickPosition.X, 10);
                    //transform.Y = SnapPosition(currentPosition.Y - clickPosition.Y, 10);
                }
            }

            private double SnapPosition(double position, double gridSize)
            {
                return (Math.Truncate(position / gridSize) * gridSize);
            }
        }

        public class MVObjectManager
        {
            public ObservableCollection<MVDragableObject> ListObjects { get; set; }
            public MVObjectManager()
            {
                ListObjects = new ObservableCollection<MVDragableObject>();
            }
        }
        public class MVDragableObject
        {
        }
    }

