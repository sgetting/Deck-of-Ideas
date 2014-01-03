using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeckOfIdeas;

namespace TestDeckOfIdeas
{
    [TestClass]
    public class IdeaTest
    {
        [TestMethod]
        public void IdeaConstructorTest()
        {
            Idea testIdea = new Idea();
            Assert.IsNotNull(testIdea, "WHAT ARE YOU DOING");
            Assert.IsNotNull(testIdea.Text, "THIS SHOULD NOT FAIL");
            Assert.IsNotNull(testIdea.TagList, "HERP DERP");

            string testText = "This is an idea!";
            testIdea = new Idea(testText);
            Assert.AreEqual(testText, testIdea.Text, String.Format("Idea(string idea): text assigned {0}; expected {1}.", testIdea.Text, testText));
            Assert.AreEqual("", testIdea.AuthorName, "Idea(string idea): Author not empty string.");

            string testAuthor = "Jarenth";
            testIdea = new Idea(testText, testAuthor);
            Assert.AreEqual(testText, testIdea.Text, String.Format("Idea(string idea, string author): text assigned {0}; expected {1}.", testIdea.Text, testText));
            Assert.AreEqual(testText, testIdea.Text, String.Format("Idea(string idea, string author): author assigned {0}; expected {1}.", testIdea.AuthorName, testAuthor));
            Assert.IsNotNull(testIdea.TagList, "Idea(string idea, string author): tag list is null.");
            Assert.AreEqual(testIdea.TagList.Count, 0, "Idea(string idea, string author): tag list length not zero.");


        }

        [TestMethod]
        public void IdeaTagConstructorTest()
        {
            string testText = "Paperwork golem deathmatch.";
            string testAuthor = "Jarenth";
            string[] testTagList = {"Plot", "Characters", "Setting", "HighConcept" } ;

            Idea testIdea = new Idea(testText, testAuthor, testTagList);
        }

        [TestMethod]
        public void GetWordCountTest()
        {
            Idea testIdea = new Idea("This is four words", "author");
            Assert.AreEqual(4, testIdea.GetWordCount());
        }

    }
}
