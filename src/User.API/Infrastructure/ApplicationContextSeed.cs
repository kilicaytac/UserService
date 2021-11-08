using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Infrastructure
{
    public class ApplicationContextSeed
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var policy = CreatePolicy();

            //await policy.ExecuteAsync(async () =>
            //{
            //});
        }

        private AsyncRetryPolicy CreatePolicy(int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                    }
                );
        }
    }
}
