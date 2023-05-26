using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RandomCordinateGenerator
{
    public class ViewModel : NotifyPropertyChanged
    {
        UdpBase udpBase = new UdpBase();
        RandomGeneratorService service = new RandomGeneratorService();
        private string coordinateAndSize;

        public ICommand ButtonCommand { get; private set; }
        public string CoordinateAndSize 
        {
            get => coordinateAndSize;
            set
            {
                coordinateAndSize = value;
                UpdateProperty("CoordinateAndSize");
            }
                }

        public ViewModel()
        {
            udpBase.ReceiveAction += UdpBase_ReceiveAction;
            udpBase.ReceiveFrom();
            ButtonCommand = new RelayCommand<Button>(SendRandomCordinate);
        }

        private void UdpBase_ReceiveAction(byte[] byteMsg)
        {
            Console.WriteLine(Encoding.Default.GetString(byteMsg));
        }

        public void SendRandomCordinate(Button sender)
        {
            List<int> cordinate = service.generateCordinate();

            string msg = cordinate[0] + "/" + cordinate[1] + "/" + cordinate[2];

            CoordinateAndSize = msg;

            udpBase.SendTo(msg);
        }

    }
}
