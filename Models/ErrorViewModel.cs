using System;
using PinkUmbrella.ViewModels;

namespace PinkUmbrella.Models
{
    public class ErrorViewModel: BaseViewModel
    {
        public string ErrorCode { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string OriginalURL { get; set; }
    }
}
