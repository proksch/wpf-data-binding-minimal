using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Data_Binding_Minimal
{
    public partial class MyUserControl
    {
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName",
            typeof (string), typeof (MyUserControl));

        public PropertyChangedEventHandler PropertyChanged;

        public MyUserControl()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) =>
            {
                UserName = MyDataContext.UserName;
                PropertyChanged += (o, args) =>
                {
                    if (args.PropertyName == "UserName")
                    {
                        MyDataContext.UserName = (string) o;
                    }
                };
            };

            var dpd = DependencyPropertyDescriptor.FromProperty(UserNameProperty, typeof (MyUserControl));
            if (dpd != null)
            {
                dpd.AddValueChanged(this, delegate
                {
                    // ..
                    MyDataContext.UserName = UserName;
                });
            }
        }

        private SubDataContext MyDataContext
        {
            get { return (SubDataContext) DataContext; }
        }

        public string UserName
        {
            get { return (string) GetValue(UserNameProperty); }
            set
            {
                MyDataContext.UserName = value;
                SetValueDp(UserNameProperty, value, "UserName");
            }
        }

        public IList<string> Validate()
        {
            var errs = new List<string>();
            if (!Validation.GetHasError(MyTextBox))
            {
                return errs;
            }

            var errorEnum = Validation.GetErrors(MyTextBox).GetEnumerator();
            while (errorEnum.MoveNext())
            {
                errs.Add((string) errorEnum.Current.ErrorContent);
            }

            return errs;
        }

        public void SetValueDp(DependencyProperty dp, object value, string name)
        {
            SetValue(dp, value);
            if (PropertyChanged != null)
            {
                PropertyChanged(dp, new PropertyChangedEventArgs(name));
            }
        }
    }
}