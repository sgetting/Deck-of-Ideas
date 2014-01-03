using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace DeckOfIdeas
{
   public class Deck
   {
      public ObservableCollection<Idea> MasterIdeaList { get; private set; }
      private Random Randomizer;

      public Deck(string sourceFilePath)
      {
         MasterIdeaList = new ObservableCollection<Idea>();
         Randomizer = new Random();

         String serializedList = System.IO.File.ReadAllText(sourceFilePath);
         MasterIdeaList = JsonConvert.DeserializeObject(serializedList, typeof(ObservableCollection<Idea>)) as ObservableCollection<Idea>;
      }

      public ObservableCollection<Idea> PickIdeas(int numberOfIdeas, IdeaTag tag = IdeaTag.Any)
      {
         var taggedIdeas = tag == IdeaTag.Any ? MasterIdeaList : new ObservableCollection<Idea>(MasterIdeaList.Where(i => i.TagList.Contains(tag)));

         if (numberOfIdeas >= taggedIdeas.Count())
            return taggedIdeas;

         HashSet<int> ideaIndexes = new HashSet<int>();
         while (ideaIndexes.Count < numberOfIdeas)
            ideaIndexes.Add(Randomizer.Next(taggedIdeas.Count()));

         ObservableCollection<Idea> toReturn = new ObservableCollection<Idea>();
         foreach (int index in ideaIndexes)
            toReturn.Add(taggedIdeas[index]);

         return toReturn;
      }

      public void Sort(Func<Idea, string> orderFunc, bool ascending)
      {
         MasterIdeaList = new ObservableCollection<Idea>(
            ascending ? MasterIdeaList.OrderBy(orderFunc) : MasterIdeaList.OrderByDescending(orderFunc));
      }
   }
}
