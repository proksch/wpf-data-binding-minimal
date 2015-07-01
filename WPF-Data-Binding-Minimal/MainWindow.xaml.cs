using System.Windows;

namespace WPF_Data_Binding_Minimal
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainDataContext _myContext;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _myContext = new MainDataContext
            {
                Description = "Something",
                UserInfo = new SubDataContext
                {
                    UserName = "Hans"
                }
            };
        }

        public void OnClick(object sender, RoutedEventArgs e)
        {
            var errs = UserControl1.Validate();
            if (errs.Count > 0)
            {
                MessageBox.Show(string.Join(", ", errs));
            }
            else
            {
                MessageBox.Show(string.Format("alle jut '{0}'!", _myContext.UserInfo.UserName));
            }
            // nothing
        }
    }

    public class MainDataContext
    {
        public string Description { get; set; }
        public SubDataContext UserInfo { get; set; }
    }

    public class SubDataContext
    {
        public string UserName { get; set; }
    }
}