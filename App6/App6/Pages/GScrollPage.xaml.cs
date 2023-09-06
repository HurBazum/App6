using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Xamarin.Forms.Internals;
using System.ComponentModel;
using App6.Infrastructure.Days;

namespace App6.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GScrollPage : ContentPage
    {
        int count = 0;
        List<List<DayOfWeek>> alarmsDaysFromAlarm = new List<List<DayOfWeek>>();

        public GScrollPage()
        {
            InitializeComponent();
            alarmsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            alarmsGrid.Children.Add(GetButton(), 0, 0);
        }

        StackLayout GetStackLayout()
        {
            var stack = new StackLayout
            {
                Children =
                {
                    new Label { Text = $"Будильник #{count}: " },
                    new TimePicker { Time = DateTime.Now.TimeOfDay }
                },
                BackgroundColor = Color.Bisque,
                Margin = new Thickness(15, 0, 15, 5),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(10)
            };

            stack.Children.Add(SetDays(count));
            stack.Children.Add(ShowGrid((Grid)stack.Children[2]));
            stack.Children.Add(new Label { Text = "Громкость:" });
            stack.Children.Add(SetAlarmVolume());
            stack.Children.Add(GetSaveButton((Grid)stack.Children[2], (TimePicker)stack.Children[1], (Label)stack.Children[0], (Slider)stack.Children[5], count));

            return stack;
        }

        #region BUTTONS

        /// <summary>
        /// кнопка добавления нового будильника
        /// </summary>
        Button GetButton()
        {
            var button = new Button
            {
                Text = "Добавить будильник",
                WidthRequest = 30,
                HeightRequest = 30,
                Margin = new Thickness(20, 10)
            };

            button.Clicked += (s, e) =>
            {
                alarmsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                alarmsGrid.Children.Add(GetStackLayout(), 0, alarmsGrid.Children.Count);
                alarmsDaysFromAlarm.Add(new List<DayOfWeek>());
                count++;
            };

            return button;
        }

        /// <summary>
        /// Показать/скрыть таблицу с днями
        /// </summary>
        Button ShowGrid(Grid grid)
        {
            var button = new Button
            {
                Text = "Выбрать дни",
                HeightRequest = 40
            };

            button.Clicked += (s, e) =>
            {
                grid.IsVisible = !grid.IsVisible;
            };

            return button;
        }

        /// <summary>
        /// Кнопка сохранить
        /// </summary>
        Button GetSaveButton(Grid grid, TimePicker timePicker, Label label, Slider slider, int num)
        {
            var button = new Button
            {
                Text = "Сохранить",
                HeightRequest = 40,
                WidthRequest = 140
            };

            // ?
            button.Clicked += (s, e) =>
            {
                if (alarmsDaysFromAlarm[num].Count == 0)
                {
                    SetAlarmsDayAuto(grid, timePicker.Time, alarmsDaysFromAlarm[num]);
                }
                else
                {
                    alarmsDaysFromAlarm[num].Sort();
                }
                
                label.Text = label.Text.Remove(label.Text.IndexOf(':') + 1);

                foreach (var item in alarmsDaysFromAlarm[num])
                {
                    if (!label.Text.Contains(item.ToString()))
                    {
                        label.Text += item.ToString() + " ";
                    }
                }

                DisplayAlert("Будильник установлен", $"Сработает в {timePicker.Time}\nГромкость: {slider.Value}", "OK");
            };

            return button;
        }

        void SetAlarmsDayAuto(Grid grid, TimeSpan alarmTime, List<DayOfWeek> dayOfWeeks)
        {
            if(alarmTime < DateTime.Now.TimeOfDay)
            {
                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
                {
                    var index = grid.Children.IndexOf(x => (x is Label && (x as Label).Text == $"{DateTime.Now.DayOfWeek}"));
                    (grid.Children[index+3] as CheckBox).IsChecked = true;
                    dayOfWeeks.Add(DateTime.Now.DayOfWeek + 1);
                }
                else
                {
                    (grid.Children[1] as CheckBox).IsChecked = true;
                    dayOfWeeks.Add(DayOfWeek.Sunday);
                }
            }
            else
            {
                var index = grid.Children.IndexOf(x => (x is Label && (x as Label).Text == $"{DateTime.Now.DayOfWeek}"));
                (grid.Children[index+1] as CheckBox).IsChecked = true;
                dayOfWeeks.Add(DateTime.Now.DayOfWeek);
            }
        }

        #endregion

        /// <summary>
        /// Таблица выбора дней недели для будильника
        /// </summary>
        /// <param name="num"> указывает на список дней недели для будильника </param>
        Grid SetDays(int num)
        {
            int numberOfAlarm = num;

            List<DayView> days = new List<DayView>
            { 
                new DayView { Label = new Label { Text = $"{DayOfWeek.Sunday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Monday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Tuesday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Wednesday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Thursday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Friday}" } },
                new DayView { Label = new Label { Text = $"{DayOfWeek.Saturday}" } }
            };

            //
            foreach (var check in days)
            {
                check.CheckBox.CheckedChanged += (s, e) =>
                {
                    string addDay = days[days.IndexOf(check)].Label.Text;

                    if (check.CheckBox.IsChecked == true)
                    {
                        alarmsDaysFromAlarm[num].Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), addDay));
                    }
                    if(check.CheckBox.IsChecked == false && alarmsDaysFromAlarm[num].Contains((DayOfWeek)Enum.Parse(typeof(DayOfWeek), addDay)))
                    {
                        alarmsDaysFromAlarm[num].Remove((DayOfWeek)Enum.Parse(typeof(DayOfWeek), addDay));
                    }
                };
            }

            Grid g = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = 200 },
                    new ColumnDefinition { Width = 200 }
                }
            };

            for (int i = 0; i < 7; i++)
            {
                g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
                g.Children.Add(days[i].Label, 0, i);
                g.Children.Add(days[i].CheckBox, 1, i);
            }

            g.Padding = new Thickness(10);
            g.VerticalOptions = LayoutOptions.Center;
            g.HorizontalOptions = LayoutOptions.Center;
            g.IsVisible = false;

            return g;
        }

        Slider SetAlarmVolume()
        {
            var slider = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 100,
                ThumbColor = Color.RosyBrown
            };

            return slider;
        }
    }
}