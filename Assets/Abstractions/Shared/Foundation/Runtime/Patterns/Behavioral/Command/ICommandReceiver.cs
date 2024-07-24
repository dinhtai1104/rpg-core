using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary>
    /// Knows how to perform the operations associated with carrying out the request.
    /// </summary>
    public interface ICommandReceiver
    {
        /// <summary>
        /// Perform the action of the command.
        /// </summary>
        /// <returns>True if the execution was successful.</returns>
        bool DoAction();

        /// <summary>
        /// Undoe the changes of OnExecute.
        /// </summary>
        void UndoAction();
    }

    /// <summary>
    /// Knows how to perform the operations associated with carrying out the request.
    /// </summary>
    public interface ICommandReceiver<in T>
    {
        /// <summary>
        /// Perform the action of the command.
        /// </summary>
        /// <param name="value">Command' parameter.</param>
        /// <returns>True if the execution was successful.</returns>
        bool DoAction(T value);

        /// <summary>
        /// Undoe the changes of OnExecute.
        /// </summary>
        void UndoAction();
    }

    /// <summary>
    /// Knows how to perform the operations associated with carrying out the request.
    /// </summary>
    public interface ICommandReceiver<in T0, in T1>
    {
        /// <summary>
        /// Perform the action of the command.
        /// </summary>
        /// <param name="value0">First parameter</param>
        /// <param name="value1">Second parameter</param>
        /// <returns>True if the execution was successful.</returns>
        bool DoAction(T0 value0, T1 value1);

        /// <summary>
        /// Undoe the changes of OnExecute.
        /// </summary>
        void UndoAction();
    }

    /// <summary>
    /// Knows how to perform the operations associated with carrying out the request.
    /// </summary>
    public interface ICommandReceiver<in T0, in T1, in T2>
    {
        /// <summary>
        /// Perform the action of the command.
        /// </summary>
        /// <param name="value0">First parameter</param>
        /// <param name="value1">Second parameter</param>
        /// <param name="value3">Third parameter</param>
        /// <returns>True if the execution was successful.</returns>
        bool DoAction(T0 value0, T1 value1, T2 value2);

        /// <summary>
        /// Undoe the changes of OnExecute.
        /// </summary>
        void UndoAction();
    }
}