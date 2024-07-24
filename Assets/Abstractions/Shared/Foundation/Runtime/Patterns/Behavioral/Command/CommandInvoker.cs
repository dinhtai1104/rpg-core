using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Asks the command to carry out the request. </summary>
    public class CommandInvoker
    {
        private readonly Stack<ICommand> undo = new Stack<ICommand>();
        private readonly Stack<ICommand> redo = new Stack<ICommand>();

        /// <summary> Execute a command and add it to the undo redo stack if success. </summary>
        /// <param name="command"></param>
        /// <returns>True if the execution was successful.</returns>
        public bool Execute(ICommand command)
        {
            if (command.OnExecute() == true)
            {
                undo.Push(command);
                redo.Clear();
                return true;
            }

            return false;
        }

        /// <summary> Reverse the last command executed. </summary>
        public void Undo()
        {
            if (undo.Count > 0)
            {
                ICommand executed = undo.Pop();
                executed.OnUndo();
                redo.Push(executed);
            }
            else
            {
                Log.Warning("Undo stack empty");
            }
        }

        /// <summary> Replay the last undone command. </summary>
        public void Redo()
        {
            if (redo.Count > 0)
            {
                ICommand undone = redo.Pop();
                undone.OnExecute();
                undo.Push(undone);
            }
            else
            {
                Log.Warning("Redo stack empty");
            }
        }
    }
}