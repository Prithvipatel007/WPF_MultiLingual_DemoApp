using System;
using System.Windows;

namespace WPF_MultiLingual_AppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MyLanguage[] Languages { get; set; } = new MyLanguage[]
        {
            MyLanguage.English,
            MyLanguage.German
        };

        public MyLanguage AppLanguage
        {
            get => (MyLanguage)GetValue(AppLanguageProperty);
            set => SetValue(AppLanguageProperty, value);
        }

        public static readonly DependencyProperty AppLanguageProperty = DependencyProperty.Register(
                nameof(AppLanguage),
                typeof(MyLanguage),
               typeof(MainWindow),
               new PropertyMetadata(MyLanguage.English, LanguageProperty_changed));

        private static void LanguageProperty_changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as MainWindow;
            var newLang = (MyLanguage)e.NewValue;
            control?.LanguageChanged?.Invoke(newLang);
        }

        public Action<MyLanguage> LanguageChanged = null;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
