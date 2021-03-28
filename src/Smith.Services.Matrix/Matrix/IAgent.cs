using System;
using System.Threading;
using System.Threading.Tasks;
using Smith.MatrixSdk.ApiTypes;

namespace Smith.Services.Matrix
{
    public interface IAgent
    {
        public void SetHomeserver(Uri homeserver);
        void SetAccessToken(string accessToken);

        Task<LoginResponse> AuthenticateAsync(
            string login,
            string password,
            CancellationToken cancellationToken = default
        );
    }
}
