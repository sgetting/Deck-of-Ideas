using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Newtonsoft.Json;

namespace DeckOfIdeas
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private const string filePath = "DeckOfIdeas.json";

      private Deck DeckOfIdeasMain;
      private ObservableCollection<Idea> chosenIdeas;

      private GridViewColumnHeader listViewSortCol = null;
      private SortAdorner listViewSortAdorner = null;

      private bool areEditing = false;
      private int editingIndex = -1;

      public MainWindow()
      {
         InitializeComponent();
         DeckOfIdeasMain = new Deck(filePath);
         chosenIdeas = new ObservableCollection<Idea>();
         IdeasListView.ItemsSource = chosenIdeas;
         DeckListView.ItemsSource = DeckOfIdeasMain.MasterIdeaList;
      }

      #region Draw Tab


      private void DrawButton_Click(object sender, RoutedEventArgs e)
      {
         int[] numbersToGenerate = new int[Enum.GetValues(typeof(IdeaTag)).Length];
         numbersToGenerate[(int)IdeaTag.Any] = AnyCategoryNumberToGenerateBox.Value ?? 0;
         numbersToGenerate[(int)IdeaTag.PlotEvent] = PlotNumberToGenerateBox.Value ?? 0;
         numbersToGenerate[(int)IdeaTag.Characters] = CharactersNumberToGenerateBox.Value ?? 0;
         numbersToGenerate[(int)IdeaTag.Setting] = SettingNumberToGenerateBox.Value ?? 0;
         numbersToGenerate[(int)IdeaTag.HighConcept] = HighConceptNumberToGenerateBox.Value ?? 0;
         numbersToGenerate[(int)IdeaTag.Item] = ItemNumberToGenerateBox.Value ?? 0;

         for (int i = 0; i < numbersToGenerate.Count(); i++)
         {
            var pickedIdeas = DeckOfIdeasMain.PickIdeas(numbersToGenerate[i], (IdeaTag)Enum.ToObject(typeof(IdeaTag), i));
            foreach (Idea pickedIdea in pickedIdeas)
               chosenIdeas.Add(pickedIdea);
         }

         // Get the border of the listview (first child of a listview)
         Decorator border = VisualTreeHelper.GetChild(IdeasListView, 0) as Decorator;
         // Get scrollviewer
         ScrollViewer scrollViewer = border.Child as ScrollViewer;
         scrollViewer.ScrollToBottom();
      }

      private void ClearButton_Click(object sender, RoutedEventArgs e)
      {
         chosenIdeas.Clear();
      }

      private void IdeasListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         int selectedIndex = IdeasListView.SelectedIndex;
         if (selectedIndex == -1)
            DescriptionPanel.DataContext = null;
         else
            DescriptionPanel.DataContext = chosenIdeas[selectedIndex];
      }


      #endregion

      #region Edit Tab


      private void DeleteIdeaButton_Click(object sender, RoutedEventArgs e)
      {
         int deletingIndex = DeckListView.SelectedIndex;
         if (deletingIndex != -1)
         {
            DeckOfIdeasMain.MasterIdeaList.RemoveAt(deletingIndex);
            SaveToFile();
         }
      }

      private void EditIdeaButton_Click(object sender, RoutedEventArgs e)
      {
         editingIndex = DeckListView.SelectedIndex;
         if (editingIndex != -1)
         {
            SetEditState(true);

            Idea toEdit = DeckOfIdeasMain.MasterIdeaList[editingIndex];

            IdeaTextBox.Text = toEdit.Text;
            AuthorTextBox.Text = toEdit.AuthorName;
            PlotCheckBox.IsChecked = toEdit.TagList.Contains(IdeaTag.PlotEvent);
            CharactersCheckBox.IsChecked = toEdit.TagList.Contains(IdeaTag.Characters);
            HighConceptCheckBox.IsChecked = toEdit.TagList.Contains(IdeaTag.HighConcept);
            SettingCheckBox.IsChecked = toEdit.TagList.Contains(IdeaTag.Setting);
            ItemCheckBox.IsChecked = toEdit.TagList.Contains(IdeaTag.Item);
         }
      }

      private void CancelButton_Click(object sender, RoutedEventArgs e)
      {
         SetEditState(false);
         ClearAllFields();
      }

      private void AddSaveButton_Click(object sender, RoutedEventArgs e)
      {
         if (areEditing)
         {
            Idea edited = DeckOfIdeasMain.MasterIdeaList[editingIndex];
            edited.Text = IdeaTextBox.Text;
            edited.AuthorName = AuthorTextBox.Text;
            edited.TagList = CreateTagList();

            RefreshMasterIdeaListView();

            SetEditState(false);
         }
         else
         {
            if (String.IsNullOrWhiteSpace(IdeaTextBox.Text)) return;
            Idea toAdd = new Idea
            {
               Text = IdeaTextBox.Text,
               AuthorName = AuthorTextBox.Text,
               TagList = CreateTagList()
            };
            DeckOfIdeasMain.MasterIdeaList.Add(toAdd);
         }
         SaveToFile();
         ClearAllFields();
      }

      private void EditListViewHeader_Click(object sender, RoutedEventArgs e)
      {
         #region CopyPastedCode
         GridViewColumnHeader column = (sender as GridViewColumnHeader);
         string sortBy = column.Tag.ToString();
         if (listViewSortCol != null)
         {
            AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
            //DeckListView.Items.SortDescriptions.Clear();
         }

         ListSortDirection newDir = ListSortDirection.Ascending;
         if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
            newDir = ListSortDirection.Descending;

         listViewSortCol = column;
         listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
         AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
         //DeckListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
         #endregion

         DeckOfIdeasMain.Sort(input =>
         {
            if (sortBy == "Text")
               return input.Text;
            if (sortBy == "AuthorName")
               return input.AuthorName;
            if (sortBy == "TagListString")
               return input.TagListString;
            return input.AuthorName;
         },
            newDir == ListSortDirection.Ascending);
         DeckListView.ItemsSource = DeckOfIdeasMain.MasterIdeaList;
         RefreshMasterIdeaListView();
      }


      #region Helper methods
      private void SetEditState(bool editing)
      {
         areEditing = editing;
         editingIndex = editing ? editingIndex : -1;
         DeleteIdeaButton.IsEnabled = !editing;
         EditIdeaButton.IsEnabled = !editing;
         AddSaveButton.Content = editing ? "Save" : "Add";
      }

      private HashSet<IdeaTag> CreateTagList()
      {
         HashSet<IdeaTag> tagList = new HashSet<IdeaTag>();

         if (PlotCheckBox.IsChecked ?? false)
            tagList.Add(IdeaTag.PlotEvent);
         if (CharactersCheckBox.IsChecked ?? false)
            tagList.Add(IdeaTag.Characters);
         if (HighConceptCheckBox.IsChecked ?? false)
            tagList.Add(IdeaTag.HighConcept);
         if (SettingCheckBox.IsChecked ?? false)
            tagList.Add(IdeaTag.Setting);
         if (ItemCheckBox.IsChecked ?? false)
            tagList.Add(IdeaTag.Item);

         return tagList;
      }

      private void ClearAllFields()
      {
         IdeaTextBox.Text = "";
         AuthorTextBox.Text = "";

         PlotCheckBox.IsChecked = false;
         CharactersCheckBox.IsChecked = false;
         HighConceptCheckBox.IsChecked = false;
         SettingCheckBox.IsChecked = false;
         ItemCheckBox.IsChecked = false;
      }

      private void SaveToFile()
      {
         string serializedDeck = JsonConvert.SerializeObject(DeckOfIdeasMain.MasterIdeaList, Formatting.Indented);
         File.WriteAllText(filePath, serializedDeck);
      }

      private void RefreshMasterIdeaListView()
      {
         CollectionViewSource.GetDefaultView(DeckOfIdeasMain.MasterIdeaList).Refresh();
      }

      #region MoreCopiedCode
      //Code copy-pasted from http://www.wpf-tutorial.com/listview-control/listview-how-to-column-sorting/
      public class SortAdorner : Adorner
      {
         private static Geometry ascGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

         private static Geometry descGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

         public ListSortDirection Direction { get; private set; }

         public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
         {
            Direction = dir;
         }

         protected override void OnRender(DrawingContext drawingContext)
         {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
               return;

            TranslateTransform transform = new TranslateTransform
                    (
                            AdornedElement.RenderSize.Width - 15,
                            (AdornedElement.RenderSize.Height - 5) / 2
                    );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (Direction == ListSortDirection.Descending)
               geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
         }
      }
      #endregion
      #endregion


      #endregion
   }
}