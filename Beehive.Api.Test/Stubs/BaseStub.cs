using System.Collections.Generic;

namespace Beehive.Api.Test.Stubs
{
    public abstract class BaseStub<TKey, TRequest, TResponse>
    {
        protected readonly IDictionary<TKey, TResponse> Expectations = new Dictionary<TKey, TResponse>();
        protected readonly IList<TRequest> RecordedRequests = new List<TRequest>();

        public void ClearRecordedItems()
        {
            RecordedRequests.Clear();
        }

        public IList<TRequest> GetRecordedRequests()
        {
            return RecordedRequests;
        }

        public void AddExpectation(TKey key, TResponse response)
        {
            Expectations.Add(key, response);
        }

        public void ClearExpectations()
        {
            Expectations.Clear();
        }
    }
}