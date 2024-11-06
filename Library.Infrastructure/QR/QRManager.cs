using QRCoder.Xaml;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;


namespace Library.Infrastructure.QR
{
    public class QRManager
    {
        public DrawingImage Generate(object info) 
        {
            if (info != null)
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                };
                var jsonString = JsonSerializer.Serialize(info, options);

                var generator = new QRCodeGenerator();
                var data = generator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.H);
                var qrCode = new XamlQRCode(data);
                var image = qrCode.GetGraphic(20);

                return image;
            }
            else
            {
                MessageBox.Show("Выберите элемент для создания QR-кода");
                return null;
            }
            

        }
    }
}
