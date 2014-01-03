using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeckOfIdeas
{

   public enum IdeaTag { PlotEvent = 1, Characters = 2, Setting = 3, HighConcept = 4, Item = 5, Any = 0 }
   public class Idea
   {
      public string Text { get; set; }
      public string AuthorName { get; set; }
      [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
      public HashSet<IdeaTag> TagList { get; set; }

      [ScriptIgnore]
      public string TagListString
      {
         get
         {
            if (TagList.Count > 0)
            {
               var stringList = TagList.ToList().ConvertAll(tag => tag.ToString());
               stringList.Sort((a, b) => a.CompareTo(b));
               return stringList.Aggregate((a, b) => String.Format("{0}, {1}", a, b));
            }
            else return "";
         }
      }

      public Idea()
         : this("")
      { }

      public Idea(String Text)
      {
         this.Text = Text;
         AuthorName = ""; // no given author
         TagList = new HashSet<IdeaTag>();
      }

      public Idea(String text, String Author)
         : this(text)
      {
         AuthorName = Author;
      }

      public Idea(String text, String Author, String[] tags)
         : this(text, Author)
      {
         foreach (String tag in tags)
         {
            IdeaTag tagToAdd = (IdeaTag)Enum.Parse(typeof(IdeaTag), tag);
            TagList.Add(tagToAdd);
         }
      }

      public int GetWordCount()
      {
         return Text.Split(null).Length; // split on whitespace
      }

      public Idea AddTag(IdeaTag tag)
      {
         TagList.Add(tag);
         return this;
      }

   }
}
