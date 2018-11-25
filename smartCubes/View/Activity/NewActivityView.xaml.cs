using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;
using smartCubes.ViewModels.Activity;
using Plugin.BLE.Abstractions;
using System.Linq;
using Rg.Plugins.Popup.Services;

namespace smartCubes.View.Activity
{
    public partial class NewActivityView : ContentPage
    {
        
        public NewActivityView()
        {
            InitializeComponent();

            BindingContext = new NewActivityViewModel();
        }
       
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
           /* if (e == null)
            {
                ((ListView)sender).SelectedItem = null;
                return;
            }
            Debug.WriteLine("Seleccionado: " + e.Item);

            try
            {
                //Dispositivos seleccionado
                var deviceConnected = ((ListView)sender).SelectedItem as IDevice;
                //Conectamos con el dispositivo

                if (deviceConnected.State == DeviceState.Disconnected)
                {
                    await adapter.ConnectToDeviceAsync(deviceConnected);

                    //Mensaje en pantalla
                    await DisplayAlert(deviceConnected.Name, "Estado:" + deviceConnected.State, "OK");

                    //Servicios y caracteristicas y descriptores
                    var services = await deviceConnected.GetServicesAsync();
                    var characteristics = await services[0].GetCharacteristicsAsync();
                    //var descriptors = await characteristics[0].GetDescriptorsAsync();

                    //lectura de datos
                    characteristics[0].ValueUpdated += (s, a) =>
                    {
                        byte[] valueBytes = a.Characteristic.Value;
                        //await characteristics[0].ReadAsync(); //lee constantemente
                        String valueString = string.Concat(valueBytes.Select(b => b.ToString("X2")));
                        Debug.WriteLine(valueString, "Leyendo datos de " + deviceConnected.Name + ": ");
                    };
                    await characteristics[0].StartUpdatesAsync();

                   
                    //var bytes = await characteristics[0].ReadAsync();
                }
                else
                {
                    //Si esta conectado, se desconecta
                    await adapter.DisconnectDeviceAsync(deviceConnected);
                    await DisplayAlert(deviceConnected.Name, "Estado:" + deviceConnected.State, "OK");
                }

            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                ((ListView)sender).SelectedItem = null; // eliminar seleccion
            }*/
        }
        private void RefreshData()
        {
            //lDevices.ItemsSource = null;
            //lDevices.ItemsSource = deviceList;
        }
       
        private void OnButtonSaveClicked(object sender, EventArgs e)
        {

        }
        private void OnButtonAddDeviceClicked(object sender, EventArgs e)
        {
            AddDevicePopUp addDevice = new AddDevicePopUp();
            PopupNavigation.Instance.PushAsync(addDevice);
        }
    }
}
