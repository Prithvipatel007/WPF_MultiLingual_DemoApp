using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace WPF_MultiLingual_AppDemo
{

    public enum MyLanguage
    {
        English,
        German
    }


    public class Settings
    {
        public MyLanguage Language { get; set; } = MyLanguage.English;

        public void Save(string path)
        {
            var contents = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, contents);
        }

        public static Settings Load(string path)
        {
            var contents = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Settings>(contents);
            
        }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string SETTINGS_PATH = "settings.json";
        private ResourceDictionary _LanguageDictionary = new ResourceDictionary();

        public App()
        {
            InitializeComponent();
            Resources.MergedDictionaries.Add(_LanguageDictionary);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow();
            window.LanguageChanged += OnLanguageChanged;
            MainWindow = window;


            if (File.Exists(SETTINGS_PATH))
            {
                var settings = Settings.Load(SETTINGS_PATH);
                window.AppLanguage = settings.Language;
            }

            MainWindow.Show();
        }

        private void OnLanguageChanged(MyLanguage language)
        {
            switch (language)
            {
                case MyLanguage.English:
                    _LanguageDictionary.Source = new Uri("Language/Language.en-GB.xaml", UriKind.Relative);
                    break;
                case MyLanguage.German:
                    _LanguageDictionary.Source = new Uri("Language/Language.de-DE.xaml", UriKind.Relative);
                    break;
                default:
                    _LanguageDictionary.Clear();
                    break;
            }

            var settings = new Settings() { Language = language };
            settings.Save(SETTINGS_PATH);
        }




    }
}
