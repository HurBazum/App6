using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPage : ContentPage
    {
        public AlarmPage()
        {
            InitializeComponent();
            GetContent();
        }

        void GetContent()
        {
        }

        Label GetLabel()
        {
            return new Label { Text = "Будильник сработает" };
        }

        TimePicker GetTimePicker()
        {
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0)
            };



            return timePicker;
        }

        Button GetButton()
        {
            var addButton = new Button
            {
                Text = "+",
                FontSize = 30,
                BackgroundColor = Color.Azure, 
                
            };

            addButton.Clicked += (s, e) =>
            {
                var l = GetLabel();
                var tp = GetTimePicker();
                var tv = GetTableView(l, tp);
            };

            return addButton;
        }

        TableView GetTableView(Label l, TimePicker tp)
        {

            tp.PropertyChanged += (s, e) => TimePickerHandler(s, e, l, tp);

            var tableView = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection()
                    {
                        new TextCell { Text = l.Text },
                        new ViewCell { View = tp }
                    }
                }
            };


            return tableView;
        }

        void TimePickerHandler(object sender, PropertyChangedEventArgs e, Label l, TimePicker tp)
        {
            if(e.PropertyName == "Time")
            {
                l.Text += " в " + tp.Time;
            }
        }
    }
}