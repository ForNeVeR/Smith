using PropertyChanged;
using ReactiveUI;
using TdLib;

namespace Smith.Model.Settings.Proxy
{
    [AddINotifyPropertyChangedInterface]
    public class ProxyModel
    {
        public int Id { get; set; }

        public TdApi.Proxy Proxy { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsSaved { get; set; }

        public bool IsEditable { get; set; }

        public bool IsRemovable { get; set; }

        public ReactiveCommand<ProxyModel, ProxyModel> EnableCommand { get; set; }

        public ReactiveCommand<ProxyModel, ProxyModel> RemoveCommand { get; set; }

        public bool IsSocks5 { get; set; }

        public bool IsHttp { get; set; }

        public bool IsMtProto { get; set; }

        public bool IsServerInputVisible { get; set; }

        public bool IsUsernameInputVisible { get; set; }

        public bool IsPasswordInputVisible { get; set; }

        public bool IsSecretInputVisible { get; set; }

        public string Label { get; set; }

        public string UnsavedLabel { get; set; }

        public string Server { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Secret { get; set; }

        public static ProxyModel DisabledProxy()
        {
            return new ProxyModel
            {
                Id = -1,
                Label = "Disable proxy",
                IsSaved = true,
                IsServerInputVisible = true,
                IsUsernameInputVisible = true,
                IsPasswordInputVisible = true,
                IsEditable = false,
                IsRemovable = false,
                Server = "",
                Port = "",
                Username = "",
                Password = "",
                Secret = ""
            };
        }

        public static ProxyModel FromProxy(TdApi.Proxy proxy)
        {
            var model = new ProxyModel
            {
                Id = proxy.Id,
                Proxy = proxy,
                IsEnabled = proxy.IsEnabled,
                IsSaved = proxy.Id != 0
            };

            model.Server = proxy.Server ?? "";
            model.Port = proxy.Port == 0 ? "" : proxy.Port.ToString();
            model.Username = "";
            model.Password = "";
            model.Secret = "";

            switch (proxy.Type)
            {
                case TdApi.ProxyType.ProxyTypeSocks5 socks5:
                    model.IsSocks5 = true;
                    model.Username = socks5.Username ?? "";
                    model.Password = socks5.Password ?? "";
                    break;

                case TdApi.ProxyType.ProxyTypeHttp http:
                    model.IsHttp = true;
                    model.Username = http.Username ?? "";
                    model.Password = http.Password ?? "";
                    break;

                case TdApi.ProxyType.ProxyTypeMtproto mtproto:
                    model.IsMtProto = true;
                    model.Secret = mtproto.Secret ?? "";
                    break;
            }

            model.IsServerInputVisible = true;
            model.IsUsernameInputVisible = model.IsSocks5 || model.IsHttp;
            model.IsPasswordInputVisible = model.IsSocks5 || model.IsHttp;
            model.IsSecretInputVisible = model.IsMtProto;

            model.IsRemovable = true;
            model.IsEditable = true;

            return model;
        }

        public TdApi.Proxy ToProxy()
        {
            var proxy = new TdApi.Proxy
            {
                Id = Proxy?.Id ?? 0,
                IsEnabled = Proxy?.IsEnabled ?? false,
                LastUsedDate = Proxy?.LastUsedDate ?? 0
            };

            int.TryParse(Port, out var port);
            proxy.Port = port;
            proxy.Server = Server;

            if (IsSocks5)
            {
                proxy.Type = new TdApi.ProxyType.ProxyTypeSocks5
                {
                    Username = Username,
                    Password = Password
                };
            }

            if (IsHttp)
            {
                proxy.Type = new TdApi.ProxyType.ProxyTypeHttp
                {
                    Username = Username,
                    Password = Password
                };
            }

            if (IsMtProto)
            {
                proxy.Type = new TdApi.ProxyType.ProxyTypeMtproto
                {
                    Secret = Secret
                };
            }

            return proxy;
        }
    }
}
