﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Comet;
using Comet.WPF;
using Microsoft.Extensions.DependencyInjection;

namespace Trains.NET.Comet.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ServiceProvider serviceProvider = BuildServiceProvider();

            InitializeComponent();

#if DEBUG
            global::Comet.Reload.Init();
#endif
            global::Comet.WPF.UI.Init();
            global::Comet.Skia.UI.Init();

            Registrar.Handlers.Register<RadioButton, ToggleButtonHandler>();
            Registrar.Handlers.Register<RadioGroup, ToggleButtonGroupHandler>();

            MainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MainFrame.NavigationService.Navigate(new CometPage(MainFrame, serviceProvider.GetService<MainPage>()));
        }

        private ServiceProvider BuildServiceProvider()
        {
            var col = new ServiceCollection();
            foreach (Assembly a in GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    foreach (Type inter in t.GetInterfaces())
                    {
                        if (inter.Namespace?.StartsWith("Trains.NET", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            col.AddSingleton(inter, t);
                        }
                    }
                }
            }

            col.AddSingleton<MainPage, MainPage>();

            return col.BuildServiceProvider();

            static IEnumerable<Assembly> GetAssemblies()
            {
                yield return typeof(Trains.NET.Engine.IGameBoard).Assembly;
                yield return typeof(Trains.NET.Rendering.IGame).Assembly;
            }
        }
    }
}
