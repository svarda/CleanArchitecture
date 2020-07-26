using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.InfrastructureServices.Filters {
    public class GlobalPurposeFilter : IAsyncPageFilter {
        public GlobalPurposeFilter() {
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next) {
            await next.Invoke();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) {
            await Task.CompletedTask;
        }
    }
}
