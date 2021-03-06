using System;
using Xamarin.Forms;

namespace MyMap.Interface
{
    public interface IViewToImageService
    {
        public byte[] GetImage(View view);
    }
}
