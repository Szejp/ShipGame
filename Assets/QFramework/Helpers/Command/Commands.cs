using System;

namespace QFramework.Helpers.Command
{
    public static class Commands
    {
        public static void Fire(ICommand command)
        {
            command.Fire();
        }

        public static void Fire<T>() where T : ICommand
        {
            ((T)Activator.CreateInstance(typeof(T))).Fire();
        }
    }
}