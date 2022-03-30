using Microsoft.JSInterop;

namespace RegexReplacer.Client.Helper
{
    public class DataSaveHelper
    {
        private readonly IJSRuntime js;

        public DataSaveHelper(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask TickerChanged(string symbol, decimal price)
        {
            await js.InvokeVoidAsync("displayTickerAlert1", symbol, price);
        }
        public async Task Save(string name, string content)
        {
            await js.InvokeVoidAsync("localStorage.setItem", name, content);
        }

        public async Task<string> Read(string name)
        {
            return await js.InvokeAsync<string>("localStorage.getItem", name);
        }

        public async Task Delete(string name)
        {
            await js.InvokeAsync<string>("localStorage.removeItem", name);
        }
    }
}
