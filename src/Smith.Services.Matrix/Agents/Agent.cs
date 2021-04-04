using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Smith.MatrixSdk;
using Smith.MatrixSdk.ApiTypes;
using Smith.MatrixSdk.Extensions;

namespace Smith.Services.Matrix.Agents
{
    public class Agent : IAgent
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        private volatile MatrixClient? _client;
        private volatile string? _accessToken;

        public Agent(ILogger logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public void SetHomeserver(Uri homeserver) =>
            _client = new MatrixClient(_logger, _httpClient, homeserver);

        public void SetAccessToken(string accessToken) =>
            _accessToken = accessToken;

        public Task<LoginResponse> AuthenticateAsync(
            string login,
            string password,
            CancellationToken cancellationToken
        ) => _client.NotNull().Login(login, password, cancellationToken);
    }
}
