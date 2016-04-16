using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Logic;

namespace Logic.Test
{
    [TestClass]
    public class Vector2_Test
    {
        [TestMethod]
        public void Add()
        {
            // arrange

            Vector2 a = new Vector2(3.5, 4.5);
            Vector2 b = new Vector2(1, 2);

            // act
        
            a.Add(b);

            // assert

            Assert.AreEqual(a, new Vector2(4.5, 6.5));
            Assert.AreEqual(b, new Vector2(1, 2));
            
        }
    }
}
