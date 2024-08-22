using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Abstractions.Shared.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Dictionary<Type, ICommandHandler> _handlers;

        public CommandBus()
        {
            _handlers = new Dictionary<Type, ICommandHandler>();
        }

        public void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            var type = typeof(TCommand);
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, handler);
            }
            else
            {
                _handlers[type] = handler;
            }
        }

        public void Register<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> handler) where TCommand : ICommand<TResponse>
        {
            var type = typeof(TCommand);
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, handler);
            }
            else
            {
                _handlers[type] = handler;
            }
        }

        public void UnRegister<THandler>() where THandler : ICommandHandler
        {
            UnRegister(typeof(THandler));
        }

        private void UnRegister(Type type)
        {
            if (_handlers.ContainsKey(type))
            {
                _handlers.Remove(type);
            }
        }

        public async UniTask Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            var type = typeof(TCommand);
            if (_handlers.ContainsKey(type))
            {
                if (_handlers[type] is ICommandHandler<TCommand> handler)
                {
                    await handler.Execute(command, cancellationToken);
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast handler from {_handlers[type].GetType()} to {typeof(ICommandHandler<TCommand>)}");
                }
            }
        }

        public async UniTask<TResponse> Execute<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            var type = typeof(TCommand);
            if (_handlers.ContainsKey(type))
            {
                if (_handlers[type] is ICommandHandler<TCommand, TResponse> handler)
                {
                    return await handler.Execute(command, cancellationToken);
                }
                else
                {
                    throw new InvalidCastException($"[{GetType().Name}] Cannot cast handler from {_handlers[type].GetType()} to {typeof(ICommandHandler<TCommand>)}");
                }
            }

            return default(TResponse);
        }

        public void Clear()
        {
            _handlers.Clear();
        }
    }
}
