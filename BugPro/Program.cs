using System;
using Stateless;
namespace BugPro
{
    public enum BugState
    {
        Open,
        Assigned,
        InProgress,
        Fixed,
        Closed,
        Reopened,
        Rejected
    }
    public enum BugTrigger
    {
        Assign,
        StartProgress,
        Fix,
        Close,
        Reopen,
        Reject
    }
    public class Bug
    {
        private StateMachine<BugState, BugTrigger> _machine;

        public BugState State => _machine.State;

        public Bug()
        {
            _machine = new StateMachine<BugState, BugTrigger>(BugState.Open);

            _machine.Configure(BugState.Open)
                .Permit(BugTrigger.Assign, BugState.Assigned)
                .Permit(BugTrigger.Reject, BugState.Rejected);

            _machine.Configure(BugState.Assigned)
                .Permit(BugTrigger.StartProgress, BugState.InProgress)
                .Permit(BugTrigger.Reject, BugState.Rejected);

            _machine.Configure(BugState.InProgress)
                .Permit(BugTrigger.Fix, BugState.Fixed);

            _machine.Configure(BugState.Fixed)
                .Permit(BugTrigger.Close, BugState.Closed)
                .Permit(BugTrigger.Reopen, BugState.Reopened);

            _machine.Configure(BugState.Reopened)
                .Permit(BugTrigger.Assign, BugState.Assigned)
                .Permit(BugTrigger.Reject, BugState.Rejected);

            _machine.Configure(BugState.Rejected)
                .Permit(BugTrigger.Reopen, BugState.Reopened);

            _machine.Configure(BugState.Closed)
                .Ignore(BugTrigger.Close);
        }
        public void Assign() => _machine.Fire(BugTrigger.Assign);
        public void StartProgress() => _machine.Fire(BugTrigger.StartProgress);
        public void Fix() => _machine.Fire(BugTrigger.Fix);
        public void Close() => _machine.Fire(BugTrigger.Close);
        public void Reopen() => _machine.Fire(BugTrigger.Reopen);
        public void Reject() => _machine.Fire(BugTrigger.Reject);
    }
    class Program
    {
        static void Main(string[] args)
        {
            var bug = new Bug();
            Console.WriteLine($"Начальное состояние: {bug.State}");

            bug.Assign();
            Console.WriteLine($"После Assign: {bug.State}");

            bug.StartProgress();
            Console.WriteLine($"После StartProgress: {bug.State}");

            bug.Fix();
            Console.WriteLine($"После Fix: {bug.State}");

            bug.Close();
            Console.WriteLine($"После Close: {bug.State}");
        }
    }
}
