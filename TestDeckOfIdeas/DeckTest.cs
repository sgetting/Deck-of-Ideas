using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DeckOfIdeas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDeckOfIdeas
{
    /// <summary>
    /// Summary description for DeckTest
    /// </summary>
    [TestClass]
    public class DeckTest
    {
        public DeckTest()
        {
        }

       

        [TestMethod]
        public void DeckConstructorTest()
        {

            string firstText = "Traditional DnD high fantasy dungeon crawl, except everyone and everything is dogs.";
            string firstAuthor = "Jarenth";
            HashSet<IdeaTag> firstTags = new HashSet<IdeaTag>(){IdeaTag.HighConcept};

            Deck testDeck = new Deck("DeckOfIdeas.json");
            Assert.IsNotNull(testDeck.MasterIdeaList);
            foreach (Idea i in testDeck.MasterIdeaList)
            {
                Assert.IsNotNull(i);
                Assert.IsNotNull(i.Text);
                Assert.IsNotNull(i.AuthorName);
                Assert.IsNotNull(i.TagList);
            }

            Assert.AreEqual(firstText, testDeck.MasterIdeaList.First().Text);
            Assert.AreEqual(firstAuthor, testDeck.MasterIdeaList.First().AuthorName);
            Assert.IsTrue(firstTags.SequenceEqual(testDeck.MasterIdeaList.First().TagList));

            string lastText = "A Necromancer who can’t tell living things from dead ones.";
            string lastAuthor = "syal";
            HashSet<IdeaTag> lastTags = new HashSet<IdeaTag>();

            Assert.AreEqual(lastText, testDeck.MasterIdeaList.Last().Text);
            Assert.AreEqual(lastAuthor, testDeck.MasterIdeaList.Last().AuthorName);
            Assert.IsTrue(lastTags.SequenceEqual(testDeck.MasterIdeaList.Last().TagList));
            
        }


    }
}
