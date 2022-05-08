using System;
using System.Threading.Tasks;

namespace StockTrack.command
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _callback;

        public AsyncRelayCommand(Func<object, Task> callback, Action<Exception> onException = null) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback(parameter);
        }
    }
}
