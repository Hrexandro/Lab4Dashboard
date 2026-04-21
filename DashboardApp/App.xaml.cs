using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;

namespace DashboardApp
{
    public partial class App : Application
    {
        private CompositionContainer _container;
        private DirectoryCatalog _directoryCatalog;
        private FileSystemWatcher _watcher;
        private MainWindow _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

            if (!Directory.Exists(pluginFolder))
            {
                Directory.CreateDirectory(pluginFolder);
            }

            _directoryCatalog = new DirectoryCatalog(pluginFolder, "*.dll");

            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            aggregateCatalog.Catalogs.Add(_directoryCatalog);

            _container = new CompositionContainer(aggregateCatalog);

            _mainWindow = new MainWindow();
            _container.ComposeParts(_mainWindow);
            _mainWindow.LoadWidgets();
            _mainWindow.Show();

            _watcher = new FileSystemWatcher(pluginFolder, "*.dll");
            _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

            _watcher.Created += OnPluginFolderChanged;
            _watcher.Deleted += OnPluginFolderChanged;
            _watcher.Changed += OnPluginFolderChanged;
            _watcher.Renamed += OnPluginFolderChanged;

            _watcher.EnableRaisingEvents = true;
        }

        private void OnPluginFolderChanged(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    _directoryCatalog.Refresh();
                    _mainWindow.LoadWidgets();
                }
                catch
                {
                    // przy kopiowaniu DLL zdarzenie może odpalić się zanim plik będzie gotowy
                }
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _watcher?.Dispose();
            _container?.Dispose();
            base.OnExit(e);
        }
    }
}