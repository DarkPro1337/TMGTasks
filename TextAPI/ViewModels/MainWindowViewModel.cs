using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TextAPI.Data;
using TextAPI.Logic;

namespace TextAPI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Text API";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool idStringEmpty = true;
        private string idString;
        public string IdString
        {
            get { return idString; }
            set 
            { 
                SetProperty(ref idString, value);
                string tempstr = "";
                foreach (char sym in idString)
                {
                    if (TextBoxСheck(sym.ToString()))
                        tempstr += sym.ToString();
                }
                idString = tempstr;

                if (idString == String.Empty)
                {
                    idStringEmpty = true;
                    CountCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    idStringEmpty = false;
                    CountCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private object contentList;
        public object ContentList
        {
            get { return contentList; }
            set { SetProperty(ref contentList, value); }
        }

        private API ApiData { get; set; }
        public MainWindowViewModel()
        {
            ApiData = new API();
        }
        private DelegateCommand _countCommand;

        public DelegateCommand CountCommand =>
            _countCommand ?? (_countCommand = new DelegateCommand(ExecuteCountCommand, CanExecuteCountCommand));

        private async void ExecuteCountCommand()
        {
            List<int> idList = GetIntegersFromString(IdString);
            try
            {
                List<Text> textList = await ApiData.GetTextByIdAsync(idList);
                ContentList = WordCount.GetListTextInfo(textList);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool CanExecuteCountCommand()
        {
            return !idStringEmpty;
        }

        public static List<int> GetIntegersFromString(string Text)
        {
            Text = Text.Replace(" ", String.Empty);
            try
            {
                List<int> listWithoutDublicate = new List<int>();
                char[] decidersArray = { ',', ';' };

                List<int> list = new List<int>(Text.Split(decidersArray).Select(int.Parse));
                foreach (var item in list)
                {
                    if (item >= 1 & item <= 20)
                    {
                        if (listWithoutDublicate.Contains(item)) { }
                        else
                            listWithoutDublicate.Add(item);
                    }
                }
                return listWithoutDublicate;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<int>();
            }

        }

        private bool TextBoxСheck(string text)
        {
            return new System.Text.RegularExpressions.Regex(@"[\d!,;]").IsMatch(text);
        }
    }
}
