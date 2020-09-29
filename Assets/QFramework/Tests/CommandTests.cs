//using NUnit.Framework;
//using QFramework.Helpers.Command;
//using UnityEngine;
//
//public class CommandTests
//{
//    [Test]
//    public void FireCommandTest()
//    {
//        var testObj = new TestCommandObject {count = 1};
//        Commands.Fire(new TestCommand(testObj));
//        Assert.AreEqual(testObj.count, 2);
//    }
//
//    public class TestCommand : ICommand
//    {
//        TestCommandObject testObj;
//
//        public TestCommand(TestCommandObject testObj)
//        {
//            this.testObj = testObj;
//        }
//
//        public void Fire()
//        {
//            testObj.count++;
//            Debug.Log("[TestCommand] Fired");
//        }
//    }
//
//    public class TestCommandObject
//    {
//        public int count;
//    }
//}