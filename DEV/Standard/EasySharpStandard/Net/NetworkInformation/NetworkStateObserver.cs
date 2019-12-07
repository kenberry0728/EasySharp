using EasySharp.Observer;
using System.Net.NetworkInformation;

namespace EasySharp.Net.NetworkInformation
{
    public class NetworkStateObserver : ValueObserverBase<NetworkState>
    {
        public NetworkStateObserver()
        {
            NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
            this.DisposeActions.Add(() =>
            {
                NetworkChange.NetworkAvailabilityChanged -= OnNetworkAvailabilityChanged;
            });

            this.SetCurrentValue(ToNetworkState(NetworkInterface.GetIsNetworkAvailable()));
        }

        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            var arg = this.SetCurrentValue(ToNetworkState(e.IsAvailable));
            this.OnValueChange(sender, arg);
        }

        private NetworkState ToNetworkState(bool isAvailable)
        {
            return isAvailable
                ? NetworkState.Available
                : NetworkState.Disable;
        }
    }
}
