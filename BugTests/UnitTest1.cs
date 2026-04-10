using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;

namespace BugTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_InitialState_IsOpen()
        {
            var bug = new Bug();
            Assert.AreEqual(BugState.Open, bug.State);
        }

        [TestMethod]
        public void Test_Assign_FromOpen()
        {
            var bug = new Bug();
            bug.Assign();
            Assert.AreEqual(BugState.Assigned, bug.State);
        }

        [TestMethod]
        public void Test_Reject_FromOpen()
        {
            var bug = new Bug();
            bug.Reject();
            Assert.AreEqual(BugState.Rejected, bug.State);
        }

        [TestMethod]
        public void Test_StartProgress_FromAssigned()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            Assert.AreEqual(BugState.InProgress, bug.State);
        }

        [TestMethod]
        public void Test_Fix_FromInProgress()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            bug.Fix();
            Assert.AreEqual(BugState.Fixed, bug.State);
        }

        [TestMethod]
        public void Test_Close_FromFixed()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            bug.Fix();
            bug.Close();
            Assert.AreEqual(BugState.Closed, bug.State);
        }

        [TestMethod]
        public void Test_Reopen_FromFixed()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            bug.Fix();
            bug.Reopen();
            Assert.AreEqual(BugState.Reopened, bug.State);
        }

        [TestMethod]
        public void Test_Assign_FromReopened()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            bug.Fix();
            bug.Reopen();
            bug.Assign();
            Assert.AreEqual(BugState.Assigned, bug.State);
        }

        [TestMethod]
        public void Test_Reopen_FromRejected()
        {
            var bug = new Bug();
            bug.Reject();
            bug.Reopen();
            Assert.AreEqual(BugState.Reopened, bug.State);
        }

        [TestMethod]
        public void Test_Reject_FromAssigned()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Reject();
            Assert.AreEqual(BugState.Rejected, bug.State);
        }

        [TestMethod]
        public void Test_Close_FromClosed_Ignored()
        {
            var bug = new Bug();
            bug.Assign();
            bug.StartProgress();
            bug.Fix();
            bug.Close();
            bug.Close();
            Assert.AreEqual(BugState.Closed, bug.State);
        }
    }
}
