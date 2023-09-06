using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class APage : ContentPage
    {
        int count = 0;

        public APage()
        {
            InitializeComponent();
            Content = GetSmthView();
        }

        Entry GetAgeEntry() => new Entry { Placeholder = "Age" };

        Entry GetNameEntry() => new Entry { Placeholder = "Name" };

        StackLayout GetSmthView()
        {
            var stack = new StackLayout();

            stack.Children.Add(GetGrid());

            stack.Children.Add(GetButton((Grid)stack.Children[0]));

            return stack;
        }

        Grid GetGrid()
        {
            var grid = new Grid();

            List<SmthView> list = new List<SmthView>
            {
                new SmthView { Label = new Label { Text = "Age = 98, Name = \"Gustav\"" }, Chose = new CheckBox() },
                new SmthView { Label = new Label { Text = "Age = 10, Name = \"Name\"" }, Chose = new CheckBox() },
                new SmthView { Label = new Label { Text = "Age = 98, Name = \"Gustav\"" }, Chose= new CheckBox() }
            };

            for(int i = 0; i < list.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                grid.Children.Add(list[i].Label, 0, i);
                grid.Children.Add(list[i].Chose, 1, i); 
            }

            return grid;
        }

        Button GetButton(Grid grid)
        {
            var button = new Button();

            button.Text = "Focus that!";

            View view;

            button.Clicked += (s, e) =>
            {
                if (count < 6)
                {
                    view = grid.Children[count];

                    if (count != 0 && count % 2 == 1)
                    {
                        if(count == 3)
                        {
                            (view as CheckBox).IsChecked = true;
                        }
                        DisplayAlert($"{count}", $"{(view as CheckBox).IsChecked}", "OK");

                    }
                    else
                    {
                        DisplayAlert($"{count}", $"{(view as Label).Text}", "OK");
                    }
                    count++;
                }
            };

            return button;
        }
    }

    public class SmthView : ContentView 
    {
        public static readonly BindableProperty SmthAgeProperty = BindableProperty.Create(nameof(Age), typeof(int), typeof(SmthView), 0);
        public static readonly BindableProperty SmthNameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(SmthView), string.Empty);
        public static readonly BindableProperty SmthCheckBoxProperty = BindableProperty.Create(nameof(Chose), typeof(CheckBox), typeof(SmthView), new CheckBox() { IsChecked = false });
        public static readonly BindableProperty SmthLabelProperty = BindableProperty.Create(nameof(Label), typeof(Label), typeof(SmthView), new Label { Text = "null" });

        public int Age
        {
            get => (int)GetValue(SmthAgeProperty);
            set => SetValue(SmthAgeProperty, value);
        }

        public string Name
        {
            get => (string)GetValue(SmthNameProperty);
            set => SetValue(SmthNameProperty, value);
        }

        public Label Label
        {
            get => (Label)GetValue(SmthLabelProperty);
            set => SetValue(SmthLabelProperty, value);
        }

        public CheckBox Chose
        {
            get => (CheckBox)GetValue(SmthCheckBoxProperty);
            set => SetValue(SmthCheckBoxProperty, value);
        }
    }
}