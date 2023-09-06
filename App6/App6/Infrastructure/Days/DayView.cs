using System;
using Xamarin.Forms;

namespace App6.Infrastructure.Days
{
    internal class DayView : ContentView
    {
        public static readonly BindableProperty CheckBoxProperty = BindableProperty.Create(nameof(CheckBox), typeof(CheckBox), typeof(DayView), new CheckBox());
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(Label), typeof(DayView), new Label { Text = "null" });

        public CheckBox CheckBox 
        {
            get => (CheckBox)GetValue(CheckBoxProperty);
            set => SetValue(CheckBoxProperty, value);
        }
        public Label Label 
        { 
            get => (Label)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public DayView()
        {
            CheckBox = new CheckBox();
        }
    }
}