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
    /// <summary>
    /// Main window view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// The application title.
        /// </summary>
        private string _title = "Text API";
        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>
        /// The application title.
        /// </value>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// The string empty flag.
        /// </summary>
        private bool idStringEmpty = true;
        /// <summary>
        /// The string identifier.
        /// </summary>
        private string idString;
        /// <summary>
        /// Gets or sets the string identifier.
        /// </summary>
        /// <value>
        /// The string identifier.
        /// </value>
        public string IdString
        {
            get { return idString; }
            set 
            { 
                SetProperty(ref idString, value);
                // Checking input with Regex.
                string tempstr = "";
                foreach (char sym in idString)
                {
                    if (RegexCheck(sym.ToString()))
                        tempstr += sym.ToString();
                }
                idString = tempstr;

                // Checking user's input.
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
        /// <summary>
        /// The content list
        /// </summary>
        private object contentList;
        /// <summary>
        /// Gets or sets the content list.
        /// </summary>
        /// <value>
        /// The content list.
        /// </value>
        public object ContentList
        {
            get { return contentList; }
            set { SetProperty(ref contentList, value); }
        }

        /// <summary>
        /// Gets or sets the API data.
        /// </summary>
        /// <value>
        /// The API data.
        /// </value>
        private API ApiData { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            ApiData = new API();
        }
        /// <summary>
        /// The count command
        /// </summary>
        private DelegateCommand _countCommand;
        /// <summary>
        /// Gets the count command.
        /// </summary>
        /// <value>
        /// The count command.
        /// </value>
        public DelegateCommand CountCommand =>
            _countCommand ?? (_countCommand = new DelegateCommand(ExecuteCountCommand, CanExecuteCountCommand));

        /// <summary>
        /// Executes the count command.
        /// </summary>
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

        /// <summary>
        /// Determines <see cref="CountCommand"/> can be executed or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if command can be executed; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecuteCountCommand()
        {
            // Mirrored flag.
            return !idStringEmpty;
        }

        /// <summary>
        /// Gets the integers from string.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns><c>List</c> without duplcates and integers between 1 and 20; otherwise <c>MessageBox</c> with error message and empty <c>List</c></returns>
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

        /// <summary>
        /// Check text for matching the RegEx that accepts only integers and separators.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>bool</c></returns>
        private bool RegexCheck(string text)
        {
            return new System.Text.RegularExpressions.Regex(@"[\d!,;]").IsMatch(text);
        }
    }
}
